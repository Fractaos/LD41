using LudumDare41.Graphics;
using LudumDare41.Utility;
using Microsoft.Xna.Framework;

namespace LudumDare41.ShooterPhase
{
    public class SubMachine : Weapon
    {
        public SubMachine(Vector2 position, int bulletInWeapon, WeaponState weaponState, Camera currentCamera) : base(Assets.SubMachine, position, bulletInWeapon, weaponState, currentCamera)
        {
            _totalNumberBulletInLoader = 32;
            _numberBulletInLoader = _totalBullet >= _totalNumberBulletInLoader ? _totalNumberBulletInLoader : _totalBullet;
            _timeToReload = 2000f;
            _timeSinceBeginReload = _timeToReload;
            _bulletSpeed = 40f;
            _timeBetweenFire = 250f;
            _damage = 7.5f;
            _timeElaspedSinceLastShot = _timeBetweenFire;
            _shotSound = Assets.SubMachineShot;
            _reloadPb = new ProgressBar(new Vector2(Position.X - 25, Position.Y - 50), 50, 10, Color.White, _timeToReload, false);
        }

        public SubMachine(Vector2 position, WeaponState weaponState, Camera currentCamera) : this(position, 150, weaponState, currentCamera)
        {

        }
    }
}