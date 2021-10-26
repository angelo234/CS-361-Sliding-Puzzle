﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace CS_361_Sliding_Puzzle
{
    public class Tile
    {
        public Image TileImage { get; }
        public int Index { get; }

        public Tile(Image tileImage, int index)
        {
            TileImage = tileImage;
            Index = index;
        }
    }
}
