using System;
using System.Collections.Generic;
using System.Windows.Markup;
using LudumDare41.Graphics;
using LudumDare41.Utility;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
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
        protected SoundEffect _shotSound;

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

        public virtual void Fire()
        {
            if (_timeElaspedSinceLastShot > _timeBetweenFire)
            {
                _timeElaspedSinceLastShot = 0;
                Vector2 direction = Input.MousePos - Position;
                direction.Normalize();
                _bulletsFired.Add(new Bullet(Assets.Bullet, Position, _bulletSpeed, direction));
                _shotSound.Play();
            }
        }

        public virtual void Update(GameTime time)
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

        public new virtual void Draw(SpriteBatch spriteBatch)
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

        public virtual void Draw(SpriteBatch spriteBatch, Vector2 target)
        {
            switch (_weaponState)
            {
                case WeaponState.OnFloor:
                    spriteBatch.Draw(Texture, Position, Color.White);
                    break;
                case WeaponState.Holded:
                    Vector2 direction = target - Position;
                    direction.Normalize();

                    float rotation = (float)Math.Atan2(direction.Y, direction.X);

                    spriteBatch.Draw(Texture, Position, null, Color.White, rotation,
                        new Vector2((float)Texture.Width / 2, (float)Texture.Height / 2), 1f, SpriteEffects.None, 0f);

                    foreach (var bullet in _bulletsFired)
                    {
                        bullet.Draw(spriteBatch);
                    }
                    break;
            }
        }
    }
}