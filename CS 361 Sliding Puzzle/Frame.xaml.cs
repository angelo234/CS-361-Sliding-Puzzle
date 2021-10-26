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
using System.Windows.Shapes;

namespace CS_361_Sliding_Puzzle
{
    /// <summary>
    /// Interaction logic for Frame.xaml
    /// and credits to website for code structure: https://azerdark.wordpress.com/2010/04/23/multi-page-application-in-wpf/
    /// </summary>
    public partial class Frame : Window
    {   
        public Frame()
        {
            InitializeComponent();
            ViewSwitcher.TheFrame = this;
            ViewSwitcher.Switch(new MainMenuView());
        }

        public void Navigate(UserControl nextView)
        {
            this.Content = nextView;
        }

        public void Navigate(UserControl nextView, object state)
        {
            this.Content = nextView;

            if (nextView is ISwitchable)
            {
                ISwitchable view = nextView as ISwitchable;

                view.OnViewSwitched(state);
            } 
            else
            {
                throw new ArgumentException("NextPage is not ISwitchable! "
                  + nextView.Name.ToString());
            }
                
        }
    }
}
