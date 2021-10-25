using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace CS_361_Sliding_Puzzle
{
    public static class ViewSwitcher
    {
        public static Frame viewController;

        public static void Switch(UserControl newView)
        {
            viewController.Navigate(newView);
        }

        public static void Switch(UserControl newView, object state)
        {
            viewController.Navigate(newView, state);
        }
    }
}
