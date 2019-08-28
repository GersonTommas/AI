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
        public Options()
        {
            InitializeComponent();
        }

        #region Binding
        private class WBinding : ObservableClass
        {
            #region Private
            #endregion // Private

            #region Public
            #endregion // Public

            #region Helpers
            #endregion // Helpers

            #region Commands
            private bool IsEdited = false;

            public Command ButtonResetCommand { get { return new Command(() => { }, () => { return IsEdited; }); } }
            public Command ButtonApplyCommand { get { return new Command(() => { }, () => { return IsEdited; }); } }
            #endregion // Commands
        }
        #endregion // Binding
    }
}
