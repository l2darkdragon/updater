using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace Updater.Converters
{
    public class ValueToBooleanConverter : IValueConverter
    {
        /// <summary>
        /// Converts the progress bar value to boolean to indicate whether the progress bar is filled or not.
        /// </summary>
        /// <param name="value">current value</param>
        /// <param name="targetType"></param>
        /// <param name="parameter">maximum value</param>
        /// <param name="culture"></param>
        /// <returns></returns>
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            double val = (double)value;
            double max = parameter is String ? Double.Parse((string)parameter) : (double)parameter;
            return val == max;
        }

        /// <summary>
        /// Converts the progress bar fill indicator to value. Returns maximum value if <paramref name="value"/>
        /// parameter is true, zero otherwise.
        /// </summary>
        /// <param name="value"></param>
        /// <param name="targetType"></param>
        /// <param name="parameter">maximum value</param>
        /// <param name="culture"></param>
        /// <returns></returns>
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            bool val = (bool)value;
            double max = parameter is String ? Double.Parse((string)parameter) : (double)parameter;
            return val ? max : 0;
        }
    }
}
