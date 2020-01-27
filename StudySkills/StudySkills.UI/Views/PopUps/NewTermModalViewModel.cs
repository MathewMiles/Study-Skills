using Caliburn.Micro;
using StudySkills.UI.Core.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudySkills.UI.Views.PopUps
{
    public class NewTermModalViewModel : Screen
    {
        private string _term;
        private string _definition;
        private bool _termHasDefaultValue = true;
        private bool _definitionHasDefaultValue = true;
        private readonly IEventAggregator _eventAggregator;

        public NewTermModalViewModel(
            IEventAggregator eventAggregator)
        {
            _eventAggregator = eventAggregator;
            this.Parent = App.Current.MainWindow;
            Term = "Term";
            Definition = "Definition";
        }

        public string Term
        {
            get { return _term; }
            set
            {
                _term = value;
                NotifyOfPropertyChange(() => Term);
            }
        }

        public string Definition
        {
            get { return _definition; }
            set
            {
                _definition = value;
                NotifyOfPropertyChange(() => Definition);
            }
        }

        public void Add()
        {
            _eventAggregator.PublishOnUIThread(new AddTermEvent { Term = this.Term, Definition = this.Definition });
            Term = "Term";
            _termHasDefaultValue = true;
            Definition = "Definition";
            _definitionHasDefaultValue = true;
        }

        public void Cancel()
        {
            this.TryClose();
        }

        public void Term_GotFocus()
        {
            if (_termHasDefaultValue)
            {
                Term = "";
            }
        }

        public void Term_LostFocus()
        {
            if (Term == "")
            {
                Term = "Term";
                _termHasDefaultValue = true;
            }
            else
            {
                _termHasDefaultValue = false;
            }
        }

        public void Definition_GotFocus()
        {
            if (_definitionHasDefaultValue)
            {
                Definition = "";
            }
        }

        public void Definition_LostFocus()
        {
            if (Definition == "")
            {
                Definition = "Definition";
                _definitionHasDefaultValue = true;
            }
            else
            {
                _definitionHasDefaultValue = false;
            }
        }
    }
}
