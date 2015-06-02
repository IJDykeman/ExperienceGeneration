using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Game1
{
    class Tiles
    {
        public enum Types
        {
            floor,
            wall,
            monster,
            treasure
        }

        static Dictionary<int, TilePropterties> tileAttributes = new Dictionary<int, TilePropterties>();

        static Tiles()
        {
            tileAttributes[(int)Types.floor] = new TilePropterties(Color.Gray);
            tileAttributes[(int)Types.wall] = new TilePropterties(Color.Black);
            tileAttributes[(int)Types.monster] = new TilePropterties(Color.Green);
            tileAttributes[(int)Types.treasure] = new TilePropterties(Color.Gold);
        }

        public static Array getAllTileTypes(){
            return  Enum.GetValues(typeof(Tiles.Types));
        }

        public static Color colorOf(Types type)
        {
            return tileAttributes[(int)type].color;
        }
    }
}
