using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace LudumDare41.Graphics
{
    public class Sprite : Entity
    {
        protected Texture2D _texture;
        protected Rectangle _hitbox;
        protected Texture2D _hitboxTexture;

        

        public Sprite(Texture2D texture, Vector2 position) : base(position)
        {
            _texture = texture;
            _hitbox = new Rectangle((int)Position.X, (int)Position.Y, Texture.Width, Texture.Height);
            _hitboxTexture = Utils.CreateContouringTexture(Hitbox.Width, Hitbox.Height, Color.Green);
        }

        public Texture2D Texture
        {
            get => _texture;
            set => _texture = value;
        }

        public Rectangle Hitbox
        {
            get => _hitbox;
            set => _hitbox = value;
        }

        protected void UpdateHitbox(Vector2 position)
        {
            _hitbox.X = (int) position.X;
            _hitbox.Y = (int) position.Y;

        }

        public virtual void Update(float time)
        {

        }

        public virtual void Draw(SpriteBatch batch)
        {
            batch.Draw(Texture, Position, Color.White);

            batch.Draw(_hitboxTexture, _hitbox, Color.White);
        }

    }
}
