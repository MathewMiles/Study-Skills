using System;
using System.Windows.Media.Media3D;
using System.Windows.Data;

namespace StudySkills.UI.Core.Converters
{
    /// <summary>
    /// Converts double to either 0 or 180 degrees to control the side of the flashcard that appears first.
    /// </summary>
    public class TermFirstToQuaternion : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (value is double)
            {
                if ((double)value == 0)
                    return new Quaternion(1, 0, 0, 0);
                else
                    return new Quaternion(0, 0, 0, 1);
            }
            return new Quaternion(0, 0, 0, 1);
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}