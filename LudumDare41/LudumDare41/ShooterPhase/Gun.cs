using System;
using LudumDare41.Utility;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace LudumDare41.ShooterPhase
{
    public class Gun : Weapon
    {
        public Gun(Texture2D texture, Vector2 position, int bulletInWeapon, WeaponState weaponState) : base(texture, position, bulletInWeapon, weaponState)
        {
            _numberBulletInLoader = 9;
            _bulletSpeed = 1.5f;
            _timeBetweenFire = 1000f;
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
                    break;
                case WeaponState.Empty:

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
                    break;
            }
            
        }
    }
}