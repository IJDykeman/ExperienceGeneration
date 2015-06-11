using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Game1
{
    class World
    {
        Map map;
        int pixelWidthOfTile;
        HashSet<TileLoc> highlightedSquares;
        TileMetrics metrics = new TileMetrics(new TileLoc(0, 0));

        public World(int width, int height, int npixelWidthOfTile)
        {
            map = new Map(width, height);
            new DrunkenLeapingGenerator().fill(map);
            pixelWidthOfTile = npixelWidthOfTile;
            highlightedSquares = new HashSet<TileLoc>();
        }

        public void draw(SpriteBatch spriteBatch, GraphicsDeviceManager graphics)
        {
            Drawer.draw(map, spriteBatch, graphics, tileWidth: pixelWidthOfTile);
            foreach (TileLoc highlighted in highlightedSquares){
                Vector2 screenLoc = toScreenLoc(highlighted);
                int rectanglePadding = 6;
                Primitives.drawRectangle(new Rectangle((int)screenLoc.X + rectanglePadding, (int)screenLoc.Y + rectanglePadding,
                    pixelWidthOfTile - rectanglePadding * 2, pixelWidthOfTile - rectanglePadding*2), Color.LightGreen, spriteBatch, graphics.GraphicsDevice);
                
            }
            int i=1;
            Dictionary<Tiles.Types,float> ratios = metrics.getTileRatios();
            int barHeight = 15;
            int barWidth = 700;
            Primitives.drawRectangle(new Rectangle(0, 0, barWidth+20,barHeight*ratios.Keys.Count+20), Color.White, spriteBatch, graphics.GraphicsDevice);
            foreach (Tiles.Types type in ratios.Keys){

                Primitives.drawRectangle(new Rectangle(0, i * barHeight, (int)(ratios[type] * barWidth), barHeight), Tiles.colorOf(type), spriteBatch, graphics.GraphicsDevice);
                i++;
            }
            
        }

        public void update(Vector2 mouseLoc)
        {
            TileLoc mouseTile = toMapLoc(mouseLoc);
            IEnumerable<Tile> toHighlight = tilesVisibleFrom(mouseTile);
            metrics = new TileMetrics(mouseTile, toHighlight);


            foreach (Tile tile in toHighlight)
            {
                highlightSquare(tile.loc);
            }
        }

        public Vector2 toScreenLoc(TileLoc loc)
        {
            return new Vector2(loc.x * pixelWidthOfTile, loc.y * pixelWidthOfTile);
        }

        public TileLoc toMapLoc(Vector2 screenSpaceLoc)
        {
            return new TileLoc((int)(screenSpaceLoc.X / pixelWidthOfTile), (int)(screenSpaceLoc.Y / pixelWidthOfTile));
        }

        public void highlightSquare(TileLoc toHighlight)
        {
            highlightedSquares.Add(toHighlight);
        }

        public void update()
        {
            highlightedSquares.Clear();
        }

        //<summary>Returns list of squares between from and to, starting at from.</summary>
        public List<Tile> tilesOnLine(TileLoc from, TileLoc to)
        {
            List<Tile> result = new List<Tile>();
            foreach (Tile tile in tilesOnLineIterator(from,to)){
                result.Add(tile);
            }
            return result;
        }

        public static IEnumerable tileLocsOnLineIterator(TileLoc from, TileLoc to, Map mapToUse)
        {
            
            int x = from.x;
            int y = from.y;
            int x2 = to.x;
            int y2 = to.y;
            int w = x2 - x;
            int h = y2 - y;
            int dx1 = 0, dy1 = 0, dx2 = 0, dy2 = 0;
            if (w < 0) dx1 = -1; else if (w > 0) dx1 = 1;
            if (h < 0) dy1 = -1; else if (h > 0) dy1 = 1;
            if (w < 0) dx2 = -1; else if (w > 0) dx2 = 1;
            int longest = Math.Abs(w);
            int shortest = Math.Abs(h);
            if (!(longest > shortest))
            {
                longest = Math.Abs(h);
                shortest = Math.Abs(w);
                if (h < 0) dy2 = -1; else if (h > 0) dy2 = 1;
                dx2 = 0;
            }
            int numerator = longest >> 1;
            for (int i = 0; i <= longest; i++)
            {

                yield return new TileLoc(x, y);

                numerator += shortest;
                if (!(numerator < longest))
                {
                    numerator -= longest;
                    x += dx1;
                    y += dy1;
                }
                else
                {
                    x += dx2;
                    y += dy2;
                }
            }
        }

        public IEnumerable tilesOnLineIterator(TileLoc from, TileLoc to)
        {
            foreach (TileLoc loc in tileLocsOnLineIterator(from,to,map)){
                if (map.withinMap(loc))
                {
                    yield return getTileFromTypeAndLoc(loc);
                }
            }
             
        }

        bool tileVisibleFrom(TileLoc eye, TileLoc target)
        {

            List<Tile> sightLine = tilesOnLine(eye, target);
            for (int i=0; i<sightLine.Count-1; i++){
                if (map.isOpaque(sightLine[i].loc)){
                    return false;
                }
                else if (i < sightLine.Count - 1 && sightLine[i + 1].loc.x != sightLine[i].loc.x && sightLine[i + 1].loc.y != sightLine[i].loc.y)
                {
                    //preventing the sightline from leaking through a 
                    //OX or XO
                    //XO    OX
                    //style corner
                    TileLoc cornerSide1 = new TileLoc(sightLine[i + 1].loc.x, sightLine[i].loc.y);
                    if (map.isOpaque(cornerSide1))
                    {
                        TileLoc cornerSide2 = new TileLoc(sightLine[i].loc.x, sightLine[i + 1].loc.y);
                        if (map.isOpaque(cornerSide2))
                        {
                            return false;
                        }
                    }
                }
            }
            return true;
        }

        public Tile getTileFromTypeAndLoc(TileLoc loc)
        {
            return Tiles.getTileFromType(map.getTile(loc.x, loc.y), loc);
        }

        public IEnumerable<Tile> tilesVisibleFrom(TileLoc eye)
        {
            int searchRadius = 15;  //currently the search is a square
            for (int x = eye.x - searchRadius; x <= eye.x + searchRadius; x++)
            {
                for (int y = eye.y - searchRadius; y <= eye.y + searchRadius; y++)
                {
                    TileLoc searchLoc = new TileLoc(x,y);
                    if (map.withinMap(searchLoc))
                    {
                        switch (map.getTile(x, y))
                        {
                            case Tiles.Types.floor:

                                if (tileVisibleFrom(eye, searchLoc))
                                {
                                    yield return getTileFromTypeAndLoc(new TileLoc(x, y));
                                }
                                break;
                            default:
                                if (tileVisibleFrom(eye, searchLoc))
                                {
                                    yield return getTileFromTypeAndLoc(new TileLoc(x, y));
                                }
                                break;
                        }

                    }
            }
        }
        }

    }
}
