using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace CS_361_Sliding_Puzzle
{
    public class SlidingPuzzleGame
    {
        private Image boardImage;

        private Tile[,] board;

        private int rows;
        private int columns;


        public SlidingPuzzleGame(Image boardImage, int rows, int columns)
        {
            this.boardImage = boardImage;
            this.rows = rows;
            this.columns = columns;

            // Initialize the game
            InitGame();
        }

        private void InitGame()
        {
            board = new Tile[rows, columns];

            int index = 0;

            for (int y = 0; y < rows; y++)
            {
                for (int x = 0; x < columns; x++)
                {
                    // ASSIGN IMAGE TO TILES LATER (null)

                    // Create tiles but leave bottom right empty
                    if (index != rows * columns - 1)
                    {
                        board[x, y] = new Tile(null, index);
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

    }
}
