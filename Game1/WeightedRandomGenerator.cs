using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Game1
{
    class WeightedRandomGenerator : WorldGenerator
    {
        public override void fill(Map world){

            Dictionary<Tiles.Types, float> weights = new Dictionary<Tiles.Types, float>
            {
                { Tiles.Types.floor, .78f }, 
                { Tiles.Types.monster, .01f }, 
                { Tiles.Types.treasure, .01f }, 
                { Tiles.Types.wall, .2f }
            };

            for (int x = 0; x < world.getWidth(); x++)
            {
                for (int y = 0; y < world.getHeight(); y++)
                {
                    world.setTile(x, y, getWeightedRandomTile(weights));
                }
            }
        }
    }
}
