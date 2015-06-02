using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Game1
{
    class Primitives
    {
        private static Texture2D singlePixelTexture;
        public static void initialize(GraphicsDevice device)
        {
            singlePixelTexture = new Texture2D(device, 1, 1);
            singlePixelTexture.SetData(new[] { Color.White });
        }

        public static void drawRectangle(Rectangle rect, Color color, SpriteBatch sBatch, GraphicsDevice device)
        {
            
            sBatch.Draw(singlePixelTexture, rect, color);
        }
    }
}
