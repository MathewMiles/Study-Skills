using System.ComponentModel;

namespace StudySkills.UI.Core.Models
{
    public class TermDefinitionPair : INotifyPropertyChanged
    {
        private string _term;
        private string _definition;

        #region Properties
        public string Term
        {
            get { return _term; }
            set
            {
                if (_term != value)
                {
                    _term = value;
                    OnPropertyChanged("Term");
                }
            }
        }

        public string Definition
        {
            get { return _definition; }
            set
            {
                if (_definition != value)
                {
                    _definition = value;
                    OnPropertyChanged("Definition");
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
