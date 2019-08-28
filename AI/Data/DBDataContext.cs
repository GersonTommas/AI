using System.Data.Linq.Mapping;
using System.Linq;

namespace AI
{
    #region Folders Data Context

    /// <summary>
    /// Data Context for the Folders Tables of the Database
    /// </summary>
    public partial class DBFoldersDataContext : System.Data.Linq.DataContext
    {
        #region On Created

        // Adds the Mapping Source (LINQ)
        private static MappingSource mappingSource = new AttributeMappingSource();

        // Default On Created Void
        partial void OnCreated();

        /// <summary>
        /// This connects to the actual Database
        /// </summary>
        /// <param name="connection"> SQLiteConnection </param>
        public DBFoldersDataContext(System.Data.IDbConnection connection) : base(connection, mappingSource) { OnCreated(); }

        #endregion


        #region Tables

        /// <summary>
        /// Returns the full FavoriteFolderTable content as a System.Data.Linq.Table<DBFavoriteFolderTable>
        /// </summary>
        public System.Data.Linq.Table<DBFavoriteFolderTable> FavoriteFoldersTable { get { return GetTable<DBFavoriteFolderTable>(); } }

        #endregion


        #region CRUD Operations
        public DBFavoriteFolderTable GlobalNewFavoriteFolder { get { return new DBFavoriteFolderTable() { Id = FindMaxFavoriteFoldersID() + 1 }; } }


        /// <summary>
        /// CRUD operation to create an item in FavoriteFolderTable
        /// </summary>
        /// <param name="fullPath"> Full Path of the folder </param>
        /// <param name="notes"> Notes, default null </param>
        public static void CreateFavoriteFolder(string fullPath, string notes = "") { }
        /// <summary>
        /// CRUD operation to create an item in FavoriteFolderTable
        /// </summary>
        /// <param name="folder"> Complete Item to create </param>
        public static void CreateFavoriteFolder(DBFavoriteFolderTable folder) { }


        /// <summary>
        /// CRUD operation that returns an item
        /// </summary>
        /// <param name="id"> Id of the item to look for </param>
        public static DBFavoriteFolderTable ReadFavoriteFolder(int id) { return new DBFavoriteFolderTable(); }


        /// <summary>
        /// CRUD operation to update an item in FavoriteFolderTable
        /// </summary>
        /// <param name="id"> Id of the item to Update </param>
        /// <param name="notes"> Information to Update </param>
        public static void UpdateFavoriteFolder(int id, string notes) { }
        /// <summary>
        /// CRUD operation to update an item in FavoriteFolderTable
        /// </summary>
        /// <param name="folder"> Complete Item to update </param>
        public static void UpdateFavoriteFolder(DBFavoriteFolderTable folder) { }


        /// <summary>
        /// CRUD operation to delete an item from FavoriteFolderTable
        /// </summary>
        /// <param name="id"> Id of the item to delete </param>
        public static void DeleteFavoriteFolder(int id) { HelperDeleteFavoriteFolder(id); }
        /// <summary>
        /// CRUD operation to delete an item from FavoriteFolderTable
        /// </summary>
        /// <param name="folder"> Complete Item to delete </param>
        public static void DelefeFavoriteFolder(DBFavoriteFolderTable folder) { HelperDeleteFavoriteFolder(folder.Id); }

        #endregion


        #region CRUD Helpers

        // Finds Last used Id number
        private int FindMaxFavoriteFoldersID() { try { return FavoriteFoldersTable.Select(x => x.Id).Max(); } catch { return 0; } }

        // Deletes the FavoriteFolder Item containing the corresponding ID
        private static void HelperDeleteFavoriteFolder(int id)
        {

        }

        #endregion
    }

    #endregion
}
