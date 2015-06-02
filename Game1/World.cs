using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Game1
{
   
    class Map
    {
        int width=10;
        int height=10;
        Tiles.Types[,] tileArray;

        public Map(int nWidth, int nHeight)
        {
            width = nWidth;
            height = nHeight;
            tileArray = new Tiles.Types[width,height];
            

        }

        public void initialize()
        {
            new WeightedRandomGenerator().fill(this);
        }

        public int getWidth()
        {
            return width;
        }

        public int getHeight()
        {
            return height;
        }

        public Tiles.Types getTile(int x, int y)
        {
            return tileArray[x, y];
        }
        public void setTile(int x, int y, Tiles.Types nTile)
        {
            tileArray[x, y] = nTile;
        }



    }
}
