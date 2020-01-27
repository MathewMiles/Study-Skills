using Caliburn.Micro;
using Newtonsoft.Json;
using StudySkills.UI.Core.Classes;
using StudySkills.UI.Core.Events;
using StudySkills.UI.Core.Models;
using StudySkills.UI.Views.PopUps;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows.Controls;

namespace StudySkills.UI.Views
{
    public class StudySetViewModel : Screen, INotifyPropertyChanged,  IHandle<CreateStudySetEvent>, IHandle<AddTermEvent>, IHandle<AppClosingEvent>
    {
        #region Instance Variables
        private readonly IEventAggregator _eventAggregator;
        private readonly IWindowManager _windowManager;
        private readonly IFileManager _fileManager;
        private readonly JsonSerializer serializer = new JsonSerializer();
        private const string filePath = @"C:\ProgramData\Study Skills";
        private ObservableCollection<StudySet> _studySets = new ObservableCollection<StudySet>();
        private ObservableCollection<TermDefinitionPair> _terms = new ObservableCollection<TermDefinitionPair>();
        private StudySet _selectedStudySet;

        override public event PropertyChangedEventHandler PropertyChanged;
        #endregion

        public StudySetViewModel(
            IEventAggregator eventAggregator,
            IWindowManager windowManager,
            IFileManager fileManager)
        {
            _eventAggregator = eventAggregator;
            _windowManager = windowManager;
            _fileManager = fileManager;

            _eventAggregator.Subscribe(this);
            StudySets = _fileManager.LoadStudySets();
            //LoadStudySets();
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
        }

        private void CreateStudySet(string name)
        {
            StudySets.Add(new StudySet()
            {
                Name = name,
                Terms = 0,
                FileName = Guid.NewGuid()
            });
            SelectedStudySet = StudySets.Last();
            //SaveStudySets();
            _fileManager.SaveStudySets();
        }

        private void LoadStudySets()
        {
            if (!Directory.Exists(filePath))
            {
                Directory.CreateDirectory(filePath);
            }

            if (!Directory.Exists(Path.Combine(filePath, "Study Sets")))
            {
                Directory.CreateDirectory(Path.Combine(filePath, "Study Sets"));
            }

            if (File.Exists(Path.Combine(filePath, "Study Sets.json")))
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
            using (StreamWriter sw = new StreamWriter(Path.Combine(filePath, "Study Sets.json"), false))
                using (JsonWriter writer = new JsonTextWriter(sw))
                {
                    serializer.Serialize(writer, StudySets);
                }
        }

        private void SaveTerms(int index)
        {
            if (index >= 0)
            {
                using (StreamWriter sw = new StreamWriter(Path.Combine(filePath, "Study Sets", $"{StudySets[index].FileName}.json"), false))
                    using (JsonWriter writer = new JsonTextWriter(sw))
                    {
                        serializer.Serialize(writer, Terms);
                    }
            }
        }
        #endregion

        #region Actions
        public void DeleteTerm(TermDefinitionPair term)
        {
            Terms.Remove(term);
        }

        public void LoadTerms(SelectionChangedEventArgs e)
        {
            if (e.RemovedItems.Count > 0)
            {
                //SaveTerms(StudySets.IndexOf((StudySet)e.RemovedItems[0]));
                _fileManager.SaveTerms();
            }
            /*if (File.Exists(Path.Combine(filePath, "Study Sets", $"{SelectedStudySet.FileName}.json")))
                using (StreamReader sr = new StreamReader(Path.Combine(filePath, "Study Sets", $"{SelectedStudySet.FileName}.json")))
                    using (JsonReader reader = new JsonTextReader(sr))
                    {
                        Terms = serializer.Deserialize<ObservableCollection<TermDefinitionPair>>(reader);
                    }*/
            Terms = _fileManager.LoadTerms(SelectedStudySet.FileName);
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

        public void Handle(AppClosingEvent message)
        {
            //SaveTerms(StudySets.IndexOf(SelectedStudySet));
            _fileManager.SaveTerms();
            _fileManager.SaveStudySets();
        }
        #endregion
    }
}
