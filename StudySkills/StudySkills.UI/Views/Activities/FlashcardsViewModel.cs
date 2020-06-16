using Caliburn.Micro;
using StudySkills.UI.Core.Classes;
using StudySkills.UI.Core.Events;
using StudySkills.UI.Core.Models;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace StudySkills.UI.Views.Activities
{
    public class FlashcardsViewModel : Screen, INotifyPropertyChanged
    {
        #region Instance Variables
        private readonly IEventAggregator _eventAggregator;
        private readonly IStudySetManager _studySetManager;
        private ObservableCollection<TermDefinitionPair> _terms = new ObservableCollection<TermDefinitionPair>();
        private Fraction _cardNumber = new Fraction(1, 1);
        private int _selectedTermIndex;
        private string _frontSide, _backSide, _title;
        private bool _canGoNext = true, _canGoPrevious;
        private double _random;

        override public event PropertyChangedEventHandler PropertyChanged;
        #endregion

        public FlashcardsViewModel(
            IEventAggregator eventAggregator,
            IStudySetManager studySetManager)
        {
            _eventAggregator = eventAggregator;
            _studySetManager = studySetManager;
        }

        #region Properties
        public string BackSide
        {
            get { return _backSide; }
            set
            {
                _backSide = value;
                NotifyPropertyChanged();
            }
        }

        // Controls the visibility of the next button
        public bool CanGoNext
        {
            get { return _canGoNext; }
            set
            {
                _canGoNext = value;
                NotifyPropertyChanged();
            }
        }

        // Controls the visibility of the previous button
        public bool CanGoPrevious
        {
            get { return _canGoPrevious; }
            set
            {
                _canGoPrevious = value;
                NotifyPropertyChanged();
            }
        }

        public Fraction CardNumber
        {
            get { return _cardNumber; }
            set
            {
                _cardNumber = value;
                NotifyPropertyChanged();
            }
        }

        public string FrontSide
        {
            get { return _frontSide; }
            set
            {
                _frontSide = value;
                NotifyPropertyChanged();
            }
        }

        public double Random
        {
            get { return _random; }
            set
            {
                if (_random != value)
                {
                    _random = value;
                    if (_random == 0)
                        Terms = _studySetManager.GetTerms();
                    if (_random == 1)
                        Terms = _studySetManager.GetRandomizedTerms();
                    NotifyPropertyChanged();
                }
            }
        }

        public int SelectedTermIndex
        {
            get { return _selectedTermIndex; }
            set
            {
                _selectedTermIndex = value;
                // Updates card when SelectedTermIndex changes
                CardNumber.Numerator = _selectedTermIndex + 1;
                FrontSide = Terms[_selectedTermIndex].Term;
                BackSide = Terms[_selectedTermIndex].Definition;
                NotifyPropertyChanged();
                NotifyPropertyChanged("CardNumber");
            }
        }

        public ObservableCollection<TermDefinitionPair> Terms
        {
            get { return _terms; }
            set
            {
                _terms = value;
                // Resets cards whenever the terms change
                SelectedTermIndex = 0;
                CanGoPrevious = false;
                CanGoNext = true;
                CardNumber.Numerator = 1;
                CardNumber.Denominator = _terms.Count;
                FrontSide = _terms[0].Term;
                BackSide = _terms[0].Definition;
                Title = _studySetManager.StudySetTitle;
                NotifyPropertyChanged();
                NotifyPropertyChanged("CardNumber");
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
        private void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion

        #region Actions
        /// <summary>
        /// Returns to the study set view
        /// </summary>
        public void GoBack()
        {
            _eventAggregator.PublishOnUIThread(new GoBackEvent());
        }

        public void NextTerm()
        {
            SelectedTermIndex++;
            if (SelectedTermIndex == Terms.Count - 1)
            {
                CanGoNext = false;
            }
            CanGoPrevious = true;
        }

        public void OnLoad()
        {
            Terms = _studySetManager.GetTerms();
            Random = 0;
        }

        public void PreviousTerm()
        {
            SelectedTermIndex--;
            if (SelectedTermIndex == 0)
            {
                CanGoPrevious = false;
            }
            CanGoNext = true;
        }
        #endregion
    }
}
