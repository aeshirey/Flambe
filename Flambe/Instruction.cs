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
        [PrimaryKey, AutoIncrement]
        public int InstructionId { get; set; }
        public int RecipeId { get; set; }
        public int InstructionOrder { get; set; }
        public string Text { get; set; }

        public Instruction() { }

        public void Commit()
        {
            if (this.InstructionId == 0)
                this.InstructionId = FlambeDB.connection.Insert(this);
            else
                FlambeDB.connection.Update(this);
        }

        public void Commit(Recipe parent)
        {
            this.RecipeId = parent.RecipeId;
            this.Commit();
        }

        internal void Delete()
        {
            if (this.InstructionId == 0)
                return;

            FlambeDB.connection.Delete<Instruction>(this);
        }
    };

}
