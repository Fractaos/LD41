using LudumDare41.Graphics;
using LudumDare41.Utility;
using Microsoft.Xna.Framework;

namespace LudumDare41.ShooterPhase
{
    public class Gun : Weapon
    {
        public Gun(Vector2 position, int bulletInWeapon, WeaponState weaponState) : base(Assets.Gun, position, bulletInWeapon, weaponState)
        {
            _totalNumberBulletInLoader = 9;
            _numberBulletInLoader = _totalBullet >= _totalNumberBulletInLoader ? _totalNumberBulletInLoader : _totalBullet;
            _timeToReload = 1000f;
            _bulletSpeed = 20f;
            _timeBetweenFire = 1000f;
            _damage = 10;
            _timeElaspedSinceLastShot = _timeBetweenFire;
            _shotSound = Assets.GunShot;
            _reloadPb = new ProgressBar(new Vector2(Position.X - 25, Position.Y - 50), 50, 10, Color.White, _timeToReload, false);
        }
    }
}