using System;
using System.Windows;
using System.Windows.Data;

namespace StudySkills.UI.Core.Converters
{
    /// <summary>
    /// Converts SelectedIndex to Visible if something is selected, to Hidden otherwise.
    /// </summary>
    public class SelectedIndexToVisibility : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (value is int)
            {
                if((int)value >= 0)
                    return Visibility.Visible;
            }
            return Visibility.Hidden;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotSupportedException();
        }
    }
}
