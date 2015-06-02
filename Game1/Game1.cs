using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;

namespace Game1
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        Map world;
        int screenWidth = 1500;
        int screenHeight = 800;
        int pixelWidthOfTile = 20;

        public Game1()
        {
            
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            world = new Map(screenWidth/pixelWidthOfTile, screenHeight/pixelWidthOfTile);
        }

        protected override void Initialize()
        {

            base.Initialize();
            graphics.PreferredBackBufferWidth = screenWidth;
            graphics.PreferredBackBufferHeight = screenHeight;   
            graphics.ApplyChanges();
            world.initialize();
            Primitives.initialize(graphics.GraphicsDevice);
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);
        }
        protected override void UnloadContent()
        {
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();


            base.Update(gameTime);
        }


        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here
            spriteBatch.Begin();
            Drawer.draw(world, spriteBatch, graphics, tileWidth: pixelWidthOfTile);
            Primitives.drawRectangle(new Rectangle(Mouse.GetState().X, Mouse.GetState().Y,30,30),Color.White, spriteBatch, graphics.GraphicsDevice);
            spriteBatch.End();
           
            base.Draw(gameTime);
        }
    }
}
