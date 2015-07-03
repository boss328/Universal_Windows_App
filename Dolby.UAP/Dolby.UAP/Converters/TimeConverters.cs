namespace Dolby.UAP.Converters
{
    using System;
    using Windows.UI.Xaml.Data;

    public class PositionToStringConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            DateTime position = DateTime.Parse(value.ToString());

            var x = "";
            if (position.Hour > 0)
                x = position.ToString("HH:mm:ss");
            else
                x = position.ToString("mm:ss");
            return x;
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }

    public class TimeSpanToProgressConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            TimeSpan timespan = TimeSpan.Parse(value.ToString());
            return timespan.TotalSeconds;
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }
}
