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
using System.Windows.Media.Animation;
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
        private Storyboard storyboard;

        public Frame()
        {
            InitializeComponent();

            // Animate opacity when changing views

            var animation1 = new DoubleAnimation();
            animation1.From = 0;
            animation1.To = 1.0;
            animation1.Duration = new Duration(TimeSpan.FromSeconds(0.25));

            storyboard = new Storyboard();
            storyboard.Children.Add(animation1);

            Storyboard.SetTargetName(animation1, Name);
            Storyboard.SetTargetProperty(animation1, new PropertyPath(OpacityProperty));


            ViewSwitcher.TheFrame = this;

            ViewSwitcher.TheViews.Add("main_menu", new MainMenuView());
            ViewSwitcher.TheViews.Add("image_selection_view", new ImageSelectionView());
            ViewSwitcher.TheViews.Add("game_view", new GameView());

            ViewSwitcher.Switch("main_menu");
        }

        public void Navigate(UserControl nextView)
        {
            Navigate(nextView, null);
        }

        public void Navigate(UserControl nextView, object state)
        {
            this.Content = nextView;

            storyboard.Begin(this, true);

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
