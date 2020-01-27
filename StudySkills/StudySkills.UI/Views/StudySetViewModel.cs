using Caliburn.Micro;
using Newtonsoft.Json;
using StudySkills.UI.Core.Events;
using StudySkills.UI.Core.Models;
using StudySkills.UI.Views.PopUps;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;

namespace StudySkills.UI.Views
{
    public class StudySetViewModel : Screen, INotifyPropertyChanged,  IHandle<CreateStudySetEvent>, IHandle<AddTermEvent>
    {
        #region Instance Variables
        private readonly IEventAggregator _eventAggregator;
        private readonly IWindowManager _windowManager;
        private readonly JsonSerializer serializer = new JsonSerializer();
        private const string filePath = @"C:\ProgramData\Study Skills";
        private ObservableCollection<StudySet> _studySets = new ObservableCollection<StudySet>();
        private ObservableCollection<TermDefinitionPair> _terms = new ObservableCollection<TermDefinitionPair>();
        private StudySet _selectedStudySet;

        override public event PropertyChangedEventHandler PropertyChanged;
        #endregion

        public StudySetViewModel(
            IEventAggregator eventAggregator,
            IWindowManager windowManager)
        {
            _eventAggregator = eventAggregator;
            _windowManager = windowManager;

            _eventAggregator.Subscribe(this);
            LoadStudySets();
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
        #endregion

        #region Private Methods
        private void AddTerm(string term, string definition)
        {
            Terms.Add(new TermDefinitionPair()
            {
                Term = term,
                Definition = definition
            });
            StudySets.ElementAt(StudySets.IndexOf(SelectedStudySet)).Terms++;
            UpdateStudySet(StudySets.IndexOf(SelectedStudySet));
        }

        private void CreateStudySet(string name)
        {
            StudySets.Add(new StudySet()
            {
                Name = name,
                Terms = 0
            });
            SelectedStudySet = StudySets.Last();
            SaveStudySets();
        }

        private void LoadStudySets()
        {
            if(File.Exists(Path.Combine(filePath, "Study Sets.json")))
                using (StreamReader sr = new StreamReader(Path.Combine(filePath, "Study Sets.json")))
                    using (JsonReader reader = new JsonTextReader(sr))
                    {
                        StudySets = serializer.Deserialize<ObservableCollection<StudySet>>(reader);
                    }
        }

        private void NotifyPropertyChanged([CallerMemberName] String propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private void SaveStudySets()
        {
            if (!Directory.Exists(filePath))
            {
                Directory.CreateDirectory(filePath);
            }
            using (StreamWriter sw = new StreamWriter(Path.Combine(filePath, "Study Sets.json"), false))
                using (JsonWriter writer = new JsonTextWriter(sw))
                {
                    serializer.Serialize(writer, StudySets);
                }
        }
        
        private void UpdateStudySet(int index)
        {
            StudySets[index] = new StudySet { Name = StudySets[index].Name,
                                              Terms = StudySets[index].Terms};
            SelectedStudySet = StudySets[index];
        }

        private void UpdateTerm(int index)
        {
            Terms[index] = new TermDefinitionPair{ Term = Terms[index].Term,
                                                   Definition = Terms[index].Definition};
        }
        #endregion

        #region Actions
        public void DeleteTerm(TermDefinitionPair term)
        {
            Terms.Remove(term);
        }

        public void OpenCreateStudySetModal()
        {
            _windowManager.ShowDialog(new NewStudySetModalViewModel(_eventAggregator));
        }

        public void OpenNewTermModal()
        {
            _windowManager.ShowDialog(new NewTermModalViewModel(_eventAggregator));
        }
        #endregion

        #region Handlers
        public void Handle(CreateStudySetEvent message)
        {
            CreateStudySet(message.Name);
        }

        public void Handle(AddTermEvent message)
        {
            AddTerm(message.Term, message.Definition);
        }
        #endregion
    }
}
