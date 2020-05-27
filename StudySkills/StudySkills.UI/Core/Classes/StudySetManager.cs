using Newtonsoft.Json;
using StudySkills.UI.Core.Models;
using System;
using System.Collections.ObjectModel;
using System.IO;

namespace StudySkills.UI.Core.Classes
{
    /// <summary>
    /// Handles saving/loading study sets to/from files and transferring terms.
    /// </summary>
    public class StudySetManager : IStudySetManager
    {
        #region Instance Variables
        private const string filePath = @"C:\ProgramData\Study Skills";
        private readonly JsonSerializer serializer = new JsonSerializer();
        private ObservableCollection<StudySet> _studySets = new ObservableCollection<StudySet>();
        private ObservableCollection<TermDefinitionPair> _terms = new ObservableCollection<TermDefinitionPair>();
        private Guid _currentFile;
        private string _studySetTitle;
        #endregion

        #region Properties
        public string StudySetTitle
        {
            get { return _studySetTitle; }
            set { _studySetTitle = value; }
        }
        #endregion

        #region Public Methods
        /// <summary>
        /// Gets a randomized ObservableCollection of the loaded terms.
        /// </summary>
        /// <returns></returns>
        public ObservableCollection<TermDefinitionPair> GetRandomizedTerms()
        {
            var randomList = new ObservableCollection<TermDefinitionPair>();
            var random = new Random();
            foreach (var term in _terms)
            {
                randomList.Add(new TermDefinitionPair() { Term = term.Term, Definition = term.Definition});
            }
            for (var c = _terms.Count; c > 0; c--)
            {
                randomList.Move(random.Next(c), _terms.Count - 1);
            }
            return randomList;
        }

        /// <summary>
        /// Gets an ObservableCollection of the loaded terms.
        /// </summary>
        public ObservableCollection<TermDefinitionPair> GetTerms() => _terms;

        /// <summary>
        /// Loads the StudySets from the json file and returns a ref to it.
        /// </summary>
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
            {
                using (StreamReader sr = new StreamReader(Path.Combine(filePath, "Study Sets.json")))
                    using (JsonReader reader = new JsonTextReader(sr))
                    {
                        _studySets = serializer.Deserialize<ObservableCollection<StudySet>>(reader);
                    }
            }
            return ref _studySets;
        }

        /// <summary>
        /// Loads the current StudySet's terms from its json file and returns a ref to it.
        /// </summary>
        /// <param name="fileName">Guid for the StudySet's file.</param>
        public ref ObservableCollection<TermDefinitionPair> LoadTerms(Guid fileName)
        {
            _currentFile = fileName;
            if (File.Exists(Path.Combine(filePath, "Study Sets", $"{fileName}.json")))
            {
                using (StreamReader sr = new StreamReader(Path.Combine(filePath, "Study Sets", $"{fileName}.json")))
                    using (JsonReader reader = new JsonTextReader(sr))
                    {
                        _terms = serializer.Deserialize<ObservableCollection<TermDefinitionPair>>(reader);
                    }
            }
            return ref _terms;
        }

        /// <summary>
        /// Save the StudySets to the json.
        /// </summary>
        public void SaveStudySets()
        {
            using (StreamWriter sw = new StreamWriter(Path.Combine(filePath, "Study Sets.json"), false))
                using (JsonWriter writer = new JsonTextWriter(sw))
                {
                    serializer.Serialize(writer, _studySets);
                }
        }

        /// <summary>
        /// Saves the current StudySet's terms to a json.
        /// </summary>
        public void SaveTerms()
        {
            using (StreamWriter sw = new StreamWriter(Path.Combine(filePath, "Study Sets", $"{_currentFile}.json"), false))
                using (JsonWriter writer = new JsonTextWriter(sw))
                {
                    serializer.Serialize(writer, _terms);
                }
        }
        #endregion
    }
}
