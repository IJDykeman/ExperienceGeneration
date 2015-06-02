using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Game1
{
    static class Drawer
    {
        public static void draw(Map world, SpriteBatch spriteBatch, GraphicsDeviceManager graphics, int tileWidth = 10)
        {
            for (int x = 0; x < world.getWidth(); x++)
            {
                for (int y = 0; y < world.getHeight(); y++)
                {
                    Color color = Tiles.colorOf(world.getTile(x, y));
                    Primitives.drawRectangle(new Rectangle(x * tileWidth, y * tileWidth, tileWidth,tileWidth), color, spriteBatch, graphics.GraphicsDevice);
                }
            }
        }
    }
}
