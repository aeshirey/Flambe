namespace Flambe
{
    using System;
    using SQLite;

    public class Ingredient
    {
        public Recipe Parent;

        [PrimaryKey]
        public Guid IngredientId { get; set; }
        public Guid RecipeId { get; set; }
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
            IngredientId = Guid.NewGuid();
        }

        public Ingredient(Recipe recipe)
        {
            IngredientId = Guid.NewGuid();
            Parent = recipe;
        }

        //public bool Delete()
        //{
        //    if (IngredientId == null || IngredientId == Guid.Empty)
        //    {
        //        return false;
        //    }

        //    FlambeDB.DbConnection.Delete<Ingredient>(this.IngredientId);

        //    Parent.Ingredients.Remove(this);
        //    return true;
        //}

        public void Commit()
        {
            if (RecipeId == null || RecipeId == Guid.Empty)
            {
                if (Parent == null || Parent.RecipeId == null || Parent.RecipeId == Guid.Empty)
                {
                    throw new InvalidOperationException("Can't get parent recipe id");
                }

                RecipeId = Parent.RecipeId;
            }

            FlambeDB.DbConnection.InsertOrReplace(this);
        }
    }
}
