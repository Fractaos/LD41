using System;
using LudumDare41.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace LudumDare41.ShooterPhase
{
    public class Bullet : Sprite
    {
        private float _speed;
        private Vector2 _direction;

        public Bullet(Texture2D texture, Vector2 position, float speed, Vector2 direction) : base(texture, position)
        {
            _speed = speed;
            _direction = direction;
        }

        public bool ToDestroy { get; set; } = false;

        public void Update(GameTime time)
        {
            Position.X += _direction.X * _speed;
            Position.Y += _direction.Y * _speed;
            
            if (Position.X > Utils.WIDTH || Position.X < 0 || Position.Y > Utils.HEIGHT || Position.Y < 0)
            {
                ToDestroy = true;
            }
        }

        public override void Draw(SpriteBatch spriteBatch)
        {

            base.Draw(spriteBatch);
        }
    }
}