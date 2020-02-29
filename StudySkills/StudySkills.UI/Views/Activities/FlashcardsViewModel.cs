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
        private readonly IEventAggregator _eventAggregator;
        private readonly IStudySetManager _studySetManager;
        private ObservableCollection<TermDefinitionPair> _terms = new ObservableCollection<TermDefinitionPair>();
        private Fraction _cardNumber = new Fraction(1, 1);
        private int _selectedTermIndex;
        private string _frontSide, _backSide;
        private bool _canGoNext = true, _canGoPrevious;
        private double _random;

        override public event PropertyChangedEventHandler PropertyChanged;

        public FlashcardsViewModel(
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
                SelectedTermIndex = 0;
                CardNumber.Numerator = 1;
                CardNumber.Denominator = _terms.Count;
                FrontSide = _terms[0].Term;
                BackSide = _terms[0].Definition;
                NotifyPropertyChanged();
            }
        }

        public int SelectedTermIndex
        {
            get { return _selectedTermIndex; }
            set
            {
                _selectedTermIndex = value;
                NotifyPropertyChanged();
                CardNumber.Numerator = _selectedTermIndex + 1;
                NotifyPropertyChanged("CardNumber");
                FrontSide = Terms[_selectedTermIndex].Term;
                BackSide = Terms[_selectedTermIndex].Definition;
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

        public string BackSide
        {
            get { return _backSide; }
            set
            {
                _backSide = value;
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

        public bool CanGoNext
        {
            get { return _canGoNext; }
            set
            {
                _canGoNext = value;
                NotifyPropertyChanged();
            }
        }

        public bool CanGoPrevious
        {
            get { return _canGoPrevious; }
            set
            {
                _canGoPrevious = value;
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
                    NotifyPropertyChanged();
                    if (_random == 0)
                        Terms = _studySetManager.GetTerms();
                    if (_random == 1)
                        Terms = _studySetManager.GetRandomizedTerms();
                    SelectedTermIndex = 0;
                    CanGoPrevious = false;
                    CanGoNext = true;
                }
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

        public void NextTerm()
        {
            SelectedTermIndex++;
            if (SelectedTermIndex == Terms.Count - 1)
            {
                CanGoNext = false;
            }
            CanGoPrevious = true;
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
    }
}
