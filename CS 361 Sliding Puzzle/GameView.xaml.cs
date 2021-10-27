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
        private SlidingPuzzleGame game;

        private int rows = 4;
        private int columns = 4; 

        public GameView(System.Drawing.Image image)
        {
            InitializeComponent();
            
            game = new SlidingPuzzleGame(image, (int)TheCanvas.Width, (int)TheCanvas.Height, rows, columns);

            // Testing purposes

            /*
            game.TryMoveTile(2, 3);
            game.PrintBoard();

            game.TryMoveTile(2, 2);
            game.PrintBoard();

            game.TryMoveTile(1, 1);
            game.PrintBoard();

            */

            game.PrintBoard();

            RenderCanvas();
        }



        public void OnViewSwitched(object state)
        {
            throw new NotImplementedException();
        }

        private void RenderCanvas()
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

            game.ClickedOnBoard(mouseX, mouseY);

            RenderCanvas();
        }
    }
}
