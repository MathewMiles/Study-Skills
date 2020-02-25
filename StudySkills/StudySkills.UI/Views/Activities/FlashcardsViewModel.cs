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
        private readonly IFileManager _fileManager;
        private ObservableCollection<TermDefinitionPair> _terms = new ObservableCollection<TermDefinitionPair>();
        private int _selectedTermIndex;
        private string _frontSide, _backSide;
        private Fraction _cardNumber;

        override public event PropertyChangedEventHandler PropertyChanged;

        public FlashcardsViewModel(
            IEventAggregator eventAggregator,
            IFileManager fileManager)
        {
            _eventAggregator = eventAggregator;
            _fileManager = fileManager;
        }

        public ObservableCollection<TermDefinitionPair> Terms
        {
            get { return _terms; }
            set
            {
                _terms = value;
                SelectedTermIndex = 0;
                CardNumber = new Fraction(1, _terms.Count);
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
            if(SelectedTermIndex != Terms.Count - 1)
            {
                SelectedTermIndex++;
                CardNumber.Numerator++;
                NotifyPropertyChanged("CardNumber");
                FrontSide = Terms[SelectedTermIndex].Term;
                BackSide = Terms[SelectedTermIndex].Definition;
            }
        }

        public void PreviousTerm()
        {
            if (SelectedTermIndex != 0)
            {
                SelectedTermIndex--;
                CardNumber.Numerator--;
                NotifyPropertyChanged("CardNumber");
                FrontSide = Terms[SelectedTermIndex].Term;
                BackSide = Terms[SelectedTermIndex].Definition;
            }
        }
    }
}
