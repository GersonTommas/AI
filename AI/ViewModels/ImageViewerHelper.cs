using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;

namespace AI
{
    /// <summary>
    /// A helper class to query information about directories
    /// </summary>
    public static class DirectoryStructure
    {
        /// <summary>
        /// Gets all logical drives on the computer
        /// </summary>
        /// <returns>A list of All the logical drives on the machine</returns>
        public static List<DirectoryItem> GetLogicalDrives()
        {
            // Get every logical drive on the machine
            return Directory.GetLogicalDrives().Select(drive => new DirectoryItem { FullPath = drive, Type = DirectoryItemType.Drive }).ToList();
        }

        /// <summary>
        /// Gets the directories top-level content
        /// </summary>
        /// <param name="fullPath">The full path to the directory</param>
        /// <returns>A list of all the directories iside the "fullPath" direcrory</returns>
        public static List<DirectoryItem> GetDirectoryContents(string fullPath)
        {
            // Create empty list
            var items = new List<DirectoryItem>();

            #region Get Folders

            // Try and get directories from the folder
            // ignoring any issues doing so
            try
            {
                var dirs = Directory.GetDirectories(fullPath);

                if (dirs.Length > 0)
                    items.AddRange(dirs.Select(dir => new DirectoryItem { FullPath = dir, Type = DirectoryItemType.Folder }));
            }
            catch { }

            #endregion

            return items;
        }

        /// <summary>
        /// Gets the directories top-level files
        /// </summary>
        /// <param name="fullPath">The full path to the directory</param>
        /// <returns>A list of all the Files iside the "fullPath" direcrory</returns>
        public static List<DirectoryItem> GetDirectoryFiles(string fullPath)
        {
            // Create empty list
            var items = new List<DirectoryItem>();

            #region Get Files

            // Try and get files from the folder
            // ignoring any issues doing so
            try
            {
                //var fs = (string[])Directory.GetFiles(fullPath).Where(f => f.EndsWith(".png") || f.EndsWith(".jpg"));
                var fs = Directory.GetFiles(fullPath);
                if (fs.Length > 0)
                    items.AddRange(fs.Select(file => new DirectoryItem { FullPath = file, Type = DirectoryItemType.File }));
            }
            catch { }

            #endregion

            return items;
        }

        /// <summary>
        /// Gets the directories top-level images (PNG, JPG)
        /// </summary>
        /// <param name="fullPath">The full path to the directory</param>
        /// <returns>A list of all the images iside the "fullPath" direcrory</returns>
        public static ObservableCollection<ImageItem> GetDirectoryImages(string fullPath)
        {
            // Create new empty List
            var items = new ObservableCollection<ImageItem>();

            // Try to get the files from the folder
            // Ignoring any issues doing so
            try
            {
                // Get all the files on the directory
                var fs = Directory.GetFiles(fullPath);
                if (fs.Length > 0)
                {
                    foreach (var f in fs)
                    {
                        if (f.ToLower().EndsWith(".jpg") || f.ToLower().EndsWith(".png"))
                            items.Add(new ImageItem { FullPath = f });
                    }
                }
            } catch { }

            return items;
        }

        #region Helpers

        /// <summary>
        /// Find the file or folder name from a full path
        /// </summary>
        /// <param name="path">The full path</param>
        /// <returns></returns>
        public static string GetFileFolderName(string path)
        {
            // If we have no path, return empty
            if (string.IsNullOrEmpty(path))
                return string.Empty;

            // Make all slashes back slashes
            var normalizedPath = path.Replace('/', '\\');

            // Find the last backslash in the path
            var lastIndex = normalizedPath.LastIndexOf('\\');

            // If we don't find a backslash, return the path itself
            if (lastIndex <= 0)
                return path;

            // Return the name after the last back slash
            return path.Substring(lastIndex + 1);
        }

        #endregion
    }
}
