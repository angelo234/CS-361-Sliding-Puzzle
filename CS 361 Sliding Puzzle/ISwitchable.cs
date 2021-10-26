using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CS_361_Sliding_Puzzle
{
    /// <summary>
    /// Inherited by UserControls to allow them to be switched to by the ViewSwitcher
    /// and credits to website for code structure: https://azerdark.wordpress.com/2010/04/23/multi-page-application-in-wpf/
    /// </summary>
    public interface ISwitchable
    {
        void OnViewSwitched(object state);
    }
}
