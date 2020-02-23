using Caliburn.Micro;
using StudySkills.UI.Core.Classes;
using StudySkills.UI.Core.Events;
using StudySkills.UI.Core.Models;
using System;
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
        private int _selectedTermIndex;
        private string _frontSide, _backSide;

        override public event PropertyChangedEventHandler PropertyChanged;

        public ObservableCollection<TermDefinitionPair> Terms
        {
            get { return _terms; }
            set
            {
                _terms = value;
                SelectedTermIndex = 0;
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
            if(SelectedTermIndex != Terms.Count - 1)
            {
                SelectedTermIndex++;
                FrontSide = Terms[SelectedTermIndex].Term;
                BackSide = Terms[SelectedTermIndex].Definition;
            }
        }

        public void PreviousTerm()
        {
            if (SelectedTermIndex != 0)
            {
                SelectedTermIndex--;
                FrontSide = Terms[SelectedTermIndex].Term;
                BackSide = Terms[SelectedTermIndex].Definition;
            }
        }
    }
}
