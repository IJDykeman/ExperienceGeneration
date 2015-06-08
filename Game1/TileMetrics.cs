using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Game1
{
    class TileMetrics
    {
        Dictionary<Tiles.Types, int> visibleTileCounts;
        TileLoc loc;

        public TileMetrics(TileLoc nloc)
        {
            visibleTileCounts = new Dictionary<Tiles.Types, int>();
            loc = nloc;
        }

        public TileMetrics(TileLoc nloc, Dictionary<Tiles.Types, int> nvisibleTileCounts)
        {
            loc = nloc;
            visibleTileCounts = nvisibleTileCounts;
        }

        public TileMetrics(TileLoc nloc, IEnumerable tilesSeen)
        {
            loc = nloc;
            visibleTileCounts = new Dictionary<Tiles.Types, int>();
            foreach (Tile tile in tilesSeen)
            {
                recordSeenTile(tile.getTileType());
            }
        }

        public void recordSeenTile(Tiles.Types type)
        {
            if (!visibleTileCounts.ContainsKey(type))
            {
                visibleTileCounts[type] = 1;
            }
            else
            {
                visibleTileCounts[type]++;
            }
        }

        public int getNumSeen(Tiles.Types type)
        {
            if (!visibleTileCounts.ContainsKey(type))
            {
                return 0;
            }
            else
            {
                return visibleTileCounts[type];
            }
        }

        public IEnumerable<Tiles.Types> allTypesSeen()
        {
            return visibleTileCounts.Keys.AsEnumerable<Tiles.Types>();
        }
    }
}
