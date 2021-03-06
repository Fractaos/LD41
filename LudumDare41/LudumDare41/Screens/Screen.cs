﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace LudumDare41.Screens
{
    public abstract class Screen
    {
        protected SpriteBatch spriteBatch;





        public Screen()
        {
            spriteBatch = new SpriteBatch(Main.Device);
        }

        public SpriteBatch SpriteBatch
        {
            get => spriteBatch;
        }

        public abstract void Create();

        public abstract void Update(GameTime time);

        public abstract void Draw();

        public abstract void Resume();

        public void Dispose()
        {
            spriteBatch.Dispose();
        }
    }


}
