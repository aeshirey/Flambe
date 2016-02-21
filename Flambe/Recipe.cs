namespace Flambe
{
    using System;
    using System.Collections.Generic;
    using System.Collections.Specialized;
    using System.Linq;
    using System.Net;
    using System.Net.Http;
    using System.Text;
    using System.Threading.Tasks;
    using System.Web.Script.Serialization;
    using SQLite;

    public class Recipe
    {
        public const string ApiGetUrl = "http://flambe.azurewebsites.net/api/get/{0}";
        public const string ApiUploadUrl = "http://flambe.azurewebsites.net/api/upload";
        public const string CardUrl = "http://flambe.azurewebsites.net/recipe/{0}";

        [PrimaryKey]
        public Guid RecipeId { get; set; }
        public string Name { get; set; }
        public string Credit { get; set; }
        public string Comment { get; set; }
        public string Cuisine { get; set; }
        public string Category { get; set; }
        public string Servings { get; set; }
        public string PrepTime { get; set; }
        public string CookTime { get; set; }
        public float Rating { get; set; }

        public IList<Ingredient> Ingredients;
        public IList<Instruction> Instructions;

        public Recipe()
        {
            this.Ingredients = new List<Ingredient>();
            this.Instructions = new List<Instruction>();
        }

        /// <summary>
        /// Take a list of recipe ids and attempt to download them all
        /// </summary>
        /// <param name="recipeIds">A string containing comma-separated ids and ranges, such as "90140, 90110-90112, 99055"</param>
        /// <returns>A Tuple&lt;int,int&gt; indicating how many downloads succeeded and how many were attempted</returns>
        public static async Task<Tuple<int, int>> DownloadRecipe(IEnumerable<Guid> recipeIds)
        {
            int attempted = 0, succeeded = 0;
            foreach (var recipeId in recipeIds)
            {
                if (await DownloadRecipe(recipeId))
                {
                    succeeded++;
                }

                attempted++;
            }

            return new Tuple<int, int>(succeeded, attempted);
        }

        public static async Task<bool> DownloadRecipe(Guid recipeId)
        {
            var url = string.Format(ApiGetUrl, recipeId);

            using (var client = new HttpClient())
            {
                var response = await client.GetAsync(new Uri(url));

                if (response.IsSuccessStatusCode)
                {
                    var json = await response.Content.ReadAsStringAsync();

                    Recipe deserialized = FromJson(json.Trim());

                    if (deserialized == null)
                    {
                        return false;
                    }


                    //foreach (var ingredient in deserialized.Ingredients)
                    //{
                    //    ingredient.Parent = deserialized;
                    //    ingredient.RecipeId = Guid.Empty;
                    //}

                    //foreach (var instruction in deserialized.Instructions)
                    //{
                    //    instruction.Parent = deserialized;
                    //    instruction.RecipeId = Guid.Empty;
                    //}

                    deserialized.Commit();
                    return true;
                }
            }

            return false;
        }

        public void Commit()
        {
            FlambeDB.DbConnection.InsertOrReplace(this);
            //if (this.RecipeId == Guid.Empty)
            //{
            //    FlambeDB.DbConnection.Insert(this);
            //}
            //else
            //{
            //    FlambeDB.DbConnection.Update(this);
            //}

            foreach (var ingredient in this.Ingredients)
            {
                ingredient.RecipeId = RecipeId;
                FlambeDB.DbConnection.InsertOrReplace(ingredient);
            }

            foreach (var instruction in this.Instructions)
            {
                instruction.RecipeId = RecipeId;
                FlambeDB.DbConnection.InsertOrReplace(instruction);
            }

            //for (var i = 0; i < this.Ingredients.Count; i++)
            //{
            //    this.Ingredients[i].IngredientOrder = i + 1;
            //    this.Ingredients[i].Commit();
            //}

            //for (var i = 0; i < this.Instructions.Count; i++)
            //{
            //    this.Instructions[i].InstructionOrder = i + 1;
            //    this.Instructions[i].Commit();
            //}
        }

        public void LoadChildren()
        {
            Instructions = FlambeDB.DbConnection.Table<Instruction>()
                .Where(i => i.RecipeId == RecipeId)
                .OrderBy(i => i.InstructionOrder)
                .ToList();

            foreach (var instruction in Instructions)
            {
                instruction.Parent = this;
            }

            Ingredients = FlambeDB.DbConnection.Table<Ingredient>()
                .Where(i => i.RecipeId == RecipeId)
                .OrderBy(i => i.IngredientOrder)
                .ToList();

            foreach (var ingredient in Ingredients)
            {
                ingredient.Parent = this;
            }
        }

        public string ToHtml()
        {
            var sb = new StringBuilder();
            sb.AppendLine("<h1>" + this.Name + "</h1>");

            sb.AppendLine("<!-- id: " + this.RecipeId.ToString() + " -->");

            Func<string, string, bool> add = (k, v) => !string.IsNullOrEmpty(v)
                    && sb.AppendLine(string.Format("{0}: {1}<br/>", k, v)) != null;

            add("Credit", this.Credit);
            add("Comment", this.Comment);
            add("Cuisine", this.Cuisine);
            add("Category", this.Category);
            add("Servings", this.Servings);
            add("Prep Time", this.PrepTime);
            add("Cook Time", this.CookTime);
            add("Rating", 0 == this.Rating ? string.Empty : (Math.Round(this.Rating * 2) / 2).ToString());

            if (this.Ingredients.Count > 0)
            {
                sb.AppendLine("<h2>Ingredients</h2>\n");
                sb.AppendLine("<ul>");
                foreach (var ingredient in this.Ingredients)
                {
                    var ingredientContent = ingredient.Quantity
                        + (string.IsNullOrEmpty(ingredient.Units) ? string.Empty : " " + ingredient.Units)
                        + (string.IsNullOrEmpty(ingredient.Item) ? string.Empty : " " + ingredient.Item)
                        + (string.IsNullOrEmpty(ingredient.Remarks) ? string.Empty : ", " + ingredient.Remarks)
                        + (ingredient.IsOptional ? " (optional" : string.Empty);
                    sb.AppendLine("<li>" + ingredientContent + "</li>");
                }

                sb.AppendLine("</ul>");
            }

            if (this.Instructions.Count > 0)
            {
                sb.AppendLine("<h2>Instructions</h2>");
                sb.AppendLine("<ol>");
                foreach (var instruction in this.Instructions)
                {
                    sb.AppendLine("<li>" + instruction.Text + "</li>");
                }

                sb.AppendLine("</ol>");
            }

            return sb.ToString();
        }

        public string ToJson()
        {
            // kill all circular references prior to serialization
            foreach (var ingredient in Ingredients)
            {
                ingredient.Parent = null;
            }

            foreach (var instruction in Instructions)
            {
                instruction.Parent = null;
            }

            var jss = new JavaScriptSerializer();
            var serialized = jss.Serialize(this);
            return serialized;
        }

        internal void Delete()
        {
            foreach (var ingredient in this.Ingredients)
            {
                FlambeDB.DbConnection.Delete(ingredient);
            }

            foreach (var instruction in this.Instructions)
            {
                FlambeDB.DbConnection.Delete(instruction);
                //instruction.Delete();
            }

            FlambeDB.DbConnection.Delete<Recipe>(this.RecipeId);
        }

        internal int? Upload()
        {
            using (var client = new WebClient())
            {
                var payload = new NameValueCollection()
                {
                    { "json", ToJson() }
                };

                var response = client.UploadValues(ApiUploadUrl, payload);
                var decoded = Encoding.UTF8.GetString(response);

                int result;
                return int.TryParse(decoded, out result) ? result : (int?)null;
            }
        }

        private static Recipe FromJson(string json)
        {
            var jss = new JavaScriptSerializer();
            try
            {
                return jss.Deserialize<Recipe>(json);
            }
            catch
            {
                return null;
            }
        }
    }
}
