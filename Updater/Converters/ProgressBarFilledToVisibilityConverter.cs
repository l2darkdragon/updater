using System;
using System.Globalization;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

namespace Updater.Converters
{
    public class ProgressBarFilledToVisibilityConverter : IValueConverter
    {
        /// <summary>
        /// Converts the progress bar value to visibility. Returns Collapsed if the progress bar is
        /// fully filled (Value equals Maximum) or unfilled (Value equals Minimum), Visible
        /// otherwise.
        /// </summary>
        /// <param name="value">progress bar instance</param>
        /// <param name="targetType"></param>
        /// <param name="parameter"></param>
        /// <param name="culture"></param>
        /// <returns></returns>
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            ProgressBar bar = (ProgressBar)value;
            return bar.Value == bar.Maximum || bar.Value == bar.Minimum ? Visibility.Collapsed : Visibility.Visible;
        }

        /// <summary>
        /// Not supported.
        /// </summary>
        /// <param name="value"></param>
        /// <param name="targetType"></param>
        /// <param name="parameter"></param>
        /// <param name="culture"></param>
        /// <returns></returns>
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotSupportedException();
        }
    }
}
