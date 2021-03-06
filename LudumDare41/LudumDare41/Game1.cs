﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Runtime.InteropServices;

namespace LudumDare41
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Game1 : Game
    {
        GraphicsDeviceManager _graphics;

        Main _main;

        [DllImport("user32.dll")]
        static extern void ClipCursor(ref Rectangle rect);

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";

        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            _main = new Main(_graphics, this);
            _main.Initialize();
            base.Initialize();
        }

        protected override void LoadContent()
        {

        }

        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();
            if (IsActive)
            {
                Rectangle rec = Window.ClientBounds;
                rec.Width += rec.X;
                rec.Height += rec.Y;
                ClipCursor(ref rec);
            }

            _main.Update(gameTime);
            // TODO: Add your update logic here

            base.Update(gameTime);
        }


        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here
            _main.Draw();
            base.Draw(gameTime);
        }
    }
}
