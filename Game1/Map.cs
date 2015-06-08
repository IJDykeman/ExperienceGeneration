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

        public Tiles.Types getTile(TileLoc loc)
        {
            return tileArray[loc.x, loc.y];
        }

        public void setTile(int x, int y, Tiles.Types nTile)
        {
            if (withinMap(new TileLoc(x, y)))
            {
                tileArray[x, y] = nTile;
            }
        }



        public bool withinMap(TileLoc loc)
        {
            return loc.x >= 0 && loc.x < width && loc.y >= 0 && loc.y < height;
        }




        public bool isOpaque(TileLoc loc)
        {
            if (withinMap(loc))
            {
                return Tiles.isOpaque(getTile(loc));
            }
            return false;
        }
    }
}
