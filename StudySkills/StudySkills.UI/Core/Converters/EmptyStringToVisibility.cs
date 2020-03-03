using System;
using System.Windows;
using System.Windows.Data;

namespace StudySkills.UI.Core.Converters
{
    public class EmptyStringToVisibility : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (value is string)
                if ((string)value == "" || value == null)
                    return Visibility.Visible;
            return Visibility.Hidden;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
