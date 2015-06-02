using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Game1
{
    class RandomGenerator : WorldGenerator
    {
        public override void fill(Map world){
            for (int x = 0; x < world.getWidth(); x++)
            {
                for (int y = 0; y < world.getHeight(); y++)
                {
                    world.setTile(x, y, getRandomTile());
                }
            }
        }
    }
}
