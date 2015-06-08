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
            tileAttributes[(int)Types.floor] = new TilePropterties(Types.floor, Color.Gray, false);
            tileAttributes[(int)Types.wall] = new TilePropterties(Types.wall, Color.Black, true);
            tileAttributes[(int)Types.monster] = new TilePropterties(Types.monster, Color.Green, false);
            tileAttributes[(int)Types.treasure] = new TilePropterties(Types.treasure, Color.Gold, false);
        }

        public static Array getAllTileTypes(){
            return  Enum.GetValues(typeof(Tiles.Types));
        }

        public static Color colorOf(Types type)
        {
            return tileAttributes[(int)type].color;
        }

        public static bool isOpaque(Types type)
        {
            return tileAttributes[(int)type].opaque;
        }
         
        public static Tile getTileFromType(Types type, TileLoc loc){
            return new Tile(loc,tileAttributes[(int)type]);
        }
    }
}
