using Caliburn.Micro;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace StudySkills.UI.Views
{
    public class ShellViewModel : Conductor<object>
    {
        private readonly IEventAggregator _eventAggregator;
        private StudySetViewModel _studySetVM;

        public ShellViewModel(
            StudySetViewModel studySetVM,
            IEventAggregator eventAggregator)
        {
            _eventAggregator = eventAggregator;
            _studySetVM = studySetVM;
            ActivateItem(_studySetVM);
        }

        public void DragWindow(object sender)
        {
            ((Window)sender).DragMove(); 
        }

        public void Minimize(object sender)
        {
            ((Window)sender).WindowState = WindowState.Minimized;
        }

        public void Close(object sender)
        {
            ((Window)sender).Close();
        }
    }
}
