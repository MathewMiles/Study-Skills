using System;
using System.Windows.Data;

namespace StudySkills.UI.Core.Converters
{
    public class FromToDegrees : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            /*if (value is double)
            {
                if ((double)value <= 180)
                    return 180.0;
                else
                    return 0.0;
            }*/
            return 45.0;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
