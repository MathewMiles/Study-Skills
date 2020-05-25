using Caliburn.Micro;
using StudySkills.UI.Core.Events;

namespace StudySkills.UI.Views.PopUps
{
    public class NewStudySetModalViewModel : Screen
    {
        private string _studySetName;
        private readonly IEventAggregator _eventAggregator;

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
                NotifyOfPropertyChange(() => StudySetName);
            }
        }

        public void Create()
        {
            _eventAggregator.PublishOnUIThread(new CreateStudySetEvent { Name = StudySetName });
            Cancel();
        }

        public void Cancel()
        {
            this.TryClose();
            App.Current.MainWindow.Effect = null;
        }
    }
}
