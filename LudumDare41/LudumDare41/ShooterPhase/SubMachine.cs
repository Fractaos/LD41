using System;
using LudumDare41.Graphics;
using LudumDare41.Utility;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace LudumDare41.ShooterPhase
{
    public class SubMachine : Weapon
    {
        public SubMachine(Vector2 position, int bulletInWeapon, WeaponState weaponState) : base(Assets.SubMachine, position, bulletInWeapon, weaponState)
        {
            _totalNumberBulletInLoader = 32;
            _numberBulletInLoader = _totalBullet >= _totalNumberBulletInLoader ? _totalNumberBulletInLoader : _totalBullet;
            _timeToReload = 2000f;
            _bulletSpeed = 40f;
            _timeBetweenFire = 250f;
            _timeElaspedSinceLastShot = _timeBetweenFire;
            _shotSound = Assets.SubMachineShot;
            _reloadPb = new ProgressBar(new Vector2(Position.X - 25, Position.Y - 50), 50, 10, Color.White, _timeToReload, false);
        }
    }
}