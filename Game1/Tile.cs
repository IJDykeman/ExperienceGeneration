using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Game1
{
    struct Tile
    {
        public TilePropterties properties;
        public TileLoc loc;
        public Tile(TileLoc nloc, TilePropterties nproperties)
        {
            loc = nloc;
            properties = nproperties;
        }

        public Tiles.Types getTileType()
        {
            return properties.getType();
        }
    }
}
