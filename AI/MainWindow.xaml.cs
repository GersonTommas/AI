// using System;
// using System.Collections.Generic;
// using System.Linq;
// using System.Text;
using System.Windows;
// using System.Windows.Controls;
// using System.Windows.Data;
// using System.Windows.Documents;
using System.Windows.Input;
// using System.Windows.Media;
// using System.Windows.Media.Imaging;
// using System.Windows.Navigation;
// using System.Windows.Shapes;

namespace AI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            WindowImageViewer WIV = new WindowImageViewer(); WIV.Show(); this.Close();
        }

        /// <summary>
        /// Allows the Window to be Dragged by pressing and holding the left mouse button on any part of it
        /// </summary>
        /// <param name="sender"> Window </param>
        /// <param name="e"></param>
        private void Window_Drag(object sender, MouseButtonEventArgs e) { this.DragMove(); }
    }
}
