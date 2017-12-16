namespace Flambe
{
    using SQLite;

    public class FlambeDB : SQLiteConnection
    {
        public const string DatabaseFile = @"recipes.sqlite";
        public static SQLiteConnection DbConnection;
        public FlambeDB(string databaseFile = DatabaseFile) : base(databaseFile)
        {
            CreateTable<Recipe>();
            CreateTable<Instruction>();
            CreateTable<Ingredient>();
            Commit();
        }
    }
}
