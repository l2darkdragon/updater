using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Media;

namespace Updater.Converters
{
    public class BooleanToColorConverter : IValueConverter
    {
        /// <summary>
        /// Converts the boolean to control visibility. Returns Visible if true, Collapsed otherwise.
        /// </summary>
        /// <param name="value"></param>
        /// <param name="targetType"></param>
        /// <param name="parameter">negation parameter</param>
        /// <param name="culture"></param>
        /// <returns></returns>
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            bool negated = false;

            if (parameter != null)
            {
                string s = (string)parameter;
                negated = s != null && s.Equals("!") || s.Equals("Not") || s.Equals("Invert");
            }

            return (bool)value == negated ? "#538ce1" : "#4c4c4c";
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
