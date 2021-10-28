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
        private Random rand;

        private Tile[,] board;

        private Image boardImage;
        private Tile lastTile;

        private int boardSizeX;
        private int boardSizeY;

        private int rows;
        private int columns;

        private int tileSizeX;
        private int tileSizeY;

        private bool initialized = false;
        private bool running = false;

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
            rand = new Random();

            board = new Tile[rows, columns];

            tileSizeX = boardSizeX / columns;
            tileSizeY = boardSizeY / rows;

            int index = 0;

            for (int y = 0; y < rows; y++)
            {
                for (int x = 0; x < columns; x++)
                {
                    Image tileImage = new Bitmap(tileSizeX, tileSizeY);

                    var g = Graphics.FromImage(tileImage);
                    g.DrawImage(boardImage, new Rectangle(0, 0, tileSizeX, tileSizeY), new Rectangle(x * tileSizeX, y * tileSizeY, tileSizeX, tileSizeY), GraphicsUnit.Pixel);

                    // Create tiles but leave bottom right empty
                    if (index != rows * columns - 1)
                    {
                        board[x, y] = new Tile(tileImage, index);
                    }
                    else
                    {
                        lastTile = new Tile(tileImage, index);
                    }

                    g.Dispose();

                    index++;
                }
            }

            ScrambleTiles();

            running = true;
            initialized = true;
        }

        // Randomly scramble tiles
        private void ScrambleTiles()
        {
            for (int i = 0; i < 20; i++)
            {
                int tileX = rand.Next(0, columns);
                int tileY = rand.Next(0, rows);

                TryMoveTile(tileX, tileY);
            }
        }

        private int[] GetDifferentTilePos(int tileX, int tileY)
        {
            for (int x = -1; x < 2; x += 2)
            {
                int xPos = tileX + x;
                int yPos = tileY;

                if (xPos < columns && xPos >= 0)
                {
                    return new int[] { xPos, yPos };
                }
            }

            for (int y = -1; y < 2; y += 2)
            {
                int xPos = tileX;
                int yPos = tileY + y;

                if (yPos < rows && yPos >= 0)
                {
                    return new int[] { xPos, yPos };
                }
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
        
        // For debugging purposes
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

        // Returns null if cannot be moved
        // or the position of the free space
        private int[] CanMoveTile(int tileX, int tileY)
        {
            for (int x = -1; x < 2; x += 2)
            {
                int xPos = tileX + x;
                int yPos = tileY;

                if (xPos < columns && xPos >= 0)
                {
                    if (board[xPos, yPos] == null)
                    {
                        return new int[] { xPos, yPos };
                    }
                }
            }

            for (int y = -1; y < 2; y += 2)
            {
                int xPos = tileX;
                int yPos = tileY + y;

                if (yPos < rows && yPos >= 0)
                {
                    if (board[xPos, yPos] == null)
                    {
                        return new int[] { xPos, yPos };
                    }
                }
            }

            return null;
        }

        // Called when mouse clicked on the board
        // returns an integer value based on the result of moving the tile
        // 0 == couldn't move tile
        // 1 == moved tile succesfully
        // 2 == won the game
        public int ClickedOnBoard(int mouseX, int mouseY)
        {
            int tileX = Math.Clamp(mouseX / tileSizeX, 0, columns - 1);
            int tileY = Math.Clamp(mouseY / tileSizeY, 0, rows - 1);

            return TryMoveTile(tileX, tileY);
        }

        // Tries to move the specified tile
        // returns an integer value based on the result of moving the tile
        // 0 == couldn't move tile
        // 1 == moved tile succesfully
        // 2 == won the game
        public int TryMoveTile(int tileX, int tileY)
        {
            // If game ended then just return false
            if (!running && initialized)
            {
                return 0;
            }

            int[] freeSpace = CanMoveTile(tileX, tileY);

            if (freeSpace != null)
            {
                // Place tile at empty space

                board[freeSpace[0], freeSpace[1]] = board[tileX, tileY];
                board[tileX, tileY] = null;
              
                if (initialized)
                {
                    // Check if won and return value based on result
                    bool won = CheckWin();
                    return won ? 2 : 1;
                }
                else
                {
                    return 1;
                }

            }

            return 0;
        }

        // Check if all tiles are in correct order to determine if won game
        public bool CheckWin()
        {
            int index = 0;

            for (int y = 0; y < rows; y++)
            {
                for (int x = 0; x < columns; x++)
                {
                    if (index != rows * columns - 1)
                    {
                        if (board[x, y] == null || board[x, y].Index != index)
                        {
                            return false;
                        }
                    }

                    index++;
                }
            }

            // If here, game won and draw last tile!

            board[columns - 1, rows - 1] = lastTile;
            running = false;

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
