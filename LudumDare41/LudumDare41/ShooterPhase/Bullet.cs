using LudumDare41.Graphics;
using LudumDare41.Screens;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace LudumDare41.ShooterPhase
{
    public class Bullet : Sprite
    {
        private float _speed;
        private Vector2 _direction;
        private Sprite _side;
        private Weapon _fromWeapon;

        public Bullet(Texture2D texture, Vector2 position, float speed, Vector2 direction) : base(texture, position)
        {
            _speed = speed;
            _direction = direction;
        }

        public Bullet(Texture2D texture, Vector2 position, float speed, Vector2 direction, Sprite side, Weapon fromWeapon) : this(texture, position, speed, direction)
        {
            _side = side;
            _fromWeapon = fromWeapon;
        }

        public bool ToDestroy { get; set; }

        public Sprite Side
        {
            get => _side;
        }

        public Weapon FromWeapon
        {
            get => _fromWeapon;
        }

        public void Update(GameTime time)
        {
            Position.X += _direction.X * _speed;
            Position.Y += _direction.Y * _speed;
            ShooterScreen tempScreen = (ShooterScreen)Main.CurrentScreen;
            tempScreen.ProcessBulletCollision(this);
            if (Position.X > Utils.WIDTH || Position.X < 0 || Position.Y > Utils.HEIGHT || Position.Y < 0)
            {
                ToDestroy = true;
            }

            UpdateHitbox(Position);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {

            base.Draw(spriteBatch);
        }
    }
}