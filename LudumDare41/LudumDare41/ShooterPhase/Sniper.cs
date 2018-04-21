using LudumDare41.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace LudumDare41.ShooterPhase
{
    public class Sniper : Weapon
    {
        public Sniper(Texture2D texture, Vector2 position, int bulletInWeapon, WeaponState weaponState) : base(texture, position, bulletInWeapon, weaponState)
        {
            _numberBulletInLoader = 5;
            _bulletSpeed = 50;
            _timeBetweenFire = 2000f;
            _timeElaspedSinceLastShot = _timeBetweenFire;
            _shotSound = Assets.SniperShot;
        }
    }
}