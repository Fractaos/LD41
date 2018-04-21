using LudumDare41.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace LudumDare41.ShooterPhase
{
    public abstract class Weapon : Sprite
    {
        private float _fireSpeed, _bulletSpeed;
        private int _numberBulletInLoader, _totalBullet;

        protected Weapon(Texture2D texture, Vector2 position) : base(texture, position)
        {

        }

        public float FireSpeed
        {
            get => _fireSpeed;
            set => _fireSpeed = value;
        }

        public float BulletSpeed
        {
            get => _bulletSpeed;
        }

        public int NumberBulletInLoader
        {
            get => _numberBulletInLoader;
        }

        public int TotalBullet
        {
            get => _totalBullet;
            set => _totalBullet = value;
        }

        public abstract void Update(GameTime time);

        public new abstract void Draw(SpriteBatch spriteBatch);
    }
}