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
                    if(index != rows * columns - 1)
                    {
                        board[x, y] = new Tile(null, index);

                        System.Diagnostics.Debug.WriteLine(index);
                    }

                    index++;
                }
            }

            bool win = CheckWin();

            System.Diagnostics.Debug.WriteLine(win);
        }

        public bool CheckWin()
        {
            int index = 0;

            for(int y = 0; y < rows; y++)
            {
                for(int x = 0; x < columns; x++)
                {
                    if(index != rows * columns - 1 && board[x, y].Index != index)
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
