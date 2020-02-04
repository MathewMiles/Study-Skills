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
        private readonly IEventAggregator _eventAggregator;
        private readonly IFileManager _fileManager;
        private StudySetViewModel _studySetVM;
        private FlashcardsViewModel _flashcardsVM;

        public ShellViewModel(
            StudySetViewModel studySetVM,
            FlashcardsViewModel flashcardsVM,
            IEventAggregator eventAggregator,
            IFileManager fileManager)
        {
            _eventAggregator = eventAggregator;
            _fileManager = fileManager;
            _studySetVM = studySetVM;
            _flashcardsVM = flashcardsVM;

            _eventAggregator.Subscribe(this);
            ActivateItem(_studySetVM);
        }

        public void DragWindow(object sender)
        {
            ((Window)sender).DragMove(); 
        }

        public void Minimize(object sender)
        {
            ((Window)sender).WindowState = WindowState.Minimized;
        }

        public void Close(object sender)
        {
            ((Window)sender).Close();
        }

        public void NotifyOfClosing()
        {
            _eventAggregator.PublishOnUIThread(new AppClosingEvent());
        }

        public void Handle(GoBackEvent message)
        {
            ActivateItem(_studySetVM);
        }

        public void Handle(SwitchToActivityEvent message)
        {
            switch (message.NewActivity)
            {
                case Activity.Flashcards:
                    _flashcardsVM.Terms = _fileManager.GetTerms();
                    ActivateItem(_flashcardsVM);
                    break;
            }
        }
    }
}
