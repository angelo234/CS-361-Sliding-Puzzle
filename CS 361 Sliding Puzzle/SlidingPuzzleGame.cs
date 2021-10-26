using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CS_361_Sliding_Puzzle
{
    public class SlidingPuzzleGame
    {
        private Image boardImage;

        private Tile[,] board;

        private int boardSizeX;
        private int boardSizeY;

        private int rows;
        private int columns;


        public SlidingPuzzleGame(Image boardImage, int boardSizeX, int boardSizeY, int rows, int columns)
        {
            // Resize input image to fit board size
            this.boardImage = ResizeImageSimple(boardImage, new Size(boardSizeX, boardSizeY));
            this.boardSizeX = boardSizeX;
            this.boardSizeY = boardSizeY;
            this.rows = rows;
            this.columns = columns;

            // Initialize the game
            InitGame();
        }

        private void InitGame()
        {
            board = new Tile[rows, columns];

            int tileSizeX = boardSizeX / columns;
            int tileSizeY = boardSizeY / rows;

            int index = 0;

            for (int y = 0; y < rows; y++)
            {
                for (int x = 0; x < columns; x++)
                {
                    // ASSIGN IMAGE TO TILES LATER (null)

                    // Create tiles but leave bottom right empty
                    if (index != rows * columns - 1)
                    {
                        Image tileImage = new Bitmap(tileSizeX, tileSizeY);

                        var g = Graphics.FromImage(tileImage);
                        g.DrawImage(boardImage, new Rectangle(0, 0, tileSizeX, tileSizeY), new Rectangle(x * tileSizeX, y * tileSizeY, tileSizeX, tileSizeY), GraphicsUnit.Pixel);
                        g.Dispose();

                        board[x, y] = new Tile(tileImage, index);
                    }

                    index++;
                }
            }
        }

        // Returns null if cannot be moved
        // or the position of the free space
        private int[] CanMoveTile(int tileX, int tileY)
        {
            for (int x = -1; x < 2; x += 2)
            {
                try
                {
                    int xPos = tileX + x;
                    int yPos = tileY;

                    if (board[xPos, yPos] == null)
                    {
                        return new int[] { xPos, yPos };
                    }
                }
                catch (IndexOutOfRangeException) { }
            }

            for (int y = -1; y < 2; y += 2)
            {
                try
                {
                    int xPos = tileX;
                    int yPos = tileY + y;

                    if (board[xPos, yPos] == null)
                    {
                        return new int[] { xPos, yPos };
                    }
                }
                catch (IndexOutOfRangeException) { }
            }

            return null;
        }

        public Image GetTileImage(int x, int y)
        {
            Tile tile = board[x, y];

            if (tile != null)
            {
                return board[x, y].TileImage;
            }
            
            return null;
        }

        public void PrintBoard()
        {
            for (int y = 0; y < rows; y++)
            {
                for (int x = 0; x < columns; x++)
                {
                    Tile tile = board[x, y];

                    if(tile != null)
                    {
                        System.Diagnostics.Debug.Write(tile.Index);
                    }
                    else
                    {
                        System.Diagnostics.Debug.Write("O");
                    }

                    if (x != columns - 1)
                    {
                        System.Diagnostics.Debug.Write(" | ");
                    }
                }

                System.Diagnostics.Debug.WriteLine("\n--------------");
            }
        }

        public bool TryMoveTile(int tileX, int tileY)
        {
            int[] freeSpace = CanMoveTile(tileX, tileY);
            
            if (freeSpace != null)
            {
                // Place tile at empty space

                board[freeSpace[0], freeSpace[1]] = board[tileX, tileY];
                board[tileX, tileY] = null;

                System.Diagnostics.Debug.WriteLine(board[tileX, tileY]);

                //bool won = CheckWin();

                //Console.WriteLine(won);

                return true;
            }

            System.Diagnostics.Debug.WriteLine("Couldn't move tile");

            return false;
        }

        public bool CheckWin()
        {
            int index = 0;

            for (int y = 0; y < rows; y++)
            {
                for (int x = 0; x < columns; x++)
                {
                    if (index != rows * columns - 1 && board[x, y].Index != index)
                    {
                        return false;
                    }

                    index++;
                }
            }

            return true;
        }

        /// <summary>
        /// https://stackoverflow.com/a/14347746
        /// </summary>
        /// <param name="imgToResize"></param>
        /// <param name="size"></param>
        /// <returns></returns>
        public static Image ResizeImageSimple(Image imgToResize, Size size)
        {
            return (Image)(new Bitmap(imgToResize, size));
        }

        /// <summary>
        /// Credits to: https://stackoverflow.com/a/24199315
        /// Resize the image to the specified width and height.
        /// </summary>
        /// <param name="image">The image to resize.</param>
        /// <param name="width">The width to resize to.</param>
        /// <param name="height">The height to resize to.</param>
        /// <returns>The resized image.</returns>
        private Bitmap ResizeImage(Image image, int width, int height)
        {
            var destRect = new Rectangle(0, 0, width, height);
            var destImage = new Bitmap(width, height);

            destImage.SetResolution(image.HorizontalResolution, image.VerticalResolution);

            using (var graphics = Graphics.FromImage(destImage))
            {
                graphics.CompositingMode = CompositingMode.SourceCopy;
                graphics.CompositingQuality = CompositingQuality.HighQuality;
                graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
                graphics.SmoothingMode = SmoothingMode.HighQuality;
                graphics.PixelOffsetMode = PixelOffsetMode.HighQuality;

                using (var wrapMode = new ImageAttributes())
                {
                    wrapMode.SetWrapMode(WrapMode.TileFlipXY);
                    graphics.DrawImage(image, destRect, 0, 0, image.Width, image.Height, GraphicsUnit.Pixel, wrapMode);
                }
            }

            return destImage;
        }
    }
}
