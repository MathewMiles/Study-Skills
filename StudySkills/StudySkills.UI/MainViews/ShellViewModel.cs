using Caliburn.Micro;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudySkills.UI.MainViews
{
    public class ShellViewModel : Conductor<object>
    {
        private TitleBarViewModel _titleBarVM;

        public ShellViewModel(TitleBarViewModel titleBarVM)
        {
            _titleBarVM = titleBarVM;
            ActivateItem(_titleBarVM);
        }
    }
}
