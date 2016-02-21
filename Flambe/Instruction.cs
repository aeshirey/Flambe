namespace Flambe
{
    using System;
    using SQLite;

    public class Instruction
    {
        public Recipe Parent;

        [PrimaryKey]
        public Guid InstructionId { get; set; }
        public Guid RecipeId { get; set; }
        public int InstructionOrder { get; set; }
        public string Text { get; set; }

        public Instruction()
        {
            InstructionId = Guid.NewGuid();
        }

        public Instruction(Recipe recipe)
        {
            InstructionId = Guid.NewGuid();
            Parent = recipe;
        }

        public void Commit()
        {
            if (this.RecipeId == null || this.RecipeId == Guid.Empty)
            {
                if (Parent == null || Parent.RecipeId == Guid.Empty)
                {
                    throw new InvalidOperationException("Can't get parent recipe id");
                }

                this.RecipeId = Parent.RecipeId;
            }

            if (this.InstructionId == null || this.InstructionId == Guid.Empty)
            {
                FlambeDB.DbConnection.Insert(this);
            }
            else
            {
                FlambeDB.DbConnection.Update(this);
            }
        }

        //internal void Delete()
        //{
        //    if (this.InstructionId == null || this.InstructionId == Guid.Empty)
        //    {
        //        return;
        //    }

        //    FlambeDB.DbConnection.Delete<Instruction>(this.InstructionId);
        //}
    }
}
