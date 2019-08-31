using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace AI
{
    /// <summary>
    /// The view model for the applications main Directory view
    /// </summary>
    public class DirectoryViewModel : ObservableClass
    {
        #region Private Properties

        DirectoryItemViewModel _treeViewSelItem;
        ImageItem _ImageSelected;
        string _ComboBoxSelItem = "List";

        #endregion


        #region Public Properties

        /// <summary>
        /// A list of all directories on the machine
        /// </summary>
        public ObservableCollection<DirectoryItemViewModel> Folders { get; set; }


        /// <summary>
        /// Selected folder in the TreeView
        /// </summary>
        public DirectoryItemViewModel TreeViewSelItem
        {
            get { return _treeViewSelItem; }
            set { if (_treeViewSelItem != value) { _treeViewSelItem = value; OnPropertyChanged(nameof(TreeViewSelItem)); OnPropertyChanged(nameof(ImagesList)); } }
        }

        /// <summary>
        /// Property that stores the selected image to be displayed in the Visor
        /// </summary>
        public ImageItem ImageSelected
        {
            get { return _ImageSelected; }
            set { if (_ImageSelected != value) { _ImageSelected = value; OnPropertyChanged(nameof(ImageSelected)); } }
        }

        /// <summary>
        /// Property that stores the selected option, to show the images as List or Thumbnails. When this option changes, it will update the lists automatically using the helper
        /// </summary>
        public string ComboBoxSelItem
        {
            get { return _ComboBoxSelItem; }
            set { if (_ComboBoxSelItem != value) { _ComboBoxSelItem = value; OnPropertyChanged(nameof(ComboBoxSelItem)); OnPropertyChanged(nameof(ListBoxImagesTemplate)); } }
        }

        #endregion


        #region READONLY

        /// <summary>
        /// READONLY - List of options to show the images
        /// </summary>
        public List<string> ComboBoxItemsList { get { return new List<string>() { "List", "Thumbnails" }; } }

        /// <summary>
        /// READONLY - DataTemplate that shows an Icon and Text if "List is selected, or a little Image and Text if "Thumbnail" is selected
        /// </summary>
        public DataTemplate ListBoxImagesTemplate
        {
            get
            {
                // New Empty DataTemplate
                DataTemplate Dt = new DataTemplate();


                // New Empty Border
                var Br = new FrameworkElementFactory(typeof(Border));
                // New Empty StackPanel
                var Sp1 = new FrameworkElementFactory(typeof(StackPanel));
                // New Empty Image
                var Im1 = new FrameworkElementFactory(typeof(Image));
                // New Empty TextBlock
                var Tb1 = new FrameworkElementFactory(typeof(TextBlock));

                // Border Color Gray
                Br.SetValue(Border.BorderBrushProperty, new SolidColorBrush(Colors.Gray));

                // Alignes the TextBlock to be centered relative to the Image/Icon
                Tb1.SetValue(TextBlock.HorizontalAlignmentProperty, HorizontalAlignment.Center);
                // Sets the text of the TextBlock to be the Name of the file
                Tb1.SetBinding(TextBlock.TextProperty, new Binding("Name"));

                // This happends if the option "List" is selected
                if (ComboBoxSelItem == "List")
                {
                    // Image Properties regarding Size
                    Im1.SetValue(Image.HeightProperty, 100.0);
                    Im1.SetValue(Image.MaxWidthProperty, 100.0);
                    Im1.SetValue(Image.MarginProperty, new Thickness(15, 5, 15, 5));
                    Im1.SetBinding(Image.SourceProperty, new Binding("Type") { Converter = ImageTypeToBitmapImageConverter.Instance });
                }
                // This happens if the option "Thumbnails" is selected
                else
                {
                    // Border Properties, Thickness (Makes the Border to be visible) and Padding (Makes the Border to have an interior Margin to acomodate the Image and the Text)
                    Br.SetValue(Border.BorderThicknessProperty, new Thickness(2));
                    Br.SetValue(Border.PaddingProperty, new Thickness(5));

                    // Image Source is set to be the FullPath of the file
                    Im1.SetValue(Image.SourceProperty, new Binding("FullPath") { Converter = StringToSmallBitmapImageConverter.Instance });
                }

                // Appends to create the actual Visual Tree for the DataTemplate
                Sp1.AppendChild(Im1); Sp1.AppendChild(Tb1); Br.AppendChild(Sp1);

                // Sets the First object of the Visual Tree to be the Border, which contains the rest of the Elements created
                Dt.VisualTree = Br;
                // Type of the DataTemplate, setting its type to ItemPanelTemplate refering to ListBox.ItemTemplate
                Dt.DataType = typeof(ItemsPanelTemplate);

                return Dt;
            }
        }

        /// <summary>
        /// READONLY - List of images contained on the selected Directory of the TreeView
        /// </summary>
        public ObservableCollection<ImageItem> ImagesList
        { get { return TreeViewSelItem != null ? DirectoryStructure.GetDirectoryImages(TreeViewSelItem.FullPath) : null; } }

        /// <summary>
        /// READONLY - Turns Image Type of the selected image into its respective Icon
        /// </summary>
        public string ImageIcon
        {
            get
            {
                if (ImageSelected != null)
                {
                    switch (ImageSelected.Type)
                    {
                        case ImageType.Png:
                            return "PNG";
                        case ImageType.Jpg:
                            return "JPG";
                        case ImageType.Gif:
                            return "GIF";
                    }
                }
                return "file";
            }
        }

        #endregion


        #region Helpers

        #endregion


        #region Constructor

        /// <summary>
        /// Default constructor
        /// </summary>
        public DirectoryViewModel()
        {
            // Get the logical drives
            var children = DirectoryStructure.GetLogicalDrives();

            // Create the view models from the data
            this.Folders = new ObservableCollection<DirectoryItemViewModel>(
                children.Select(drive => new DirectoryItemViewModel(drive.FullPath, DirectoryItemType.Drive)));
        }

        #endregion
    }



    /// <summary>
    /// A view model for each directory item
    /// </summary>
    public class DirectoryItemViewModel : ObservableClass
    {
        #region Public Properties

        /// <summary>
        /// The type of this item
        /// </summary>
        public DirectoryItemType Type { get; set; }

        public string ImageName => Type == DirectoryItemType.Drive ? "drive" : (Type == DirectoryItemType.File ? "file" : (IsExpanded ? "folder-open" : "folder-closed"));

        /// <summary>
        /// The full path to the item
        /// </summary>
        public string FullPath { get; set; }

        /// <summary>
        /// The name of this directory item
        /// </summary>
        public string Name { get { return this.Type == DirectoryItemType.Drive ? this.FullPath : DirectoryStructure.GetFileFolderName(this.FullPath); } }

        /// <summary>
        /// A list of all children contained inside this item
        /// </summary>
        public ObservableCollection<DirectoryItemViewModel> Children { get; set; }

        /// <summary>
        /// Indicates if this item can be expanded
        /// </summary>
        public bool CanExpand { get { return this.Type != DirectoryItemType.File; } }

        /// <summary>
        /// Indicates if the current item is expanded or not
        /// </summary>
        public bool IsExpanded
        {
            get
            {
                return this.Children?.Count(f => f != null) > 0;
            }
            set
            {
                // If the UI tells us to expand...
                if (value == true)
                    // Find all children
                    Expand();
                // If the UI tells us to close
                else
                    this.ClearChildren();
                OnPropertyChanged("Children");
            }
        }

        #endregion


        #region Public Commands

        /// <summary>
        /// The command to expand this item
        /// </summary>
        public Command ExpandCommand { get { return new Command(() => { Expand(); }, () => { return this.Type != DirectoryItemType.File; }); } }

        #endregion


        #region Constructor

        /// <summary>
        /// Default constructor
        /// </summary>
        /// <param name="fullPath">The full path of this item</param>
        /// <param name="type">The type of item</param>
        public DirectoryItemViewModel(string fullPath, DirectoryItemType type)
        {

            // Set path and type
            this.FullPath = fullPath;
            this.Type = type;

            // Setup the children as needed
            this.ClearChildren();
        }

        #endregion


        #region Helper Methods

        /// <summary>
        /// Removes all children from the list, adding a dummy item to show the expand icon if required
        /// </summary>
        private void ClearChildren()
        {
            // Clear items
            this.Children = new ObservableCollection<DirectoryItemViewModel>();

            // Show the expand arrow if we are not a file
            if (this.Type != DirectoryItemType.File)
                this.Children.Add(null);
        }

        /// <summary>
        ///  Expands this directory and finds all children
        /// </summary>
        private void Expand()
        {
            
            // We cannot expand a file
            if (this.Type == DirectoryItemType.File)
                return;
                

            // Find all children
            var children = DirectoryStructure.GetDirectoryContents(this.FullPath);
            this.Children = new ObservableCollection<DirectoryItemViewModel>(
                                children.Select(content => new DirectoryItemViewModel(content.FullPath, content.Type)));
        }

        #endregion
    }
}