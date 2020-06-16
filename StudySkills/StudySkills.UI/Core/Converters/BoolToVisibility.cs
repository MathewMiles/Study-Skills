using System;
using System.Windows;
using System.Windows.Data;

namespace StudySkills.UI.Core.Converters
{
    /// <summary>
    /// Converts true to Visible and false to Hidden.
    /// </summary>
    public class BoolToVisibility : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if(value is bool)
            {
                if ((bool)value)
                    return Visibility.Visible;
            }
            return Visibility.Hidden;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
