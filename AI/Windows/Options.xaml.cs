using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace AI
{
    /// <summary>
    /// Interaction logic for Options.xaml
    /// </summary>
    public partial class Options : Window
    {
        #region Constructor

        /// <summary>
        /// Default Constructor
        /// </summary>
        public Options()
        {
            InitializeComponent();
            // Binds the View Model to the Window
            DataContext = new WBinding(this);
        }

        #endregion


        #region Binding
        private class WBinding : ObservableClass
        {
            #region Private

            // When any of the Options is changed it will turn True
            private bool IsEdited = false;

            // THIS Window
            private Window ThisWindow;

            #endregion


            #region Public



            #endregion


            #region Constructor

            /// <summary>
            /// Default Constructor
            /// </summary>
            /// <param name="w">THIS Window</param>
            public WBinding(Window w)
            {
                ThisWindow = w;
            }

            #endregion


            #region Helpers
            #endregion // Helpers


            #region Commands

            /// <summary>
            /// Closes the Window ignoring all changes
            /// </summary>
            public Command ButtonCancelCommand { get { return new Command(() => { ThisWindow.Close(); }); } }

            /// <summary>
            /// Defaults all the options, not considering user changes
            /// </summary>
            public Command ButtonDefaultCommand { get { return new Command(() => {  }, () => { return IsEdited; }); } }

            /// <summary>
            /// Saves the Options without closing the Window
            /// </summary>
            public Command ButtonApplyCommand { get { return new Command(() => { }, () => { return IsEdited; }); } }

            #endregion // Commands
        }
        #endregion // Binding
    }
}
