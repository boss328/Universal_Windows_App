namespace Dolby.UAP.Converters
{
    using System;
    using Windows.UI.Xaml.Data;

    public class IndexToTitleBackgroundConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            int index = 0;
            int.TryParse(value.ToString(), out index);
            return index % 2 == 0 ? App.Current.Resources["BrandingLightBlue"] : App.Current.Resources["BrandingDarkBlue"];
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }

    public class IndexToDescriptionBackgroundConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            int index = 0;
            int.TryParse(value.ToString(), out index);
            return index % 2 == 0 ? App.Current.Resources["BrandingLightBackground"] : App.Current.Resources["BrandingDarkBackground"];
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }
}
