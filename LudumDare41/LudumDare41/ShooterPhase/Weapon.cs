using System.Collections.Generic;
using System.Windows.Markup;
using LudumDare41.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace LudumDare41.ShooterPhase
{

    public enum WeaponState
    {
        Holded,
        Empty,
        OnFloor
    }

    public abstract class Weapon : Sprite
    {
        protected float _timeBetweenFire, _bulletSpeed, _rotation, _timeElaspedSinceLastShot;
        protected int _numberBulletInLoader, _totalBullet;
        protected bool _playerHold = false;
        protected WeaponState _weaponState;
        protected bool _canDestroy = false;
        protected List<Bullet> _bulletsFired;

        protected Weapon(Texture2D texture, Vector2 position, int bulletInWeapon, WeaponState weaponState) : base(texture, position)
        {
            _bulletsFired = new List<Bullet>();
            _totalBullet = bulletInWeapon;
            _weaponState = weaponState;
        }

        public WeaponState WeaponState
        {
            get => _weaponState;
            set => _weaponState = value;
        }

        public bool CanDestroy
        {
            get => _canDestroy;
            set => _canDestroy = value;
        }

        public float TimeBetweenFire
        {
            get => _timeBetweenFire;
            set => _timeBetweenFire = value;
        }

        public float BulletSpeed
        {
            get => _bulletSpeed;
        }

        public int NumberBulletInLoader
        {
            get => _numberBulletInLoader;
        }

        public int TotalBullet
        {
            get => _totalBullet;
            set => _totalBullet = value;
        }

        public bool PlayerHold
        {
            get => _playerHold;
            set => _playerHold = value;
        }

        public abstract void Fire();

        public abstract void Update(GameTime time);

        public new abstract void Draw(SpriteBatch spriteBatch);
    }
}