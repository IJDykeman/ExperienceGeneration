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
        public bool interesting;
        Tiles.Types type;
        public TilePropterties(Tiles.Types ntype, Color nColor, bool nOpaque, bool ninteresting)
        {
            type = ntype;
            color = nColor;
            opaque = nOpaque;
            interesting = ninteresting;
        }

        public Tiles.Types getType()
        {
            return type;
        }
    }
}
