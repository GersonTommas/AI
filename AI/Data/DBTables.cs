using System.Data.Linq.Mapping;

namespace AI
{
    #region Folders

    #region DBFavoriteFolderTable

    [Table(Name = nameof(DBFavoriteFolderTable))]
    public class DBFavoriteFolderTable : ObservableClass
    {
        int _id; string _fullPath, _notes;

        /// <summary>
        /// Id (Autoincrements)
        /// </summary>
        [Column(Storage = nameof(_id))]
        public int Id
        {
            get { return _id; }
            set { if (_id != value) { _id = value; OnPropertyChanged(nameof(Id)); } }
        }

        /// <summary>
        /// Full path of the favorited folder
        /// </summary>
        [Column(Storage = nameof(_fullPath))]
        public string FullPath
        {
            get { return _fullPath; }
            set { if (_fullPath != value) { _fullPath = value; OnPropertyChanged(nameof(FullPath)); } }
        }

        /// <summary>
        /// Notes
        /// </summary>
        [Column(Storage = nameof(_notes))]
        public string Notes
        {
            get { return _notes; }
            set { if (_notes != value) { _notes = value; OnPropertyChanged(nameof(Notes)); } }
        }
    }

    #endregion


    #region DBHiddenFolderTable

    [Table(Name = nameof(DBHiddenFolderTable))]
    public class DBHiddenFolderTable : ObservableClass
    {
        int _id; string _fullPath, _notes;

        /// <summary>
        /// Id (Autoincrements)
        /// </summary>
        [Column(Storage = nameof(_id))]
        public int Id
        {
            get { return _id; }
            set { if (_id != value) { _id = value; OnPropertyChanged(nameof(Id)); } }
        }

        /// <summary>
        /// Full path of the favorited folder
        /// </summary>
        [Column(Storage = nameof(_fullPath))]
        public string FullPath
        {
            get { return _fullPath; }
            set { if (_fullPath != value) { _fullPath = value; OnPropertyChanged(nameof(FullPath)); } }
        }

        /// <summary>
        /// Notes
        /// </summary>
        [Column(Storage = nameof(_notes))]
        public string Notes
        {
            get { return _notes; }
            set { if (_notes != value) { _notes = value; OnPropertyChanged(nameof(Notes)); } }
        }
    }

    #endregion

    #endregion
}