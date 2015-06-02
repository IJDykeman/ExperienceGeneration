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

        public HashSet<TileLoc> TilesOnLine (TileLoc p0, TileLoc p1){
            HashSet<TileLoc> result = new HashSet<TileLoc>();
            float dX = p1.x - p0.x;
            float dY = p1.y - p0.y;
            float error = 0;
            float deltaerr = Math.Abs(dY / dX);
            int y = p0.y;

            for (int x = p0.x; x <= p1.x; x++)
            {
                result.Add(new TileLoc(x, y));
                error = error + deltaerr;
                while (error >= .5f)
                {
                    result.Add(new TileLoc(x, y));
                    y = y + Math.Sign(p1.y - p0.y);
                    error = error - 1;
                }
            }
            return result;
        }



    }
}
