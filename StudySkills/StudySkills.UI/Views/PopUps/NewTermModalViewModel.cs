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
        private readonly IEventAggregator _eventAggregator;

        public NewTermModalViewModel(
            IEventAggregator eventAggregator)
        {
            _eventAggregator = eventAggregator;
            this.Parent = App.Current.MainWindow;
        }

        public string Term
        {
            get { return _term; }
            set
            {
                _term = value;
            }
        }

        public string Definition
        {
            get { return _definition; }
            set
            {
                _definition = value;
            }
        }

        public void Add()
        {
            _eventAggregator.PublishOnUIThread(new AddTermEvent { Term = this.Term, Definition = this.Definition });
        }

        public void Cancel()
        {
            this.TryClose();
        }
    }
}
