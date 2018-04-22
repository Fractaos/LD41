using LudumDare41.Graphics;
using LudumDare41.Screens;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace LudumDare41.ShooterPhase
{
    public enum EnemyState
    {
        LookingForPlayer,
        Aiming,
        Shooting
    }

    public class Enemy : Sprite
    {
        private const int MAX_LIFE = 50;
        private Weapon _weaponHolded;
        private float _rotation;
        private Player _thePlayer;
        private bool _alive = true;
        private int _life;


        private Camera _currentCamera;

        public Enemy(Texture2D texture, Vector2 position, Player thePlayer, Camera currentCamera) : base(texture, position)
        {
            _currentCamera = currentCamera;
            _thePlayer = thePlayer;
            _life = MAX_LIFE;
            int rnd = Utils.RANDOM.Next(Utils.NUMBER_TYPE_WEAPON);
            switch (rnd)
            {
                case 0:
                    _weaponHolded = new Gun(Position, 3, WeaponState.Holded, _currentCamera);
                    break;
                case 1:
                    _weaponHolded = new SubMachine(Position, 3, WeaponState.Holded, _currentCamera);
                    break;
                case 2:
                    _weaponHolded = new Sniper(Position, 3, WeaponState.Holded, _currentCamera);
                    break;
                default:
                    _weaponHolded = new Gun(Position, 3, WeaponState.Holded, _currentCamera);
                    break;
            }

            _hitbox = new Rectangle(_hitbox.X - Texture.Width / 2, _hitbox.Y - Texture.Height / 2, _hitbox.Width, _hitbox.Height);
        }

        public int Life
        {
            get => _life;
        }

        public bool Alive
        {
            get => _alive;
        }

        public void TakeDamage(float amount)
        {
            _life -= (int)amount;
            if (_life <= 0)
            {
                _life = 0;
                _alive = false;
                Assets.EnemyDead.Play();
            }
        }

        public void Update(GameTime time)
        {

            float speedFactor = 1f;
            if (Main.CurrentScreen is ShooterScreen currentScreen)
            {
                speedFactor = currentScreen.TimeScale;
            }

            Vector2 direction = _thePlayer.Position - Position;
            direction.Normalize();

            _rotation = (float)Math.Atan2(direction.Y, direction.X);

            _weaponHolded?.Fire(_thePlayer.Position, this, speedFactor);
            if (_weaponHolded != null && _weaponHolded.CanDestroy)
                _weaponHolded = null;
            _weaponHolded?.Update(time);
        }

        public new void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(Texture, Position, null, Color.White, _rotation,
                new Vector2((float)Texture.Width / 2, (float)Texture.Height / 2), 1f,
                SpriteEffects.None, 1f);
            _weaponHolded?.Draw(spriteBatch, _thePlayer.Position);

            //spriteBatch.Draw(_hitboxTexture, _hitbox, Color.White);
        }
    }
}