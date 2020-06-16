using System;
using System.Windows.Media.Media3D;
using System.Windows.Data;

namespace StudySkills.UI.Core.Converters
{
    /// <summary>
    /// Converts Quaternion from 0 to 180 degress and vice versa.
    /// </summary>
    public class QuaternionFlip : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (value is Quaternion)
            {
                if (((Quaternion)value).Angle == 0)
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
