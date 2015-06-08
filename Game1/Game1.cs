﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;

namespace Game1
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        World world;
        int screenWidth = 1500;
        int screenHeight = 800;

        public Game1()
        {
            
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            int pixelWidthOfTile = 20;
            world = new World(screenWidth / pixelWidthOfTile, screenHeight / pixelWidthOfTile, pixelWidthOfTile);
        }

        protected override void Initialize()
        {

            base.Initialize();
            graphics.PreferredBackBufferWidth = screenWidth;
            graphics.PreferredBackBufferHeight = screenHeight;   
            graphics.ApplyChanges();
            this.IsMouseVisible = true;
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

            world.update();
            base.Update(gameTime);
        }


        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            spriteBatch.Begin();
            Vector2 mouseLoc = new Vector2(Mouse.GetState().X, Mouse.GetState().Y);
            world.update(mouseLoc);
            world.highlightSquare(world.toMapLoc(new Vector2(Mouse.GetState().X, Mouse.GetState().Y)));

            world.draw(spriteBatch, graphics);
            spriteBatch.End();
           
            base.Draw(gameTime);
        }
    }
}
