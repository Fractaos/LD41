using System;
using LudumDare41.Graphics;
using LudumDare41.Utility;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace LudumDare41.ShooterPhase
{
    public class Player : Sprite
    {
        private const int MAX_LIFE = 100;

        private float _moveSpeed = .6f, _aimSpeed = 1, _shootSpeed = 1;
        private float _rotation;

        private int _life;

        private bool _canGrabWeapon;
        private Weapon _currentWeapon, _grabbableWeapon;

        private CrossAim _crossAim;

        public Player(Texture2D texture, Vector2 position) : base(texture, position)
        {
            _crossAim = new CrossAim(Assets.CrossAim, new Vector2((float)Utils.WIDTH/2, (float)Utils.HEIGHT/2), this);
            _life = MAX_LIFE;
        }

        public int Life
        {
            get => _life;
            set => _life = value;
        }

        public void CanGrabWeapon(Weapon grabbableWeapon)
        {
            _canGrabWeapon = true;
            _grabbableWeapon = grabbableWeapon;
        }

        public void Update(GameTime time)
        {
            float elapsedGameTimeMillis = time.ElapsedGameTime.Milliseconds;
            if (Input.KeyPressed(Keys.Z, false))
                Position.Y -= _moveSpeed * elapsedGameTimeMillis;
            if (Input.KeyPressed(Keys.S, false))
                Position.Y += _moveSpeed * elapsedGameTimeMillis;
            if (Input.KeyPressed(Keys.Q, false))
                Position.X -= _moveSpeed * elapsedGameTimeMillis;
            if (Input.KeyPressed(Keys.D, false))
                Position.X += _moveSpeed * elapsedGameTimeMillis;

            if (_canGrabWeapon && Input.KeyPressed(Keys.E, true))
            {
                _currentWeapon = _grabbableWeapon;
                _currentWeapon.PlayerHold = true;
                _currentWeapon.WeaponState = WeaponState.Holded;
                _grabbableWeapon = null;
                _canGrabWeapon = false;
            }

            if (_currentWeapon != null)
            {
                _currentWeapon.Position = Position;
                if (Input.Left(false))
                {
                    _currentWeapon.Fire(Input.MousePos);
                }

                if (Input.KeyPressed(Keys.R, true))
                    _currentWeapon.WeaponState = WeaponState.Reload;
                if (_currentWeapon.CanDestroy)
                    _currentWeapon = null;
            }

            _currentWeapon?.Update(time);
            Vector2 direction = Input.MousePos - Position;
            direction.Normalize();

            _rotation = (float) Math.Atan2(direction.Y, direction.X);


            _crossAim.Update(time);
            base.Update(elapsedGameTimeMillis);
        }

        public new void Draw(SpriteBatch spriteBatch)
        {
            _crossAim.Draw(spriteBatch);
            if (_currentWeapon != null)
                spriteBatch.DrawString(Assets.Font,
                    _currentWeapon.NumberBulletInLoader + "/" + _currentWeapon.TotalBullet, new Vector2(10, 10),
                    Color.White);
            spriteBatch.Draw(Texture, Position, null, Color.White, _rotation,
                new Vector2((float) Texture.Width / 2, (float) Texture.Height / 2), 1f,
                SpriteEffects.None, 1f);
            _currentWeapon?.Draw(spriteBatch);
        }
    }
}