using System;
using System.ComponentModel;

namespace StudySkills.UI.Core.Models
{
    public class StudySet : INotifyPropertyChanged
    {
        private string _name;
        private int _terms;
        private Guid _fileName;

        #region Properties
        public string Name
        {
            get { return _name; }
            set
            {
                if (_name != value)
                {
                    _name = value;
                    OnPropertyChanged("Name");
                }
            }
        }

        public int Terms
        {
            get { return _terms; }
            set
            {
                if (_terms != value)
                {
                    _terms = value;
                    OnPropertyChanged("Terms");
                }
            }
        }

        public Guid FileName
        {
            get { return _fileName; }
            set
            {
                if (_fileName != value)
                {
                    _fileName = value;
                    OnPropertyChanged("FileName");
                }
            }
        }
        #endregion

        public event PropertyChangedEventHandler PropertyChanged;

        public void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
