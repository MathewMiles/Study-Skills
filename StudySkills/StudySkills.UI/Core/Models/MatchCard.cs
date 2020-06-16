using System.ComponentModel;

namespace StudySkills.UI.Core.Models
{
    public class MatchCard : INotifyPropertyChanged
    {
        private string _text;
        private bool _unmatched;
        private int _id;

        #region Constructors
        public MatchCard()
        {
            _text = "";
            _unmatched = true;
            _id = -1;
        }

        public MatchCard(string text, int id)
        {
            _text = text;
            _unmatched = true;
            _id = id;
        }
        #endregion

        #region Properties
        public string Text
        {
            get { return _text; }
            set
            {
                _text = value;
                OnPropertyChanged("Text");
            }
        }

        public bool Unmatched
        {
            get { return _unmatched; }
            set
            {
                _unmatched = value;
                OnPropertyChanged("Unmatched");
            }
        }

        public int Id
        {
            get { return _id; }
            set
            {
                _id = value;
                OnPropertyChanged("Id");
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
