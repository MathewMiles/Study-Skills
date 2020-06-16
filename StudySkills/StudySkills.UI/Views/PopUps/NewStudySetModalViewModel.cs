using Caliburn.Micro;
using StudySkills.UI.Core.Events;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace StudySkills.UI.Views.PopUps
{
    public class NewStudySetModalViewModel : Screen
    {
        private string _studySetName;
        private readonly IEventAggregator _eventAggregator;

        override public event PropertyChangedEventHandler PropertyChanged;

        public NewStudySetModalViewModel(
            IEventAggregator eventAggregator)
        {
            _eventAggregator = eventAggregator;
            this.Parent = App.Current.MainWindow;
        }

        public string StudySetName 
        {
            get { return _studySetName; }
            set
            {
                _studySetName = value;
                NotifyPropertyChanged();
            }
        }

        private void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public void Create()
        {
            _eventAggregator.PublishOnUIThread(new CreateStudySetEvent { Name = StudySetName });
            Cancel();
        }

        public void Cancel()
        {
            this.TryClose();
        }
    }
}
