using StudySkills.UI.Core.Models;
using System;
using System.Collections.ObjectModel;

namespace StudySkills.UI.Core.Classes
{
    /// <summary>
    /// Handles saving/loading study sets to/from files and transferring terms.
    /// </summary>
    public interface IStudySetManager
    {
        #region Properties
        string StudySetTitle
        {
            get;
            set;
        }
        #endregion

        #region Public Methods
        /// <summary>
        /// Gets an ObservableCollection of randomized terms.
        /// </summary>
        ObservableCollection<TermDefinitionPair> GetRandomizedTerms();

        /// <summary>
        /// Gets an ObservableCollection of the loaded terms.
        /// </summary>
        ObservableCollection<TermDefinitionPair> GetTerms();

        /// <summary>
        /// Loads the StudySets from the json file and returns a ref to it.
        /// </summary>
        ref ObservableCollection<StudySet> LoadStudySets();

        /// <summary>
        /// Loads the current StudySet's terms from its json file and returns a ref to it.
        /// </summary>
        /// <param name="fileName">Guid for the StudySet's file.</param>
        ref ObservableCollection<TermDefinitionPair> LoadTerms(Guid fileName);

        /// <summary>
        /// Save the StudySets to the json.
        /// </summary>
        void SaveStudySets();

        /// <summary>
        /// Saves the current StudySet's terms to a json.
        /// </summary>
        void SaveTerms();
        #endregion
    }
}
