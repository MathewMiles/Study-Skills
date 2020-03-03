using Caliburn.Micro;
using StudySkills.UI.Core.Classes;
using StudySkills.UI.Core.Events;
using StudySkills.UI.Core.Models;
using StudySkills.UI.Views.PopUps;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows.Controls;

namespace StudySkills.UI.Views
{
    public class StudySetViewModel : Screen, INotifyPropertyChanged,  IHandle<CreateStudySetEvent>, IHandle<AppClosingEvent>
    {
        #region Instance Variables
        private readonly IEventAggregator _eventAggregator;
        private readonly IWindowManager _windowManager;
        private readonly IStudySetManager _studySetManager;
        private ObservableCollection<StudySet> _studySets = new ObservableCollection<StudySet>();
        private ObservableCollection<TermDefinitionPair> _terms = new ObservableCollection<TermDefinitionPair>();
        private StudySet _selectedStudySet;
        private string _newTerm, _newDefinition;

        override public event PropertyChangedEventHandler PropertyChanged;
        #endregion

        public StudySetViewModel(
            IEventAggregator eventAggregator,
            IWindowManager windowManager,
            IStudySetManager studySetManager)
        {
            _eventAggregator = eventAggregator;
            _windowManager = windowManager;
            _studySetManager = studySetManager;

            _eventAggregator.Subscribe(this);
            StudySets = _studySetManager.LoadStudySets();
        }

        #region Properties
        public ObservableCollection<StudySet> StudySets 
        {
            get { return _studySets; }
            private set 
            {
                _studySets = value;
                NotifyPropertyChanged();
            }
        }

        public StudySet SelectedStudySet
        {
            get { return _selectedStudySet; }
            set
            {
                _selectedStudySet = value;
                NotifyPropertyChanged();
            }
        }

        public ObservableCollection<TermDefinitionPair> Terms
        {
            get { return _terms; }
            private set
            {
                _terms = value;
                NotifyPropertyChanged();
            }
        }

        public string NewTerm
        {
            get { return _newTerm; }
            set
            {
                _newTerm = value;
                NotifyPropertyChanged();
            }
        }

        public string NewDefinition
        {
            get { return _newDefinition; }
            set
            {
                _newDefinition = value;
                NotifyPropertyChanged();
            }
        }
        #endregion

        #region Private Methods
        private void CreateStudySet(string name)
        {
            StudySets.Add(new StudySet()
            {
                Name = name,
                Terms = 0,
                FileName = Guid.NewGuid()
            });
            SelectedStudySet = StudySets.Last();
            _studySetManager.SaveStudySets();
        }

        private void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion

        #region Actions
        public void AddTerm()
        {
            if (NewTerm == "" || NewTerm == null ||NewDefinition == "" || NewDefinition == null)
            {
                _windowManager.ShowDialog(new MessageModalViewModel("Error", "Must have a term and definition."));
                return;
            }
            Terms.Add(new TermDefinitionPair()
            {
                Term = NewTerm,
                Definition = NewDefinition
            });
            StudySets.ElementAt(StudySets.IndexOf(SelectedStudySet)).Terms++;
            NewTerm = "";
            NewDefinition = "";
        }

        public void DeleteTerm(TermDefinitionPair term)
        {
            Terms.Remove(term);
        }

        public void Flashcards()
        {
            _eventAggregator.PublishOnUIThread(new SwitchToActivityEvent() { NewActivity = Activity.Flashcards});
        }

        public void LoadTerms(SelectionChangedEventArgs e)
        {
            if (e.RemovedItems.Count > 0)
            {
                _studySetManager.SaveTerms();
            }
            Terms = _studySetManager.LoadTerms(SelectedStudySet.FileName);
            _studySetManager.StudySetTitle = SelectedStudySet.Name;
        }

        public void OpenCreateStudySetModal()
        {
            _windowManager.ShowDialog(new NewStudySetModalViewModel(_eventAggregator));
        }

        public void Match()
        {
            _eventAggregator.PublishOnUIThread(new SwitchToActivityEvent() { NewActivity = Activity.Match });
        }
        #endregion

        #region Handlers
        public void Handle(CreateStudySetEvent message)
        {
            CreateStudySet(message.Name);
        }

        public void Handle(AppClosingEvent message)
        {
            _studySetManager.SaveTerms();
            _studySetManager.SaveStudySets();
        }
        #endregion
    }
}
