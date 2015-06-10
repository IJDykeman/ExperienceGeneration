using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Game1
{
    struct TileLoc
    {
        public int x;
        public int y;

        public TileLoc(int nx, int ny)
        {
            x = nx;
            y = ny;
        }

        public static float distance(TileLoc l1, TileLoc l2)
        {
            return (float)Math.Sqrt(Math.Pow((l1.x - l2.x), 2) + Math.Pow((l1.y - l2.y), 2));
        }
    }
}
