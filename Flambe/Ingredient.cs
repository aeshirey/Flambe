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
        private Recipe parent;

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
            this.parent = recipe;
        }

        public bool Delete()
        {
            if (this.IngredientId == 0)
                return false;

            FlambeDB.connection.Delete<Ingredient>(this);

            //if (FlambeDB.Execute("DELETE FROM ingredients WHERE ingredient_id={0}", this.IngredientId) == 0)
            //    return false;

            parent.Ingredients.Remove(this);
            return true;
        }

        public void Commit()
        {
            if (this.IngredientId == 0)
                this.IngredientId = FlambeDB.connection.Insert(this);
            else
                FlambeDB.connection.Update(this);
            /*
                IngredientId = int.Parse(ingredientRow["ingredient_id"].ToString()),
                RecipeId = int.Parse(ingredientRow["recipe_id"].ToString()),
                Quantity = ingredientRow["quantity"].ToString(),
                Units = ingredientRow["units"].ToString(),
                Item = ingredientRow["item"].ToString(),
                Remarks = ingredientRow["remarks"].ToString(),
                IsOptional = ingredientRow["is_optional"].ToString() == "1",
                GroupName = ingredientRow["group_name"].ToString(),
                GroupOrder = int.Parse(ingredientRow["group_order"].ToString()),
                IngredientOrder = int.Parse(ingredientRow["ingredient_order"].ToString())
             */
            //if (this.IngredientId == 0)
            //{
            //    this.IngredientId = (int)FlambeDB.Execute("INSERT INTO ingredients (recipe_id, quantity, units, item, remarks, is_optional, ingredient_order) VALUES ({0}, '{1}', '{2}', '{3}', '{4}', {5}, {6}",
            //        parent.RecipeId, this.Quantity, this.Units, this.Item, this.Remarks, this.IsOptional ? 1 : 0, this.IngredientOrder);
            //}
            //else
            //{
            //    FlambeDB.Execute("UPDATE ingredients SET quantity='{0}', units='{1}', item='{2}', remarks='{3}', is_optional={4}, ingredient_order={5} WHERE ingredient_id={6} LIMIT 1",
            //        this.Quantity, this.Units, this.Item, this.Remarks, this.IsOptional ? 1 : 0, this.IngredientOrder, this.IngredientId);
            //}
        }
    }
}
