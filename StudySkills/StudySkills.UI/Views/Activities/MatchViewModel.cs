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
        #region Instance Variables
        private readonly IEventAggregator _eventAggregator;
        private readonly IStudySetManager _studySetManager;
        private ObservableCollection<TermDefinitionPair> _terms = new ObservableCollection<TermDefinitionPair>();
        private ObservableCollection<MatchCard> _matchCards = new ObservableCollection<MatchCard>();
        private MatchCard _selectedMatchCard = null;
        private string _title;
        private int _lastCardId = -1;

        override public event PropertyChangedEventHandler PropertyChanged;
        #endregion

        public MatchViewModel(
            IEventAggregator eventAggregator,
            IStudySetManager studySetManager)
        {
            _eventAggregator = eventAggregator;
            _studySetManager = studySetManager;
        }

        #region Properties
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

        public ObservableCollection<TermDefinitionPair> Terms
        {
            get { return _terms; }
            set
            {
                _terms = value;
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
        #endregion

        #region Private Methods
        /// <summary>
        /// Resets the cards
        /// </summary>
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
        #endregion

        #region Actions
        /// <summary>
        /// Logic for the cards being selected and matched
        /// </summary>
        public void CheckMatch(SelectionChangedEventArgs e)
        {
            if(e.AddedItems.Count == 1)
            {
                // If only one card is selected, save as last card
                if (_lastCardId == -1)
                {
                    _lastCardId = ((MatchCard)e.AddedItems[0]).Id;
                }
                else
                {
                    // If cards are a match, they disappear, otherwise it resets the selections
                    if (_lastCardId == ((MatchCard)e.AddedItems[0]).Id)
                    {
                        foreach (var card in MatchCards)
                        {
                            if (card.Id == _lastCardId)
                                card.Unmatched = false;
                        }
                    }
                    _lastCardId = -1;
                    SelectedMatchCard = null;
                }
            }
            // Resets cards if all are matched
            else if (MatchCards.Count > 0)
            {
                _lastCardId = -1;
                foreach (var card in MatchCards)
                {
                    if (card.Unmatched)
                        return;
                }
                Terms = _studySetManager.GetRandomizedTerms();
                CreateMatchCards();
            }
        }

        /// <summary>
        /// Returns to the study set view
        /// </summary>
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
        #endregion
    }
}
