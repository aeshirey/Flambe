namespace Flambe
{
    using System;
    using SQLite;

    public class Ingredient
    {
        public Recipe Parent;

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

        public Ingredient()
        { 
        }

        public Ingredient(Recipe recipe)
        {
            Parent = recipe;
        }

        public bool Delete()
        {
            if (IngredientId == 0)
            {
                return false;
            }

            FlambeDB.DbConnection.Delete<Ingredient>(this.IngredientId);

            Parent.Ingredients.Remove(this);
            return true;
        }

        public void Commit()
        {
            if (RecipeId == 0)
            {
                if (Parent == null || Parent.RecipeId == 0)
                {
                    throw new InvalidOperationException("Can't get parent recipe id");
                }

                RecipeId = Parent.RecipeId;
            }

            if (IngredientId == 0)
            {
                IngredientId = FlambeDB.DbConnection.Insert(this);
            }
            else
            {
                FlambeDB.DbConnection.Update(this);
            }
        }
    }
}
