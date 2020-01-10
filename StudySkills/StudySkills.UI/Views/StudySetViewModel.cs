using Caliburn.Micro;
using StudySkills.UI.Core.Events;
using StudySkills.UI.Core.Models;
using StudySkills.UI.Views.PopUps;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudySkills.UI.Views
{
    public class StudySetViewModel : Screen,  IHandle<CreateStudySetEvent>
    {
        private readonly IEventAggregator _eventAggregator;
        private readonly IWindowManager _windowManager;
        private ObservableCollection<StudySet> _studySets = new ObservableCollection<StudySet>();
        private ObservableCollection<TermDefinitionPair> _terms = new ObservableCollection<TermDefinitionPair>();
        private StudySet _selectedStudySet;

        public StudySetViewModel(
            IEventAggregator eventAggregator,
            IWindowManager windowManager)
        {
            _eventAggregator = eventAggregator;
            _windowManager = windowManager;

            _eventAggregator.Subscribe(this);
            Test();
        }

        #region Properties
        public ObservableCollection<StudySet> StudySets 
        {
            get { return _studySets; }
            private set 
            {
                _studySets = value;
                NotifyOfPropertyChange(() => StudySets);
            }
        }

        public StudySet SelectedStudySet
        {
            get { return _selectedStudySet; }
            set
            {
                _selectedStudySet = value;
                NotifyOfPropertyChange(() => SelectedStudySet);
            }
        }

        public ObservableCollection<TermDefinitionPair> Terms
        {
            get { return _terms; }
            private set
            {
                _terms = value;
                NotifyOfPropertyChange(() => Terms);
            }
        }
        #endregion

        #region Private Methods
        private void CreateStudySet(string name)
        {
            StudySets.Add(new StudySet()
            {
                Name = name,
                Terms = 0
            });
            SelectedStudySet = StudySets.Last();
        }
        #endregion

        #region Actions
        public void OpenCreateStudySetModal()
        {
            _windowManager.ShowDialog(new NewStudySetModalViewModel(_eventAggregator));
        }
        #endregion

        #region Handlers
        public void Handle(CreateStudySetEvent message)
        {
            CreateStudySet(message.Name);
        }
        #endregion

        private void Test()
        {
            /*for(int c=1; c<9; c++)
            {
                StudySets.Add(new StudySet()
                {
                    Name = $"Study Set {c} longbitattheendlol",
                    Terms = 120
                });
            }*/
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
