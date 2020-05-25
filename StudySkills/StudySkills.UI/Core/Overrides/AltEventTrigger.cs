using System;
using System.Windows.Interactivity;

namespace StudySkills.UI.Core.Overrides
{
    public class AltEventTrigger : EventTrigger
    {
        private static bool isAttached;
        private static bool detachOldTrigger;
        private bool currentTrigger;

        protected override void OnAttached()
        {
            base.OnAttached();
            if (isAttached) detachOldTrigger = true;
            else
            {
                isAttached = true;
                currentTrigger = true;
            }
        }

        protected override void OnEvent(EventArgs eventArgs)
        {
            if (detachOldTrigger && currentTrigger)
            {
                detachOldTrigger = false;
                Detach();
                return;
            }
            if (!detachOldTrigger && !currentTrigger) currentTrigger = true;
            base.OnEvent(eventArgs);
        }

        protected override void OnDetaching()
        {
            base.OnDetaching();
        }
    }
}
