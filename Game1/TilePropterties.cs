using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Game1
{
    struct TilePropterties
    {
        public Color color;
        public bool opaque;
        Tiles.Types type;
        public TilePropterties(Tiles.Types ntype, Color nColor, bool nOpaque)
        {
            type = ntype;
            color = nColor;
            opaque = nOpaque;
        }

        public Tiles.Types getType()
        {
            return type;
        }
    }
}
