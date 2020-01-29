using Caliburn.Micro;
using StudySkills.UI.Core.Events;
using StudySkills.UI.Core.Models;
using StudySkills.UI.Views.Activities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace StudySkills.UI.Views
{
    public class ShellViewModel : Conductor<object>, IHandle<GoBackEvent>, IHandle<SwitchToActivityEvent>
    {
        private readonly IEventAggregator _eventAggregator;
        private StudySetViewModel _studySetVM;
        private FlashcardsViewModel _flashcardsVM;

        public ShellViewModel(
            StudySetViewModel studySetVM,
            FlashcardsViewModel flashcardsVM,
            IEventAggregator eventAggregator)
        {
            _eventAggregator = eventAggregator;
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
                    ActivateItem(_flashcardsVM);
                    break;
            }
        }
    }
}
