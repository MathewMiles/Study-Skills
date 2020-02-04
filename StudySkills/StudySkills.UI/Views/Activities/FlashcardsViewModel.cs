using Caliburn.Micro;
using StudySkills.UI.Core.Classes;
using StudySkills.UI.Core.Events;
using StudySkills.UI.Core.Models;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;

namespace StudySkills.UI.Views.Activities
{
    public class FlashcardsViewModel : Screen, INotifyPropertyChanged
    {
        private readonly IEventAggregator _eventAggregator;
        private readonly IFileManager _fileManager;
        private ObservableCollection<TermDefinitionPair> _terms = new ObservableCollection<TermDefinitionPair>();
        private TermDefinitionPair _selectedTerm;

        override public event PropertyChangedEventHandler PropertyChanged;

        public ObservableCollection<TermDefinitionPair> Terms
        {
            get { return _terms; }
            set
            {
                _terms = value;
                SelectedTerm = _terms[0];
                NotifyPropertyChanged();
            }
        }

        public TermDefinitionPair SelectedTerm
        {
            get { return _selectedTerm; }
            set
            {
                _selectedTerm = value;
                NotifyPropertyChanged();
            }
        }

        public FlashcardsViewModel(
            IEventAggregator eventAggregator,
            IFileManager fileManager)
        {
            _eventAggregator = eventAggregator;
            _fileManager = fileManager;
        }

        private void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public void GoBack()
        {
            _eventAggregator.PublishOnUIThread(new GoBackEvent());
        }

        public void NextTerm()
        {
            if (Terms.IndexOf(SelectedTerm) != Terms.Count - 1)
            {
                SelectedTerm = Terms.ElementAt(Terms.IndexOf(SelectedTerm) + 1);
            }
        }

        public void PreviousTerm()
        {
            if(Terms.IndexOf(SelectedTerm) != 0)
            {
                SelectedTerm = Terms.ElementAt(Terms.IndexOf(SelectedTerm) - 1);
            }
        }
    }
}
