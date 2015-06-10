using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Game1
{
    class TileMetrics
    {
        Dictionary<Tiles.Types, float> visibleTileCounts;
        TileLoc loc;

        public TileMetrics(TileLoc nloc)
        {
            visibleTileCounts = new Dictionary<Tiles.Types, float>();
            loc = nloc;
        }

        public TileMetrics(TileLoc nloc, Dictionary<Tiles.Types, float> nvisibleTileCounts)
        {
            loc = nloc;
            visibleTileCounts = nvisibleTileCounts;
        }

        public TileMetrics(TileLoc nloc, IEnumerable tilesSeen)
        {
            loc = nloc;
            visibleTileCounts = new Dictionary<Tiles.Types, float>();
            foreach (Tile tile in tilesSeen)
            {
                recordSeenTile(tile.getTileType(), 100.0f/(float)Math.Pow(TileLoc.distance(nloc,tile.loc),2));
            }
        }

        public void recordSeenTile(Tiles.Types type, float weight)
        {
            if (type == Tiles.Types.floor){
                return;
            }
            if (!visibleTileCounts.ContainsKey(type))
            {
                visibleTileCounts[type] = weight;
            }
            else
            {
                visibleTileCounts[type] += weight;
            }
        }

        public float getEmotionalWeight(Tiles.Types type)
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

        public Dictionary<Tiles.Types, float> getTileRatios()
        {
            Dictionary<Tiles.Types, float> result = new Dictionary<Tiles.Types, float>();
            float totalTilesSeen = 0;
            foreach (Tiles.Types type in allTypesSeen()){
                totalTilesSeen += getEmotionalWeight(type);
                result[type] = getEmotionalWeight(type);
            }
            //normalize counts to get ratios
            foreach (Tiles.Types type in allTypesSeen())
            {
                result[type] /= (float)totalTilesSeen;
            }
            return result;
        }

        public IEnumerable<Tiles.Types> allTypesSeen()
        {
            return visibleTileCounts.Keys.AsEnumerable<Tiles.Types>();
        }
    }
}
