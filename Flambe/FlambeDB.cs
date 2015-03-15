namespace Flambe
{
    using SQLite;

    public static class FlambeDB
    {
        public const string DatabaseFile = @"recipes.sqlite";
        public static SQLiteConnection DbConnection;
        public static void LoadDB()
        {
            DbConnection = new SQLiteConnection(DatabaseFile);
            DbConnection.CreateTable<Recipe>();
            DbConnection.CreateTable<Instruction>();
            DbConnection.CreateTable<Ingredient>();
            DbConnection.Commit();
        }

        public static void CloseDB()
        {
            DbConnection.Close();
        }
    }
}
