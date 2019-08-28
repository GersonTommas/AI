using System;
using System.Data.Linq.Mapping;
using System.Data.SQLite;
using System.IO;

namespace AI
{
    #region Dabase
    [Database(Name = "MyDataBase")]
    class DataBase
    {
        #region Public Access

        /// <summary>
        /// Create tables if they do not exist, and cast database into the data context
        /// </summary>
        public static void DBConnect() { CreateTables(); DBData = new DBFoldersDataContext(Conect()); }

        /// <summary>
        /// This is used to access the Database
        /// </summary>
        public static DBFoldersDataContext DBData;

        #endregion

        #region Private
        // Database connection
        private static SQLiteConnection Conect() { return new SQLiteConnection("Data Source=" + Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\EMMA\\EmmaDB.db;foreign keys=true;"); }

        // Tries to create tables if do not exist
        private static void CreateTables()
        {
            HelperCreateTable("CREATE TABLE IF NOT EXISTS [DBFavoriteFolderTable] ([Id] INTEGER PRIMARY KEY AUTOINCREMENT, [FullPath] TEXT NOT NULL, [Notes] TEXT");
            //HelperCreateTable("CREATE TABLE IF NOT EXISTS [DBHiddenFolderTable] ([Id] INTEGER PRIMARY KEY AUTOINCREMENT, [FullPath] TEXT NOT NULL, [Notes] TEXT");
        }

        #endregion


        #region Helpers
        // Helper to create tables
        private static void HelperCreateTable(string _STRtable)
        {
            Directory.CreateDirectory(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\EMMA\\");
            using (var sqldb = Conect())
            {
                sqldb.Open();
                var commandTable01 = new SQLiteCommand(_STRtable, sqldb);
                commandTable01.ExecuteNonQuery();
                sqldb.Close();
            }
        }

        #endregion
    }

    #endregion
}
