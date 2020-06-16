using Caliburn.Micro;
using StudySkills.UI.Core.Classes;
using StudySkills.UI.Core.Events;
using StudySkills.UI.Core.Models;
using StudySkills.UI.Views.Activities;
using System.Windows;

namespace StudySkills.UI.Views
{
    public class ShellViewModel : Conductor<object>, IHandle<GoBackEvent>, IHandle<SwitchToActivityEvent>
    {
        #region Instance Variables
        private readonly IEventAggregator _eventAggregator;
        private readonly IStudySetManager _studySetManager;
        private StudySetViewModel _studySetVM;
        private FlashcardsViewModel _flashcardsVM;
        private MatchViewModel _matchVM;
        #endregion

        public ShellViewModel(
            StudySetViewModel studySetVM,
            FlashcardsViewModel flashcardsVM,
            MatchViewModel matchVM,
            IEventAggregator eventAggregator,
            IStudySetManager studySetManager)
        {
            _eventAggregator = eventAggregator;
            _studySetManager = studySetManager;
            _studySetVM = studySetVM;
            _flashcardsVM = flashcardsVM;
            _matchVM = matchVM;

            _eventAggregator.Subscribe(this);
            ActivateItem(_studySetVM);
        }

        #region Actions
        public void Close(object sender)
        {
            ((Window)sender).Close();
        }

        public void DragWindow(object sender)
        {
            ((Window)sender).DragMove(); 
        }

        public void Minimize(object sender)
        {
            ((Window)sender).WindowState = WindowState.Minimized;
        }

        public void NotifyOfClosing()
        {
            _eventAggregator.PublishOnUIThread(new AppClosingEvent());
        }
        #endregion

        #region Event Handlers
        public void Handle(GoBackEvent message)
        {
            ActivateItem(_studySetVM);
        }

        public void Handle(SwitchToActivityEvent message)
        {
            switch (message.NewActivity)
            {
                case Activity.Flashcards:
                    ActivateItem(_flashcardsVM);
                    break;
                case Activity.Match:
                    ActivateItem(_matchVM);
                    break;
            }
        }
        #endregion
    }
}
