using Microsoft.Xna.Framework;
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

        public static bool operator == (TileLoc value1, TileLoc value2)
        {
            return value1.x == value2.x && value1.y == value2.y;
        }

        public static bool operator !=(TileLoc value1, TileLoc value2)
        {
            return !(value1 == value2);
        }

        public static TileLoc operator + (TileLoc value1, TileLoc value2)
        {
            return new TileLoc(value1.x - value2.x, value1.y - value2.y);
        }

        public static float distance(TileLoc l1, TileLoc l2)
        {
            return (float)Math.Sqrt(Math.Pow((l1.x - l2.x), 2) + Math.Pow((l1.y - l2.y), 2));
        }
    }
}
