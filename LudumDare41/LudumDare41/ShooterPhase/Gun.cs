using System;
using LudumDare41.Graphics;
using LudumDare41.Utility;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace LudumDare41.ShooterPhase
{
    public class Gun : Weapon
    {
        public Gun(Texture2D texture, Vector2 position, int bulletInWeapon, WeaponState weaponState) : base(texture, position, bulletInWeapon, weaponState)
        {
            _numberBulletInLoader = 9;
            _bulletSpeed = 20f;
            _timeBetweenFire = 1000f;
            _timeElaspedSinceLastShot = _timeBetweenFire;
            _shotSound = Assets.GunShot;
        }
    }
}