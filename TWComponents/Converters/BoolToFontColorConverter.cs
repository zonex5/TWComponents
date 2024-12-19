using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Media;

namespace TwComponents.Converters
{
    public class BoolToFontColorConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is bool boolValue)
            {
                return boolValue ? Brushes.White : Brushes.Black;
            }

            throw new InvalidOperationException("The value must be a boolean.");
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotSupportedException("ConvertBack is not supported.");
        }
    }
}