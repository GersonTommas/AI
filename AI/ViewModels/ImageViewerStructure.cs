namespace AI
{

    #region Directory Item

    /// <summary>
    /// Information about a directory item such as a drive, a file or a folder
    /// </summary>
    public class DirectoryItem
    {
        /// <summary>
        /// The type of this item
        /// </summary>
        public DirectoryItemType Type { get; set; }

        /// <summary>
        /// The absolute path to this item
        /// </summary>
        public string FullPath { get; set; }

        /// <summary>
        /// READONLY - Gets the name of this directory item
        /// </summary>
        public string Name { get { return this.Type == DirectoryItemType.Drive ? this.FullPath : DirectoryStructure.GetFileFolderName(this.FullPath); } }
    }

    #endregion

    #region Directory Type

    /// <summary>
    /// The type of a directory item
    /// </summary>
    public enum DirectoryItemType
    {
        /// <summary>
        /// A logical drive
        /// </summary>
        Drive,
        /// <summary>
        /// A file
        /// </summary>
        File,
        /// <summary>
        /// A folder
        /// </summary>
        Folder
    }

    #endregion


    #region Image Item
    /// <summary>
    /// This item contains the image path, size, extension
    /// </summary>
    public class ImageItem
    {
        /// <summary>
        /// The absolute path to this image
        /// </summary>
        public string FullPath { get; set; }

        /// <summary>
        /// READONLY - Gets the image extension as a Type
        /// </summary>
        public ImageType Type
        {
            get
            {
                if (this.FullPath.ToLower().EndsWith(".jpg"))
                    return ImageType.Jpg;
                else if (this.FullPath.ToLower().EndsWith(".png"))
                    return ImageType.Png;
                else if (this.FullPath.ToLower().EndsWith(".gif"))
                    return ImageType.Gif;
                else if (this.FullPath.ToLower().EndsWith(".bmp"))
                    return ImageType.Bmp;
                
                return ImageType.Unknown;
            }
        }

        /// <summary>
        /// The image width
        /// </summary>
        public int Width { get; set; }

        /// <summary>
        /// The image height
        /// </summary>
        public int Height { get; set; }

        /// <summary>
        /// The image resolution
        /// </summary>
        public int Resolution { get; set; }

        /// <summary>
        /// READONLY - Gets the name of the Image
        /// </summary>
        public string Name { get { return DirectoryStructure.GetFileFolderName(this.FullPath); } }

    }

    #endregion


    #region Image Type
    /// <summary>
    /// The image extension
    /// </summary>
    public enum ImageType
    {
        /// <summary>
        /// Image Type BMP
        /// </summary>
        Bmp,
        /// <summary>
        /// Image Type JPG
        /// </summary>
        Jpg,
        /// <summary>
        /// Image Type GIF
        /// </summary>
        Gif,
        /// <summary>
        /// Image Type PNG
        /// </summary>
        Png,

        /// <summary>
        /// Unknown image Type
        /// </summary>
        Unknown
    }
    #endregion

}
