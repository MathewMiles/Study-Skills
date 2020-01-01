using Caliburn.Micro;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudySkills.UI.Views
{
    public class StudySetViewModel : Screen
    {
        private IEventAggregator _eventAggregator;

        public StudySetViewModel(
            IEventAggregator eventAggregator)
        {
            _eventAggregator = eventAggregator;
        }
    }
}
