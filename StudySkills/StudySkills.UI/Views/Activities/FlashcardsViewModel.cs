using Caliburn.Micro;
using StudySkills.UI.Core.Classes;
using StudySkills.UI.Core.Events;

namespace StudySkills.UI.Views.Activities
{
    public class FlashcardsViewModel : Screen
    {
        private readonly IEventAggregator _eventAggregator;
        private readonly IFileManager _fileManager;



        public FlashcardsViewModel(
            IEventAggregator eventAggregator,
            IFileManager fileManager)
        {
            _eventAggregator = eventAggregator;
            _fileManager = fileManager;
        }

        public void GoBack()
        {
            _eventAggregator.PublishOnUIThread(new GoBackEvent());
        }
    }
}
