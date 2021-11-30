using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Interop;
using System.Windows.Media;
using System.Windows.Media.Animation;
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
        private Storyboard storyboard1;
        private Storyboard storyboard2;

        private System.Drawing.Image boardImage;

        private SlidingPuzzleGame game;

        private int rows = 3;
        private int columns = 3;

        private Timer timer;
        private long timeElapsed = 0;

        public GameView()
        {
            InitializeComponent();

            // Animation for the canvas opacity on win
            var animation1 = new DoubleAnimation();
            animation1.From = 1.0;
            animation1.To = 0.5;
            animation1.Duration = new Duration(TimeSpan.FromSeconds(0.25));

            storyboard1 = new Storyboard();
            storyboard1.Children.Add(animation1);

            Storyboard.SetTargetName(animation1, TheCanvas.Name);
            Storyboard.SetTargetProperty(animation1, new PropertyPath(OpacityProperty));

            // Animation for the "You Win!" text
            var animation2 = new DoubleAnimation();
            animation2.From = 0.0;
            animation2.To = 1.0;
            animation2.Duration = new Duration(TimeSpan.FromSeconds(0.25));

            storyboard2 = new Storyboard();
            storyboard2.Children.Add(animation2);

            Storyboard.SetTargetName(animation2, YouWinImage.Name);
            Storyboard.SetTargetProperty(animation2, new PropertyPath(OpacityProperty));

            timer = new System.Timers.Timer();
            timer.Interval = 1000;
            timer.Elapsed += UpdateTimerLabel;
        }

        // Reset all variables
        public void OnViewSwitched(object state)
        {
            if (state == null)
            {
                return;
            }

            boardImage = (System.Drawing.Image) state;

            game = new SlidingPuzzleGame(boardImage, (int)TheCanvas.Width, (int)TheCanvas.Height, rows, columns);

            // reset animations
            storyboard1.Stop(TheCanvas);
            storyboard2.Stop(YouWinImage);

            RenderCanvas(0);

            // reset timer
            timeElapsed = 0;
            timer.Start();
        }

        // Update the game timer label
        private void UpdateTimerLabel(Object source, System.Timers.ElapsedEventArgs e)
        {
            Dispatcher.BeginInvoke(new Action(() =>
            {
                timeElapsed++;
                TimerLabel.Content = "Time:  " + timeElapsed;
            })); 
        }

        // Render canvas tiles
        private void RenderCanvas(int gameResult)
        {
            Bitmap theRender = new Bitmap((int)TheCanvas.Width, (int)TheCanvas.Height);

            var g = Graphics.FromImage(theRender);
            
            for (int y = 0; y < rows; y++)
            {
                for (int x = 0; x < columns; x++)
                {
                    System.Drawing.Image tileImage = game.GetTileImage(x, y);

                    if (tileImage != null)
                    {
                        g.DrawImage(tileImage, new System.Drawing.Rectangle(x * tileImage.Width, y * tileImage.Height, tileImage.Width, tileImage.Height), new System.Drawing.Rectangle(0, 0, tileImage.Width, tileImage.Height), GraphicsUnit.Pixel);
                    }
                    
                }
            }

            // If player won game then display "You Win!" text
            if(gameResult == 2)
            {
                YouWinImage.Visibility = Visibility.Visible;

                storyboard1.Begin(TheCanvas, true);
                storyboard2.Begin(YouWinImage, true);

                timer.Stop();
            }

            g.Dispose();

            ImageSource bitmapSource = Imaging.CreateBitmapSourceFromHBitmap(
                theRender.GetHbitmap(), IntPtr.Zero, Int32Rect.Empty, BitmapSizeOptions.FromEmptyOptions());
            
            theRender.Dispose();

            TheCanvas.Background = new ImageBrush(bitmapSource);
        }

        // When user clicks on the board
        private void TheCanvas_MouseUp(object sender, MouseButtonEventArgs e)
        {
            System.Windows.Point mousePos = e.GetPosition(TheCanvas);

            int mouseX = (int) mousePos.X;
            int mouseY = (int) mousePos.Y;

            int result = game.ClickedOnBoard(mouseX, mouseY);

            RenderCanvas(result);
        }

        private void NewGameButton_Click(object sender, RoutedEventArgs e)
        {
            game = new SlidingPuzzleGame(boardImage, (int)TheCanvas.Width, (int)TheCanvas.Height, rows, columns);
            storyboard1.Stop(TheCanvas);
            storyboard2.Stop(YouWinImage);
            YouWinImage.Visibility = Visibility.Hidden;

            timeElapsed = 0;
            timer.Start();
            RenderCanvas(0);
        }

        private void QuitButton_Click(object sender, RoutedEventArgs e)
        {
            // reset everything

            game = null;

            boardImage.Dispose();
            timer.Stop();

            TimerLabel.Content = "Time: 0";

            ViewSwitcher.Switch("main_menu");
        }

    }
}
