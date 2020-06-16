using System;
using System.Windows.Interactivity;

namespace StudySkills.UI.Core.Overrides
{
    /// <summary>
    /// Used instead of EventTrigger when it attaches the handler multiple times.
    /// </summary>
    public class AltEventTrigger : EventTrigger
    {
        private static bool isAttached;
        private static bool detachOldTrigger;
        private bool currentTrigger;

        protected override void OnAttached()
        {
            base.OnAttached();
            // Detach the old trigger if there is one, during next OnEvent
            if (isAttached)
                detachOldTrigger = true;
            else
            {
                isAttached = true;
                currentTrigger = true;
            }
        }

        protected override void OnEvent(EventArgs eventArgs)
        {
            // Detaches old trigger
            if (detachOldTrigger && currentTrigger)
            {
                detachOldTrigger = false;
                Detach();
                return;
            }
            if (!detachOldTrigger && !currentTrigger)
                currentTrigger = true;
            base.OnEvent(eventArgs);
        }
    }
}
