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
        Reload,
        OnFloor
    }

    public abstract class Weapon : Sprite
    {
        protected float _timeBetweenFire, _bulletSpeed, _rotation, _timeElaspedSinceLastShot, _timeSinceBeginReload, _timeToReload;
        protected int _numberBulletInLoader, _totalNumberBulletInLoader, _totalBullet;
        protected bool _playerHold;
        protected WeaponState _weaponState;
        protected bool _canDestroy;
        protected List<Bullet> _bulletsFired;
        protected SoundEffect _shotSound;

        protected ProgressBar _reloadPb;
        


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

        public virtual void Fire(Vector2 target, Sprite side)
        {
            if (_weaponState == WeaponState.Holded)
            {
                if (_timeElaspedSinceLastShot > _timeBetweenFire)
                {
                    Vector2 direction = target - Position;
                    direction.Normalize();
                    _bulletsFired.Add(new Bullet(Assets.Bullet, Position, _bulletSpeed, direction, side));
                    _shotSound.Play();
                    _timeElaspedSinceLastShot = 0;
                    _totalBullet--;
                    _numberBulletInLoader--;
                    if (_totalBullet <= 0)
                    {
                        _weaponState = WeaponState.Empty;
                    }
                    else
                    {
                        if (_numberBulletInLoader <= 0)
                        {
                            _weaponState = WeaponState.Reload;
                        }
                    }


                }
            }
        }



        public virtual void Update(GameTime time)
        {
            switch (_weaponState)
            {
                case WeaponState.OnFloor:

                    break;
                case WeaponState.Holded:
                    
                    break;
                case WeaponState.Reload:
                    _timeSinceBeginReload += time.ElapsedGameTime.Milliseconds;
                    _reloadPb.DecreaseBar(time.ElapsedGameTime.Milliseconds);
                    _reloadPb.Position = new Vector2(Position.X - 25, Position.Y - 50);
                    if (_timeSinceBeginReload > _timeToReload)
                    {
                        _timeSinceBeginReload = 0;
                        _numberBulletInLoader = _totalBullet >= _totalNumberBulletInLoader ? _totalNumberBulletInLoader : _totalBullet;
                        _reloadPb.Reset();
                        _weaponState = WeaponState.Holded;
                    }
                    _reloadPb.Update(time.ElapsedGameTime.Milliseconds);
                    break;
                case WeaponState.Empty:
                    _canDestroy = true;
                    break;


            }
            Vector2 direction = Input.MousePos - Position;
            direction.Normalize();

            _rotation = (float)Math.Atan2(direction.Y, direction.X);


            _timeElaspedSinceLastShot += time.ElapsedGameTime.Milliseconds;
            foreach (var bullet in _bulletsFired)
            {
                bullet.Update(time);
            }

            UpdateHitbox(Position);

            _bulletsFired.RemoveAll(bullet => bullet.ToDestroy);
        }

        public new virtual void Draw(SpriteBatch spriteBatch)
        {
            switch (_weaponState)
            {
                case WeaponState.OnFloor:
                    spriteBatch.Draw(Texture, Position, Color.White);
                    break;
                case WeaponState.Holded:
                    
                    break;
                case WeaponState.Reload:
                    _reloadPb.Draw(spriteBatch);
                    break;
            }
            if (_playerHold)
            {
                spriteBatch.Draw(Texture, Position, null, Color.White, _rotation,
                    new Vector2((float)Texture.Width / 2, (float)Texture.Height / 2), 1f, SpriteEffects.None, 0f);
            }
            foreach (var bullet in _bulletsFired)
            {
                bullet.Draw(spriteBatch);
            }

            //spriteBatch.Draw(_hitboxTexture, _hitbox, Color.White);
        }

        public virtual void Draw(SpriteBatch spriteBatch, Vector2 target)
        {
            switch (_weaponState)
            {
                case WeaponState.OnFloor:
                    spriteBatch.Draw(Texture, Position, Color.White);
                    break;
                case WeaponState.Holded:
                    
                    break;
                case WeaponState.Reload:
                    _reloadPb.Draw(spriteBatch);
                    break;
            }
            Vector2 direction = target - Position;
            direction.Normalize();

            float rotation = (float)Math.Atan2(direction.Y, direction.X);

            spriteBatch.Draw(Texture, Position, null, Color.White, rotation,
                new Vector2((float)Texture.Width / 2, (float)Texture.Height / 2), 1f, SpriteEffects.None, 0f);

            foreach (var bullet in _bulletsFired)
            {
                bullet.Draw(spriteBatch);
            }

            //spriteBatch.Draw(_hitboxTexture, _hitbox, Color.White);
        }
    }
}