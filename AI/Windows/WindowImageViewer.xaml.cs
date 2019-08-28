using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;

namespace AI
{
    /// <summary>
    /// Interaction logic for WindowImageViewer.xaml
    /// </summary>
    public partial class WindowImageViewer : Window
    {
        // View model instance initialization
        DirectoryViewModel DVM = new DirectoryViewModel();
        #region Constructor

        // Default Constructor
        public WindowImageViewer()
        {
            InitializeComponent();

            // Binding the view model
            this.DataContext = DVM;
        }

        #endregion

        // When a folder is selected in the TreeView, it sets the value of "TreeviewSelItem" in the ViewModel, to the selected Item
        private void FolderView_SelectedItemChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        { DVM.TreeViewSelItem = ((TreeView)sender).SelectedItem as DirectoryItemViewModel; }
    }
}
