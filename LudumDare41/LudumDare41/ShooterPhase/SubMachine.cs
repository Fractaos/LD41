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
            _numberBulletInLoader = 32;
            _bulletSpeed = 40f;
            _timeBetweenFire = 250f;
            _timeElaspedSinceLastShot = _timeBetweenFire;
            _shotSound = Assets.SubMachineShot;
        }
    }
}