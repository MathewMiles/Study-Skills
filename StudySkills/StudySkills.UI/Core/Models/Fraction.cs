using System.ComponentModel;

namespace StudySkills.UI.Core.Models
{
    public class Fraction : INotifyPropertyChanged
    {
        private int _numerator = 1, _denominator = 1;
        private double _decimalValue;

        public Fraction(int num, int den)
        {
            Numerator = num;
            Denominator = den;
        }

        #region Properties
        public int Numerator
        {
            get { return _numerator; }
            set
            {
                _numerator = value;
                DecimalValue = _numerator / Denominator / 1.0;
                OnPropertyChanged("Numerator");
            }
        }

        public int Denominator
        {
            get { return _denominator; }
            set
            {
                _denominator = value;
                DecimalValue = Numerator / _denominator / 1.0;
                OnPropertyChanged("Denominator");
            }
        }

        public double DecimalValue
        {
            get { return _decimalValue; }
            private set
            {
                _decimalValue = value;
                OnPropertyChanged("DecimalValue");
            }
        }
        #endregion

        public event PropertyChangedEventHandler PropertyChanged;

        public void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }

        public override string ToString()
        {
            return $"{Numerator} / {Denominator}";
        }
    }
}
