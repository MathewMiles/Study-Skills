using Caliburn.Micro;
using StudySkills.UI.Core.Models;
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
        private List<StudySet> _studySets = new List<StudySet>();

        public StudySetViewModel(
            IEventAggregator eventAggregator)
        {
            _eventAggregator = eventAggregator;
            Test();
        }

        public List<StudySet> StudySets => _studySets;

        private void Test()
        {
            for(int c=1; c<9; c++)
            {
                StudySets.Add(new StudySet()
                {
                    Name = $"Study Set {c} longbitattheendlol",
                    Terms = 120
                });
            }
        }
    }
}
