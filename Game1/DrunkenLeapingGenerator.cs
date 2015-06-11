using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Game1
{
    class DrunkenLeapingGenerator : WorldGenerator
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
                    world.setTile(x, y, Tiles.Types.wall);
                }
            }

            placeFloorTiles(world);
            placeSpecialTiles(world);

        }

        private static void placeSpecialTiles(Map world)
        {
            Random rand = new Random();
            for (int x = 0; x < world.getWidth(); x++)
            {
                for (int y = 0; y < world.getHeight(); y++)
                {
                    if (world.getTile(x, y) == Tiles.Types.floor)
                    {
                        if (rand.NextDouble() > .995)
                        {
                            world.setTile(x, y, Tiles.Types.monster);
                        }
                        if (rand.NextDouble() > .995)
                        {
                            world.setTile(x, y, Tiles.Types.treasure);
                        }
                    }
                }
            }
        }

        private static void placeFloorTiles(Map world)
        {
            Random random = new Random();
            float dx = random.Next(world.getWidth());
            float dy = random.Next(world.getHeight());
            
            for (int i = 0; i < 2000; i++)
            {
                double walkAngle = random.NextDouble() * Math.PI * 2;
                float jumpLength = 1.2f;
                if (random.NextDouble() > .94) //creates the occasional corridor between rooms
                {
                    jumpLength = (float)random.NextDouble() * 20;
                }
                float ndx = dx + Convert.ToInt32(Math.Cos(walkAngle) * jumpLength);
                float ndy = dy + Convert.ToInt32(Math.Sin(walkAngle) * jumpLength);

                foreach (TileLoc loc in World.tileLocsOnLineIterator(new TileLoc((int)dx, (int)dy), new TileLoc((int)ndx, (int)ndy), world))
                {
                    TileLoc[] floorLocationsToAdd = new TileLoc[] { loc, loc + new TileLoc(1, 0), loc + new TileLoc(-1, 0), loc + new TileLoc(0, 1), loc + new TileLoc(0, -1) };
                    foreach (TileLoc placement in floorLocationsToAdd){
                        world.setTile(placement.x, placement.y, Tiles.Types.floor);
                    }
                }

                dx = ndx;
                dy = ndy;
                if (!world.withinMap(new TileLoc(Convert.ToInt32(ndx), Convert.ToInt32(ndy))) || random.NextDouble() > .99)
                {
                    dx = random.Next(world.getWidth());
                    dy = random.Next(world.getHeight());
                }
            }
        }
    }
}
