using System;

using System.Windows;
using System.ComponentModel;
using System.Windows.Input;
using System.Windows.Data;
using System.Globalization;

namespace AI
{
    #region Shared

    class Shared
    {
        #region Variables

        // Language for the UI
        public static string Lang_ => Properties.Settings.Default.AllWindowLanguage;

        #endregion


        #region Feeds

        /// <summary>
        /// NOT IMPLEMENTED
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        public System.ServiceModel.Syndication.SyndicationFeed feed(string url)
        {
            System.ServiceModel.Syndication.SyndicationFeed returnFeed;

            using (System.Xml.XmlReader reader = System.Xml.XmlReader.Create(url)) { returnFeed = System.ServiceModel.Syndication.SyndicationFeed.Load(reader); }

            return returnFeed;
        }

        #endregion

    }

    #endregion


    #region Command
    
    /// <summary>
    /// Implementation to use commands on MVVM "Public Command _Name_ { get { new Command (() => { ... Do stuf ... }, () => { return true/false }); } }
    /// </summary>
    public class Command : ICommand
    {
        #region Delegates

        public delegate void ICommandOnExecute();
        public delegate bool ICommandOnCanExecute();

        #endregion


        #region Private

        private ICommandOnExecute _execute;
        private ICommandOnCanExecute _canExecute;

        #endregion


        #region Constructor

        /// <summary>
        /// Default Constructor
        /// </summary>
        /// <param name="onExecuteMethod">Where the command do stuf</param>
        /// <param name="onCanExecuteMethod">Returns True/False if it Can/Can't do stuf</param>
        public Command(ICommandOnExecute onExecuteMethod, ICommandOnCanExecute onCanExecuteMethod = null)
        { _execute = onExecuteMethod; _canExecute = onCanExecuteMethod; }

        #endregion


        #region ICommand Members
        public event EventHandler CanExecuteChanged
        { add { CommandManager.RequerySuggested += value; } remove { CommandManager.RequerySuggested -= value; } }

        public bool CanExecute(object parameter) { return _canExecute?.Invoke() ?? true; }
        public void Execute(object parameter) { _execute?.Invoke(); }
        #endregion
    }

    #endregion


    #region Property Changed

    /// <summary>
    /// A base view model that fires Property Changed events as needed
    /// </summary>
    public abstract class ObservableClass : INotifyPropertyChanged
    {
        /// <summary>
        /// The event that is fired when any child property changes its value
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged = (sender, e) => { };


        /// <summary>
        /// Call this to fire <see cref="PropertyChanged"/> event
        /// </summary>
        /// <param name="name">Name of the property to be Updated as string</param>
        protected void OnPropertyChanged(string name) { PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name)); }
    }

    #endregion


    #region Converters

    public class BoolToVisInvertConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture) { try { if ((bool)value == true) { return Visibility.Collapsed; } else { return Visibility.Visible; } } catch { return Visibility.Visible; } }
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) { throw new NotImplementedException("Not implemented."); }
    }
    /*
    public class StringOrderConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            switch (value)
            {
                case "Inserted": return Properties.Resources.OrderOption1;
                case "Description": return Properties.Resources.OrderOption2;
                case "Ticket": return Properties.Resources.OrderOption3;
                case "Date": return Properties.Resources.OrderOption4;
            }
            return null;
        }
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            switch (value)
            {
                case "Orden de ingreso": case "As created": case "Ordem de entrada": return "Inserted";
                case "Descripción": case "Description": case "Descrição": return "Description";
                case "Número de Ticket": case "Ticket number": case "Número do Ticket": return "Ticket";
                case "Fecha / Hora": case "Date / Time": case "Data / Hora": return "Date";
            }
            return null;
        }
    }
    */

    public class StringLangConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        { switch (value) { case "en": return "English"; case "es": return "Español"; case "pt": return "Português"; } return "English"; }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        { switch (value) { case "English": return "en"; case "Español": return "es"; case "Português": return "pt"; } return "en"; }
    }
    #endregion // Converters
}