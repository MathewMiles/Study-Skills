using Caliburn.Micro;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace StudySkills.UI.Views.PopUps
{
    public class MessageModalViewModel : Screen, INotifyPropertyChanged
    {
        private string _header, _message;

        override public event PropertyChangedEventHandler PropertyChanged;

        public MessageModalViewModel(string header, string message)
        {
            this.Parent = App.Current.MainWindow;
            Header = header;
            Message = message;
        }

        public string Header
        {
            get { return _header; }
            set
            {
                _header = value;
                NotifyPropertyChanged();
            }
        }

        public string Message
        {
            get { return _message; }
            set
            {
                _message = value;
                NotifyPropertyChanged();
            }
        }

        private void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        /// <summary>
        /// Closes message window
        /// </summary>
        public void Dismiss()
        {
            this.TryClose();
        }
    }
}
