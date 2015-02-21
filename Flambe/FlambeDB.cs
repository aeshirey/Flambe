using SQLite;

namespace Flambe
{
    static class FlambeDB
    {
        public const string DATABASE_FILE = @"recipes.sqlite";
        public static SQLiteConnection connection;
        public static void LoadDB()
        {
            if (System.IO.File.Exists(DATABASE_FILE))
            {
                connection = new SQLiteConnection(DATABASE_FILE);
            }
            else
            {
                connection = new SQLiteConnection(DATABASE_FILE);
                connection.CreateTable<Recipe>();
                connection.CreateTable<Instruction>();
                connection.CreateTable<Ingredient>();
                connection.Commit();
            }
        }

        public static void CloseDB()
        {
            connection.Close();
        }
    }
}
