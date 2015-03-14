using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SQLite;

namespace Flambe
{
    public class Ingredient
    {
        public Recipe parent;

        [PrimaryKey, AutoIncrement]
        public int IngredientId { get; set; }
        public int RecipeId { get; set; }
        public string Quantity { get; set; }
        public string Units { get; set; }
        public string Item { get; set; }
        public string Remarks { get; set; }
        public bool IsOptional { get; set; }
        public string GroupName { get; set; }
        public int GroupOrder { get; set; }
        public int IngredientOrder { get; set; }


        public Ingredient() { }
        public Ingredient(Recipe recipe)
        {
            parent = recipe;
        }

        public bool Delete()
        {
            if (IngredientId == 0)
            {
                return false;
            }

            FlambeDB.connection.Delete<Ingredient>(this.IngredientId);

            parent.Ingredients.Remove(this);
            return true;
        }

        public void Commit()
        {
            if (RecipeId == 0)
            {
                if (parent == null || parent.RecipeId == 0)
                {
                    throw new InvalidOperationException("Can't get parent recipe id");
                }
                RecipeId = parent.RecipeId;
            }

            if (IngredientId == 0)
            {
                IngredientId = FlambeDB.connection.Insert(this);
            }
            else
            {
                FlambeDB.connection.Update(this);
            }
        }
    }
}
