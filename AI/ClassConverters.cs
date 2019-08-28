using System;
using System.Globalization;
//using System.IO;
using System.Windows;
using System.Windows.Data;
//using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace AI
{
    /// <summary>
    /// Converts a DirectoryItemType to a specific image type of a drive, folder or file
    /// </summary>
    [ValueConversion(typeof(DirectoryItemType), typeof(BitmapImage))]
    public class StringToImageConverter : IValueConverter
    {
        // Instance of the Converter, makes the converter ready to use
        public static StringToImageConverter Instance = new StringToImageConverter();

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return new BitmapImage(new Uri($"pack://application:,,,/Images/{value}.png"));
        }

        // ConvertBack not implemented
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) { throw new NotImplementedException(); }
    }

    /// <summary>
    /// Converts a String to a BitmapImage with DecodePixelWidth of 100 to decrease the loading time
    /// </summary>
    [ValueConversion(typeof(string), typeof(BitmapImage))]
    public class StringToBitmapImageConverter : IValueConverter
    {
        // Instance of the Converter, makes the converter ready to use
        public static StringToBitmapImageConverter Instance = new StringToBitmapImageConverter();

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            // Default Not Found Image
            var DefVal = $"pack://application:,,,/Images/NotFound.png";
            var image = new BitmapImage();

            image.BeginInit();

            // Lowers the Image resolution for faster loading
            image.DecodePixelWidth = 100;

            // Sets the Source of the Image, to the Path of the file ::: Sets the Source of the Image to the Default "Not Found" Image from the Resources of the Application
            image.UriSource = value != null ? new Uri((string)value) : new Uri(DefVal);

            image.EndInit();

            return image;
        }

        // ConvertBack not implemented
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) { throw new NotImplementedException(); }
    }

    /// <summary>
    /// Converts a ImageType to a specific image by its extension
    /// </summary>
    [ValueConversion(typeof(ImageType), typeof(BitmapImage))]
    public class ImageTypeToImageConverter : IValueConverter
    {
        // Instance of the Converter, makes the converter ready to use
        public static ImageTypeToImageConverter Instance = new ImageTypeToImageConverter();

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            ImageType IT = (ImageType)value; string val = "file";
            switch (IT)
            {
                case ImageType.Png:
                    val = "PNG";
                    break;
                case ImageType.Jpg:
                    val = "JPG";
                    break;
                case ImageType.Gif:
                    val = "GIF";
                    break;
            }
            return new BitmapImage(new Uri($"pack://application:,,,/Images/{val}.png"));
        }

        // ConvertBack not implemented
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) { throw new NotImplementedException(); }
    }


    /// <summary>
    /// Gets the image and name from the path
    /// </summary>
    [ValueConversion(typeof(string), typeof(BitmapImage))]
    public class PathToImageConverter : IValueConverter
    {
        // Instance of the Converter, makes the converter ready to use
        public static PathToImageConverter instance = new PathToImageConverter();

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            // Get the full path
            var path = (string)value;

            // If the path is null, ignore
            if (path == null)
                return null;
            
            // Default Not Found Image
            var image = $"pack://application:,,,/Images/NotFound.png";

            // If path contains nothing, show nothing
            if (string.IsNullOrEmpty(path))
                return null;
            // if the file is a PNG or JPG copy the path to the variable image
            else if (path.EndsWith(".png") || path.EndsWith(".jpg"))
                image = path;
            // If it is not a PNG or JPG return the default image
            else
                return image;

            // Convert the image's path to a BitmapImage, using the ChacheOption to cache the image at load
            return new BitmapImage(new Uri(image)).CacheOption = BitmapCacheOption.OnLoad;
        }

        // ConvertBack not implemented
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) { throw new NotImplementedException(); }
    }


    /// <summary>
    /// Turns the boolean from False to True, and from True to False, if it is not a boolean, it returns the value as it is
    /// </summary>
    [ValueConversion(typeof(bool), typeof(bool))]
    public class BoolInversionConverter : IValueConverter
    {
        // Instance of the converter, makes the converter ready to use
        public static BoolInversionConverter instance = new BoolInversionConverter();

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            try { return !(bool)value; } catch { return value; } }

        // ConvertBack not implemented
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) { throw new NotImplementedException("Not implemented."); }
    }


    /// <summary>
    /// Turns a boolean value into its Visibility Counterpart (True = Visible, False = Collapsed, Default/Not bool = Collapsed) can have the parameter "invert" to invert the polarity
    /// </summary>
    [ValueConversion(typeof(bool), typeof(Visibility))]
    public class BoolToVisibilityConverter : IValueConverter
    {
        // Instance of the converter, makes the converter ready to use
        public static BoolToVisibilityConverter instance = new BoolToVisibilityConverter();

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            try {

                bool val = (bool)value; string par = ""; if (parameter != null) { par = (string)parameter; }

                // if the value is true turn visible, if the value is false and the parameter is "invert" turn visible...
                if (val == true && par != "invert" || val == false && par == "invert")
                    return Visibility.Visible;

            } catch { }

            // if not, collapse
            return Visibility.Collapsed;
        }

        // ConvertBack not implmented
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) { throw new NotImplementedException("Not implemented."); }
    }


    /// <summary>
    /// Compares two strings (value, parameter), if they are the same, it returns Visibility.Visible, if not, Visibility.Collapsed
    /// </summary>
    [ValueConversion(typeof(string), typeof(Visibility))]
    public class StringComparerVisibilityVisibleConverter : IValueConverter
    {
        // Instance of the converter, makes the converter ready to use
        public static StringComparerVisibilityVisibleConverter instance = new StringComparerVisibilityVisibleConverter();

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture) { return (string)value == (string)parameter ? Visibility.Visible : Visibility.Collapsed; }

        // ConvertBack not implmented
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) { throw new NotImplementedException("Not Implemented."); }
    }


    /// <summary>
    /// Compares two strings (value, parameter), if they are the same, it returns Visibility.Collapsed, if not, Visibility.Visible
    /// </summary>
    [ValueConversion(typeof(string), typeof(Visibility))]
    public class StringComparerVisibilityCollapsedConverter : IValueConverter
    {
        // Instance of the converter, makes the converter ready to use
        public static StringComparerVisibilityCollapsedConverter instance = new StringComparerVisibilityCollapsedConverter();

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture) { return (string)value != (string)parameter ? Visibility.Visible : Visibility.Collapsed; }

        // ConvertBack not implemented
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) { throw new NotImplementedException("Not Implemented"); }
    }
}
