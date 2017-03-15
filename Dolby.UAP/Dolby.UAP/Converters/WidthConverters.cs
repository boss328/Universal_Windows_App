namespace Dolby.UAP.Converters
{
    using System;
    using Windows.UI.Xaml.Data;

    public class BoolToToggleIconWidthConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            return (bool)value == true ? 30 : 10;
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }

    public class ParentWidthToChildWidthConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            int parentWidth = 0,
                itemsNumber = 0;

            return (int.TryParse(value.ToString(), out parentWidth) && int.TryParse(parameter.ToString(), out itemsNumber)) ? parentWidth / itemsNumber : 0;
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }
    public class ParentWidthToChildHeigthConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            int parentWidth = 0,
                itemsNumber = 0;

            return (int.TryParse(value.ToString(), out parentWidth) && int.TryParse(parameter.ToString(), out itemsNumber)) ? ((parentWidth / itemsNumber) / 1.78) + 125 : 0;
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }
}
