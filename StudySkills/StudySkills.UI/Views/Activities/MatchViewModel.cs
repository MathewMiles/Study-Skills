using Caliburn.Micro;
using StudySkills.UI.Core.Classes;
using StudySkills.UI.Core.Events;
using StudySkills.UI.Core.Models;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Controls;

namespace StudySkills.UI.Views.Activities
{
    public class MatchViewModel : Screen, INotifyPropertyChanged
    {
        private readonly IEventAggregator _eventAggregator;
        private readonly IStudySetManager _studySetManager;
        private ObservableCollection<TermDefinitionPair> _terms = new ObservableCollection<TermDefinitionPair>();
        private ObservableCollection<MatchCard> _matchCards = new ObservableCollection<MatchCard>();
        private MatchCard _selectedMatchCard = null;
        private string _title;
        private int _lastCardId = -1;

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

        public MatchCard SelectedMatchCard
        {
            get { return _selectedMatchCard; }
            set
            {
                _selectedMatchCard = value;
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
            for (int c = 16; c > 0; c--)
            {
                MatchCards.Move(random.Next(c), 15);
            }
        }

        private void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public void CheckMatch(SelectionChangedEventArgs e)
        {
            if(e.AddedItems.Count == 1)
            {
                if (_lastCardId == -1)
                {
                    _lastCardId = ((MatchCard)e.AddedItems[0]).Id;
                }
                else
                {
                    if (_lastCardId == ((MatchCard)e.AddedItems[0]).Id)
                    {
                        for (int c = 0; c < 16; c++)
                        {
                            if (MatchCards[c].Id == _lastCardId)
                                MatchCards[c].Unmatched = false;
                        }
                    }
                    _lastCardId = -1;
                    SelectedMatchCard = null;
                }
            }
            else if (MatchCards.Count > 0)
            {
                _lastCardId = -1;
                for (int c = 0; c < 16; c++)
                {
                    if (MatchCards[c].Unmatched)
                        return;
                }
                Terms = _studySetManager.GetRandomizedTerms();
                CreateMatchCards();
            }
        }

        public void GoBack()
        {
            Terms.Clear();
            MatchCards.Clear();
            _eventAggregator.PublishOnUIThread(new GoBackEvent());
        }

        public void OnLoad()
        {
            Title = _studySetManager.StudySetTitle;
            Terms = _studySetManager.GetRandomizedTerms();
            CreateMatchCards();
            _lastCardId = -1;
        }
    }
}
