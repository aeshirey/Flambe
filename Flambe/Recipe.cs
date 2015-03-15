﻿namespace Flambe
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Net.Http;
    using System.Text;
    using System.Web.Script.Serialization;
    using SQLite;

    public class Recipe
    {
        public const string JsonUrl = "http://flambe.dingostick.com/recipes/json.php?rowid={0}";
        public const string CardUrl = "http://flambe.dingostick.com/recipes/view.php?rowid={0}";
        
        [PrimaryKey, AutoIncrement]
        public int RecipeId { get; set; }
        public string Name { get; set; }
        public string Credit { get; set; }
        public string Comment { get; set; }
        public string Cuisine { get; set; }
        public string Category { get; set; }
        public string Servings { get; set; }
        public string PrepTime { get; set; }
        public string CookTime { get; set; }
        public float Rating { get; set; }
        public List<Ingredient> Ingredients;
        public List<Instruction> Instructions;

        public Recipe()
        {
            this.Ingredients = new List<Ingredient>();
            this.Instructions = new List<Instruction>();
        }

        public static Recipe DownloadRecipe(int recipeId)
        {
            var url = string.Format(JsonUrl, recipeId);

            using (var client = new HttpClient())
            {
                var response = client.GetAsync(new Uri(url)).Result;
                if (response.IsSuccessStatusCode)
                {
                    var json = response.Content.ReadAsStringAsync().Result.TrimEnd();
                    var deserialized = FromJson(json);

                    // setup for committing
                    deserialized.RecipeId = 0;

                    foreach (var ingredient in deserialized.Ingredients)
                    {
                        ingredient.Parent = deserialized;
                        ingredient.RecipeId = 0;
                    }

                    foreach (var instruction in deserialized.Instructions)
                    {
                        instruction.Parent = deserialized;
                        instruction.RecipeId = 0;
                    }

                    return deserialized;
                }
            }

            return null;
        }

        public void Commit()
        {
            if (this.RecipeId == 0)
            {
                FlambeDB.DbConnection.Insert(this);
            }
            else
            {
                FlambeDB.DbConnection.Update(this);
            }

            for (var i = 0; i < this.Ingredients.Count; i++)
            {
                this.Ingredients[i].IngredientOrder = i + 1;
                this.Ingredients[i].Commit();
            }

            for (var i = 0; i < this.Instructions.Count; i++)
            {
                this.Instructions[i].InstructionOrder = i + 1;
                this.Instructions[i].Commit();
            }
        }

        public void LoadChildren()
        {
            if (Instructions == null || Instructions.Count == 0)
            {
                Instructions = FlambeDB.DbConnection.Table<Instruction>()
                    .Where(i => i.RecipeId == RecipeId)
                    .OrderBy(i => i.InstructionOrder)
                    .ToList();

                foreach (var instruction in Instructions)
                {
                    instruction.Parent = this;
                }
            }

            if (Ingredients == null || Ingredients.Count == 0)
            {
                Ingredients = FlambeDB.DbConnection.Table<Ingredient>()
                    .Where(i => i.RecipeId == RecipeId)
                    .OrderBy(i => i.IngredientOrder)
                    .ToList();

                foreach (var ingredient in Ingredients)
                {
                    ingredient.Parent = this;
                }
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
            // kill all circular references
            foreach (var ingredient in Ingredients)
            {
                ingredient.Parent = null;
            }

            foreach (var instruction in Instructions)
            {
                instruction.Parent = null;
            }

            var jss = new JavaScriptSerializer();
            return jss.Serialize(this);
        }

        internal void Delete()
        {
            foreach (var ingredient in this.Ingredients)
            {
                ingredient.Delete();
            }

            foreach (var instruction in this.Instructions)
            {
                instruction.Delete();
            }

            FlambeDB.DbConnection.Delete<Recipe>(this.RecipeId);
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
