namespace Dolby.UAP.Converters
{
    using System;
    using Windows.UI.Text;
    using Windows.UI.Xaml;
    using Windows.UI.Xaml.Data;

    public class PageTypeToVisibilityConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            int buttonType = 0;
            int.TryParse(parameter.ToString(), out buttonType);
            return (int)value == buttonType ? Visibility.Visible : Visibility.Collapsed;
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }

    public class PageTypeToOpacityConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            int buttonType = 0;
            int.TryParse(parameter.ToString(), out buttonType);
            return (int)value == buttonType ? 1 : 0.5;
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }

    public class PageTypeToFontWeightConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            int buttonType = 0;
            int.TryParse(parameter.ToString(), out buttonType);
            return (int)value == buttonType ? FontWeights.Medium : FontWeights.Light;
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }
}
