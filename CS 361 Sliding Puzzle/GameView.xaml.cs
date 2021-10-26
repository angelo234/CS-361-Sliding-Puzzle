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

namespace CS_361_Sliding_Puzzle
{
    /// <summary>
    /// Interaction logic for GameView.xaml
    /// </summary>
    public partial class GameView : UserControl, ISwitchable
    {
        public GameView(string imageFileName)
        {
            InitializeComponent();

            SlidingPuzzleGame game = new SlidingPuzzleGame(null, 4, 4);

            game.TryMoveTile(2, 3);
            game.PrintBoard();

            game.TryMoveTile(2, 2);
            game.PrintBoard();

            game.TryMoveTile(1, 1);
            game.PrintBoard();
        }

        public void OnViewSwitched(object state)
        {
            throw new NotImplementedException();
        }
    }
}
