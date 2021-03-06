﻿using LudumDare41.Graphics;
using LudumDare41.Screens;
using LudumDare41.Utility;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace LudumDare41.ShooterPhase
{
    public class CrossAim : Sprite
    {
        private Player _player;

        private const int Distance = 300;

        public CrossAim(Texture2D texture, Vector2 position, Player player) : base(texture, position)
        {
            _player = player;
        }

        public void Update(GameTime time)
        {
            float elapsedGameTimeMillis = time.ElapsedGameTime.Milliseconds;

            //Déplacement circulaire
            /*Vector2 mouseDistanceFromPlayer = new Vector2(Input.MousePos.X - _player.Position.X, Input.MousePos.Y - _player.Position.Y);
            mouseDistanceFromPlayer.Normalize();
            float angleRadians = (float)Math.Atan2(mouseDistanceFromPlayer.Y, mouseDistanceFromPlayer.X);
            
            Position = new Vector2(_player.Position.X - (float)Texture.Width/2 + ((float)Math.Cos(angleRadians)*Distance),
            _player.Position.Y - (float)Texture.Height/2 + ((float)Math.Sin(angleRadians)*Distance));*/

            Position = new Vector2(Input.MousePos.X - (float)Texture.Width / 2, Input.MousePos.Y - (float)Texture.Height / 2);
            ClampWithOffset(50);




            base.Update(elapsedGameTimeMillis);
        }

        private void ClampWithOffset(int offset)
        {
            if (Position.X < offset)
                Position.X = offset;
            if (Position.X > Utils.WIDTH - offset - Texture.Width)
                Position.X = Utils.WIDTH - offset - Texture.Width;
            if (Position.Y < offset)
                Position.Y = offset;
            if (Position.Y > Utils.HEIGHT - offset - Texture.Height)
                Position.Y = Utils.HEIGHT - offset - Texture.Height;
        }

        public new void Draw(SpriteBatch spriteBatch)
        {
            ShooterScreen tempScreen = (ShooterScreen)Main.CurrentsScreens[0];
            spriteBatch.Draw(Texture, tempScreen.Camera.ScreenToWorld(Position), Color.White);
        }
    }
}