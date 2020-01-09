using Caliburn.Micro;
using StudySkills.UI.Core.Events;
using StudySkills.UI.Core.Models;
using StudySkills.UI.Views.PopUps;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudySkills.UI.Views
{
    public class StudySetViewModel : Screen, IHandle<CreateStudySetEvent>
    {
        private readonly IEventAggregator _eventAggregator;
        private readonly IWindowManager _windowManager;
        private List<StudySet> _studySets = new List<StudySet>();
        private List<TermDefinitionPair> _terms = new List<TermDefinitionPair>();

        public StudySetViewModel(
            IEventAggregator eventAggregator,
            IWindowManager windowManager)
        {
            _eventAggregator = eventAggregator;
            _windowManager = windowManager;
            Test();
        }

        public List<StudySet> StudySets => _studySets;
        public List<TermDefinitionPair> Terms => _terms;

        private void CreateStudySet(string name)
        {
            StudySets.Add(new StudySet()
            {
                Name = name,
                Terms = 0
            });
        }

        public void OpenCreateStudySetModal()
        {
            _windowManager.ShowDialog(new NewStudySetModalViewModel());
        }

        public void Handle(CreateStudySetEvent message)
        {
            CreateStudySet(message.Name);
        }

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
            for (int c = 1; c < 9; c++)
            {
                Terms.Add(new TermDefinitionPair()
                {
                    Term = $"Term {c} longbitattheendlol",
                    Definition = $"Definition {c}"
                });
            }
        }
    }
}
