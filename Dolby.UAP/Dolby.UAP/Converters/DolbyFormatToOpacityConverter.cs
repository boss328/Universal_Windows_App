namespace Dolby.UAP.Converters
{
    using Dolby.UAP.Models;
    using System;
    using Windows.UI.Xaml.Data;

    public class DolbyFormatToOpacityConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            return (DolbyFormat)value == DolbyFormat.Enabled ? 1 : 0.5;
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }

    public class DolbyFormatToReverseOpacityConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            return (DolbyFormat)value == DolbyFormat.Enabled ? 0.5 : 1;
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }
}