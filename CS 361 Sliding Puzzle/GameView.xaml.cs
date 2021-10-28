using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Interop;
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
        private System.Drawing.Image boardImage;

        private SlidingPuzzleGame game;

        private int rows = 3;
        private int columns = 3; 

        public GameView()
        {
            InitializeComponent();      
        }

        public void OnViewSwitched(object state)
        {
            if (state == null)
            {
                return;
            }

            boardImage = (System.Drawing.Image) state;

            game = new SlidingPuzzleGame(boardImage, (int)TheCanvas.Width, (int)TheCanvas.Height, rows, columns);

            RenderCanvas(0);
        }

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
                //g.DrawString("You Win!", new Font("Arial", 36), new SolidBrush(System.Drawing.Color.Black), 98, 98);
                //g.DrawString("You Win!", new Font("Arial", 36), new SolidBrush(System.Drawing.Color.White), 100, 100);

                using (Font font1 = new Font("Arial", 48, GraphicsUnit.Point))
                {
                    System.Drawing.Rectangle rect1 = new System.Drawing.Rectangle(-3, -3, (int)TheCanvas.Width, (int)TheCanvas.Height);
                    System.Drawing.Rectangle rect2 = new System.Drawing.Rectangle(0, 0, (int)TheCanvas.Width, (int)TheCanvas.Height);

                    // Create a StringFormat object with the each line of text, and the block
                    // of text centered on the page.
                    StringFormat stringFormat = new StringFormat();
                    stringFormat.Alignment = StringAlignment.Center;
                    stringFormat.LineAlignment = StringAlignment.Center;

                    g.DrawString("You Win!", font1, System.Drawing.Brushes.Black, rect1, stringFormat);
                    

                    g.DrawString("You Win!", font1, System.Drawing.Brushes.White, rect2, stringFormat);
                    g.DrawRectangle(Pens.Black, System.Drawing.Rectangle.Round(rect2));
                }
            }

            g.Dispose();

            ImageSource bitmapSource = Imaging.CreateBitmapSourceFromHBitmap(theRender.GetHbitmap(),
                                                                                  IntPtr.Zero,
                                                                                  Int32Rect.Empty,
                                                                                  BitmapSizeOptions.FromEmptyOptions()
                  );
            theRender.Dispose();

            //TheCanvas.Children.Add(bitmap);

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

            if (result == 2)
            {
                // Player won game
            }
        }

        private void NewGameButton_Click(object sender, RoutedEventArgs e)
        {
            game = new SlidingPuzzleGame(boardImage, (int)TheCanvas.Width, (int)TheCanvas.Height, rows, columns);
            RenderCanvas(0);
        }

        private void QuitButton_Click(object sender, RoutedEventArgs e)
        {
            game = null;

            ViewSwitcher.Switch("main_menu");
        }

    }
}
