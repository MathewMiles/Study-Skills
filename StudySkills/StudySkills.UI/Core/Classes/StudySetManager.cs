using Newtonsoft.Json;
using StudySkills.UI.Core.Models;
using System;
using System.Collections.ObjectModel;
using System.IO;

namespace StudySkills.UI.Core.Classes
{
    public class StudySetManager : IStudySetManager
    {
        private const string filePath = @"C:\ProgramData\Study Skills";
        private readonly JsonSerializer serializer = new JsonSerializer();
        private ObservableCollection<StudySet> _studySets = new ObservableCollection<StudySet>();
        private ObservableCollection<TermDefinitionPair> _terms = new ObservableCollection<TermDefinitionPair>();
        private Guid _currentFile;
        private string _studySetTitle;

        public string StudySetTitle
        {
            get { return _studySetTitle; }
            set { _studySetTitle = value; }
        }

        public ObservableCollection<TermDefinitionPair> GetRandomizedTerms()
        {
            ObservableCollection<TermDefinitionPair> randomList = new ObservableCollection<TermDefinitionPair>();
            Random random = new Random();
            for (int c = 0; c < _terms.Count; c++)
            {
                randomList.Add(new TermDefinitionPair() { Term = _terms[c].Term, Definition = _terms[c].Definition});
            }
            for (int c = _terms.Count; c > 0; c--)
            {
                randomList.Move(random.Next(c), _terms.Count - 1);
            }
            return randomList;
        }
        
        public ObservableCollection<TermDefinitionPair> GetTerms() => _terms;

        public ref ObservableCollection<StudySet> LoadStudySets()
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
                        _studySets = serializer.Deserialize<ObservableCollection<StudySet>>(reader);
                    }
            return ref _studySets;
        }
        public ref ObservableCollection<TermDefinitionPair> LoadTerms(Guid fileName)
        {
            _currentFile = fileName;
            if (File.Exists(Path.Combine(filePath, "Study Sets", $"{fileName}.json")))
                using (StreamReader sr = new StreamReader(Path.Combine(filePath, "Study Sets", $"{fileName}.json")))
                using (JsonReader reader = new JsonTextReader(sr))
                {
                    _terms = serializer.Deserialize<ObservableCollection<TermDefinitionPair>>(reader);
                }
            return ref _terms;
        }
        public void SaveStudySets()
        {
            using (StreamWriter sw = new StreamWriter(Path.Combine(filePath, "Study Sets.json"), false))
                using (JsonWriter writer = new JsonTextWriter(sw))
                {
                    serializer.Serialize(writer, _studySets);
                }
        }
        public void SaveTerms()
        {
            using (StreamWriter sw = new StreamWriter(Path.Combine(filePath, "Study Sets", $"{_currentFile}.json"), false))
            using (JsonWriter writer = new JsonTextWriter(sw))
            {
                serializer.Serialize(writer, _terms);
            }
        }
    }
}
