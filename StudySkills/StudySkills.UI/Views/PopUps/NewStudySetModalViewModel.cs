using Caliburn.Micro;
using System.Windows.Media.Effects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
