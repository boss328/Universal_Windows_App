namespace Dolby.UAP.Converters
{
    using Dolby.UAP.Models;
    using System;
    using Windows.UI.Xaml;
    using Windows.UI.Xaml.Data;

    public class DolbyFormatToVisibilityConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            return (DolbyFormat)value == DolbyFormat.Enabled ? Visibility.Visible : Visibility.Collapsed;

        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }

    public class DolbyFormatToReverseVisibilityConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            return (DolbyFormat)value == DolbyFormat.Enabled ? Visibility.Collapsed : Visibility.Visible;
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }
}