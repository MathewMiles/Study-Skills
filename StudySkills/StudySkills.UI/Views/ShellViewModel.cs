using Caliburn.Micro;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudySkills.UI.Views
{
    public class ShellViewModel : Conductor<object>
    {
        private IEventAggregator _eventAggregator;
        private StudySetViewModel _studySetVM;

        public ShellViewModel(
            StudySetViewModel studySetVM,
            IEventAggregator eventAggregator)
        {
            _eventAggregator = eventAggregator;
            _studySetVM = studySetVM;
            ActivateItem(_studySetVM);
        }
    }
}
