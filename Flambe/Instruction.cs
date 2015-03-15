namespace Flambe
{
    using System;
    using SQLite;

    public class Instruction
    {
        public Recipe Parent;

        [PrimaryKey, AutoIncrement]
        public int InstructionId { get; set; }
        public int RecipeId { get; set; }
        public int InstructionOrder { get; set; }
        public string Text { get; set; }

        public Instruction()
        {
        }

        public Instruction(Recipe recipe)
        {
            Parent = recipe;
        }

        public void Commit()
        {
            if (this.RecipeId == 0)
            {
                if (Parent == null || Parent.RecipeId == 0)
                {
                    throw new InvalidOperationException("Can't get parent recipe id");
                }

                this.RecipeId = Parent.RecipeId;
            }

            if (this.InstructionId == 0)
            {
                FlambeDB.DbConnection.Insert(this);
            }
            else
            {
                FlambeDB.DbConnection.Update(this);
            }
        }
        
        internal void Delete()
        {
            if (this.InstructionId == 0)
            {
                return;
            }

            FlambeDB.DbConnection.Delete<Instruction>(this.InstructionId);
        }
    }
}
