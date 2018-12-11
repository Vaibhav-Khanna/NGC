using System;
using System.Globalization;
using Xamarin.Forms;

namespace NGC.Converter
{
    public class DateToStringConverter : IValueConverter
    {

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var date = (DateTime?)value;

            if (date != null)
            {
                return date.Value.ToString("dddd, dd MMMM", CultureInfo.CurrentCulture);
            }

            return null;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
