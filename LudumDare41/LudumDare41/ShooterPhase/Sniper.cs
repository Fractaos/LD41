using LudumDare41.Graphics;
using LudumDare41.Utility;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace LudumDare41.ShooterPhase
{
    public class Sniper : Weapon
    {
        public Sniper(Vector2 position, int bulletInWeapon, WeaponState weaponState) : base(Assets.Sniper, position, bulletInWeapon, weaponState)
        {
            _totalNumberBulletInLoader = 5;
            _numberBulletInLoader = _totalBullet >= _totalNumberBulletInLoader ? _totalNumberBulletInLoader : _totalBullet;
            _timeToReload = 3000f;
            _bulletSpeed = 50;
            _timeBetweenFire = 2000f;
            _timeElaspedSinceLastShot = _timeBetweenFire;
            _shotSound = Assets.SniperShot;
            _reloadPb = new ProgressBar(new Vector2(Position.X - 25, Position.Y - 50), 50, 10, Color.White, _timeToReload, false);
        }
    }
}