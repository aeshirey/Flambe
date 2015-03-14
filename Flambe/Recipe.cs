using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Script.Serialization;
using SQLite;

namespace Flambe
{
    public class Recipe
    {
        [PrimaryKey]
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

        public void Commit()
        {
            if (this.RecipeId == 0)
                this.RecipeId = FlambeDB.connection.Insert(this);
            else
                FlambeDB.connection.Update(this);

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
            if (this.Instructions == null || this.Instructions.Count == 0)
                this.Instructions = FlambeDB.connection.Table<Instruction>()
                    .Where(i => i.RecipeId == this.RecipeId)
                    .OrderBy(i => i.InstructionOrder)
                    .ToList();

            if (this.Ingredients == null || this.Ingredients.Count == 0)
                this.Ingredients = FlambeDB.connection.Table<Ingredient>()
                    .Where(i => i.RecipeId == this.RecipeId)
                    .OrderBy(i => i.IngredientOrder)
                    .ToList();
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
                        + (ingredient.IsOptional ? " (optional" : string.Empty)
                        ;
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

        internal void Delete()
        {
            foreach (var ingredient in this.Ingredients)
                ingredient.Delete();

            foreach (var instruction in this.Instructions)
                instruction.Delete();

            FlambeDB.connection.Delete<Recipe>(this);
        }
    }
}
