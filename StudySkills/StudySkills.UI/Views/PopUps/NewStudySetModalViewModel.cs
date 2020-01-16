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
        private bool _hasDefaultValue = true;
        private readonly IEventAggregator _eventAggregator;

        public NewStudySetModalViewModel(
            IEventAggregator eventAggregator)
        {
            _eventAggregator = eventAggregator;
            this.Parent = App.Current.MainWindow;
            StudySetName = "Name";
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

        public void StudySetName_GotFocus()
        {
            if (_hasDefaultValue)
            {
                StudySetName = "";
            }
        }

        public void StudySetName_LostFocus()
        {
            if (StudySetName == "")
            {
                StudySetName = "Name";
                _hasDefaultValue = true;
            }
            else
            {
                _hasDefaultValue = false;
            }
        }
    }
}
