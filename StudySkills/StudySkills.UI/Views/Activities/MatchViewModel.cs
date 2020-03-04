using Caliburn.Micro;
using StudySkills.UI.Core.Classes;
using StudySkills.UI.Core.Events;
using StudySkills.UI.Core.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace StudySkills.UI.Views.Activities
{
    public class MatchViewModel : Screen, INotifyPropertyChanged
    {
        private readonly IEventAggregator _eventAggregator;
        private readonly IStudySetManager _studySetManager;
        private ObservableCollection<TermDefinitionPair> _terms = new ObservableCollection<TermDefinitionPair>();
        private ObservableCollection<MatchCard> _matchCards = new ObservableCollection<MatchCard>();
        private string _title;

        override public event PropertyChangedEventHandler PropertyChanged;

        public MatchViewModel(
            IEventAggregator eventAggregator,
            IStudySetManager studySetManager)
        {
            _eventAggregator = eventAggregator;
            _studySetManager = studySetManager;
        }

        public ObservableCollection<TermDefinitionPair> Terms
        {
            get { return _terms; }
            set
            {
                _terms = value;
                NotifyPropertyChanged();
                Title = _studySetManager.StudySetTitle;
                CreateMatchCards();
            }
        }

        public ObservableCollection<MatchCard> MatchCards
        {
            get { return _matchCards; }
            set
            {
                _matchCards = value;
                NotifyPropertyChanged();
            }
        }

        public string Title
        {
            get { return _title; }
            set
            {
                _title = value;
                NotifyPropertyChanged();
            }
        }

        private void CreateMatchCards()
        {
            MatchCards.Clear();
            for (int c = 0; c < 8; c++)
            {
                MatchCards.Add(new MatchCard(Terms[c].Term, c));
                MatchCards.Add(new MatchCard(Terms[c].Definition, c));
            }
            Random random = new Random();
            for (int c = _terms.Count; c > 0; c--)
            {
                MatchCards.Move(random.Next(c), _terms.Count - 1);
            }
        }

        private void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public void GoBack()
        {
            _eventAggregator.PublishOnUIThread(new GoBackEvent());
        }
    }
}
