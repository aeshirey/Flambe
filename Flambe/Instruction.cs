using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SQLite;

namespace Flambe
{
    public class Instruction
    {
        public Recipe parent;

        [PrimaryKey, AutoIncrement]
        public int InstructionId { get; set; }
        public int RecipeId { get; set; }
        public int InstructionOrder { get; set; }
        public string Text { get; set; }

        public Instruction() { }
        public Instruction(Recipe recipe)
        {
            parent = recipe;
        }

        public void Commit()
        {
            if (this.RecipeId == 0)
            {
                if (parent == null || parent.RecipeId == 0)
                {
                    throw new InvalidOperationException("Can't get parent recipe id");
                }
                this.RecipeId = parent.RecipeId;
            }

            if (this.InstructionId == 0)
            {
                FlambeDB.connection.Insert(this);
            }
            else
            {
                FlambeDB.connection.Update(this);
            }
        }


        internal void Delete()
        {
            if (this.InstructionId == 0)
            {
                return;
            }

            FlambeDB.connection.Delete<Instruction>(this.InstructionId);
        }
    };

}
