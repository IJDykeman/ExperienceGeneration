using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Game1
{
    abstract class WorldGenerator
    {
        public abstract void fill(Map world);
        static Random rnd = new Random();
        protected Tiles.Types getRandomTile()
        {
            
            Tiles.Types type = (Tiles.Types)Tiles.getAllTileTypes().GetValue(rnd.Next(Tiles.getAllTileTypes().Length));
            return type;
        }

        protected Tiles.Types getWeightedRandomTile(Dictionary<Tiles.Types,float> weights)
        {
            float choice = (float)rnd.NextDouble();
            foreach (Tiles.Types type in weights.Keys){
                choice -= weights[type];
                if (choice<=0){
                    return type;
                }
            }
            return getRandomTile(); // need to return something in all cases.  This line should never run.
        }
    }
}
