using Newtonsoft.Json;
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
        private Guid _fileName;

        public Guid FileName
        {
            get { return _fileName; }
            set { _fileName = value; }
        }

        public ObservableCollection<StudySet> LoadStudySets()
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
                        return serializer.Deserialize<ObservableCollection<StudySet>>(reader);
                    }
            return new ObservableCollection<StudySet>();
        }
        public ObservableCollection<TermDefinitionPair> LoadTerms(Guid fileName)
        {
            FileName = fileName;
            if (File.Exists(Path.Combine(filePath, "Study Sets", $"{fileName}.json")))
                using (StreamReader sr = new StreamReader(Path.Combine(filePath, "Study Sets", $"{fileName}.json")))
                using (JsonReader reader = new JsonTextReader(sr))
                {
                    return serializer.Deserialize<ObservableCollection<TermDefinitionPair>>(reader);
                }
            return new ObservableCollection<TermDefinitionPair>();
        }
        public void SaveStudySets()
        {

        }
        public void SaveTerms()
        {

        }
    }
}
