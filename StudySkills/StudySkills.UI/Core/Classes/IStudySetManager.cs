using StudySkills.UI.Core.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudySkills.UI.Core.Classes
{
    public interface IStudySetManager
    {
        ref ObservableCollection<TermDefinitionPair> GetTerms();
        ref ObservableCollection<StudySet> LoadStudySets();
        ref ObservableCollection<TermDefinitionPair> LoadTerms(Guid fileName);
        void SaveStudySets();
        void SaveTerms();
    }
}
