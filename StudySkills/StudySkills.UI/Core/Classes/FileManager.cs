﻿using Newtonsoft.Json;
using StudySkills.UI.Core.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudySkills.UI.Core.Classes
{
    public class FileManager : IFileManager
    {
        private const string filePath = @"C:\ProgramData\Study Skills";
        private readonly JsonSerializer serializer = new JsonSerializer();
        private ObservableCollection<StudySet> _studySets = new ObservableCollection<StudySet>();
        private ObservableCollection<TermDefinitionPair> _terms = new ObservableCollection<TermDefinitionPair>();
        private Guid _currentFile;

        public ref ObservableCollection<TermDefinitionPair> GetTerms() => ref _terms;

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