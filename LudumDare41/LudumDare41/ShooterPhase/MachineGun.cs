using System;
using LudumDare41.Graphics;
using LudumDare41.Utility;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace LudumDare41.ShooterPhase
{
    public class MachineGun : Weapon
    {
        public MachineGun(Texture2D texture, Vector2 position, int bulletInWeapon, WeaponState weaponState) : base(texture, position, bulletInWeapon, weaponState)
        {
            _numberBulletInLoader = 32;
            _bulletSpeed = 40f;
            _timeBetweenFire = 250f;
            _timeElaspedSinceLastShot = _timeBetweenFire;
        }

        public override void Fire()
        {
            if (_timeElaspedSinceLastShot > _timeBetweenFire)
            {
                _timeElaspedSinceLastShot = 0;
                Vector2 direction = Input.MousePos - Position;
                direction.Normalize();
                _bulletsFired.Add(new Bullet(Assets.Bullet, Position, _bulletSpeed, direction));
            }
        }

        public override void Update(GameTime time)
        {
            switch (_weaponState)
            {
                case WeaponState.OnFloor:

                    break;
                case WeaponState.Holded:
                    Vector2 direction = Input.MousePos - Position;
                    direction.Normalize();

                    _rotation = (float)Math.Atan2(direction.Y, direction.X);
                    foreach (var bullet in _bulletsFired)
                    {
                        bullet.Update(time);
                    }

                    _bulletsFired.RemoveAll(bullet => bullet.ToDestroy);

                    _timeElaspedSinceLastShot += time.ElapsedGameTime.Milliseconds;
                    break;
                case WeaponState.Empty:
                    _canDestroy = true;
                    break;
            }
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            switch (_weaponState)
            {
                case WeaponState.OnFloor:
                    spriteBatch.Draw(Texture, Position, Color.White);
                    break;
                case WeaponState.Holded:
                    if (_playerHold)
                    {
                        spriteBatch.Draw(Texture, Position, null, Color.White, _rotation,
                            new Vector2((float)Texture.Width / 2, (float)Texture.Height / 2), 1f, SpriteEffects.None, 0f);
                    }

                    foreach (var bullet in _bulletsFired)
                    {
                        bullet.Draw(spriteBatch);
                    }
                    break;
            }
        }
    }
}