using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace StudySkills.UI.Core.Controls
{
    /// <summary>
    ///
    /// Step 1) Using this custom control in a XAML file that exists in the current project.
    /// Add this XmlNamespace attribute to the root element of the markup file where it is 
    /// to be used:
    ///
    ///     xmlns:MyNamespace="clr-namespace:StudySkills.UI.Core.Controls"
    ///
    /// Step 2)
    /// Go ahead and use your control in the XAML file.
    ///
    ///     <MyNamespace:FlashcardsControl/>
    ///
    /// </summary>
    public class FlashcardsControl : Control
    {
        static FlashcardsControl()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(FlashcardsControl), new FrameworkPropertyMetadata(typeof(FlashcardsControl)));
        }
    }
}
