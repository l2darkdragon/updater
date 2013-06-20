using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace Updater.Converters
{
    public class IntToBooleanConverter : IValueConverter
    {
        /// <summary>
        /// Converts int != 0 to true and int == 0 to false. If the parameter contains "Not", "!",
        /// or "Invert", the result is inverted before converting to bool.
        /// </summary>
        /// <param name="value"></param>
        /// <param name="targetType"></param>
        /// <param name="parameter"></param>
        /// <param name="culture"></param>
        /// <returns></returns>
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            int val = (int)value;
            bool negated = false;

            if (parameter != null)
            {
                string s = (string)parameter;
                negated = s != null && s.Equals("!") || s.Equals("Not") || s.Equals("Invert");
            }
            
            return (val != 0) == !negated;
        }

        /// <summary>
        /// Not implemented.
        /// </summary>
        /// <param name="value"></param>
        /// <param name="targetType"></param>
        /// <param name="parameter"></param>
        /// <param name="culture"></param>
        /// <returns></returns>
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
