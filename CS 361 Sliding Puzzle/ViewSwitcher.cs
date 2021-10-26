using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace CS_361_Sliding_Puzzle
{
    /// <summary>
    /// Static class used to switch the current view
    /// and credits to website for code structure: https://azerdark.wordpress.com/2010/04/23/multi-page-application-in-wpf/
    /// </summary>
    public static class ViewSwitcher
    {
        public static Frame TheFrame;

        public static void Switch(UserControl newView)
        {
            TheFrame.Navigate(newView);
        }

        public static void Switch(UserControl newView, object state)
        {
            TheFrame.Navigate(newView, state);
        }
    }
}
