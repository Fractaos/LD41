﻿using LudumDare41.Graphics;
using LudumDare41.Screens;
using LudumDare41.Utility;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;

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
        protected float _timeBetweenFire, _bulletSpeed, _rotation, _timeElaspedSinceLastShot, _timeSinceBeginReload, _timeToReload, _damage;
        protected int _numberBulletInLoader, _totalNumberBulletInLoader, _totalBullet;
        protected bool _playerHold;
        protected WeaponState _weaponState;
        protected bool _canDestroy;
        protected List<Bullet> _bulletsFired;
        protected SoundEffect _shotSound;

        protected Camera _currentCamera;

        protected ProgressBar _reloadPb;



        protected Weapon(Texture2D texture, Vector2 position, int bulletInWeapon, WeaponState weaponState, Camera currentCamera) : base(texture, position)
        {
            _currentCamera = currentCamera;
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

        public float Damage
        {
            get => _damage;
        }

        public virtual void Fire(Vector2 target, Sprite side, float speedFactor)
        {

            if (_weaponState == WeaponState.Holded)
            {
                if (_timeElaspedSinceLastShot > _timeBetweenFire)
                {
                    Vector2 direction = target - Position;
                    direction.Normalize();
                    _bulletsFired.Add(new Bullet(Assets.Bullet, Position, _bulletSpeed, direction, side, this));
                    SoundEffectInstance shotSoundInstance = _shotSound.CreateInstance();
                    if (speedFactor > 1)
                        shotSoundInstance.Pitch = 1f;
                    else
                        shotSoundInstance.Pitch = speedFactor - 1;
                    shotSoundInstance.Play();
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

            float speedFactor = 1;
            if (Main.CurrentScreen is ShooterScreen currentScreen)
            {
                speedFactor = currentScreen.TimeScale;
            }

            switch (_weaponState)
            {
                case WeaponState.OnFloor:

                    break;
                case WeaponState.Holded:

                    break;
                case WeaponState.Reload:
                    _timeSinceBeginReload -= time.ElapsedGameTime.Milliseconds * speedFactor;
                    _reloadPb.Position = new Vector2(Position.X - 25, Position.Y - 50);
                    if (_timeSinceBeginReload < 0)
                    {
                        _timeSinceBeginReload = _timeToReload;
                        _numberBulletInLoader = _totalBullet >= _totalNumberBulletInLoader ? _totalNumberBulletInLoader : _totalBullet;
                        _reloadPb.Reset();
                        _weaponState = WeaponState.Holded;
                    }
                    _reloadPb.Update(time, (int)_timeSinceBeginReload);
                    break;
                case WeaponState.Empty:
                    _canDestroy = true;
                    break;


            }
            Vector2 direction = _currentCamera.ScreenToWorld(Input.MousePos) - Position;
            direction.Normalize();

            _rotation = (float)Math.Atan2(direction.Y, direction.X);


            _timeElaspedSinceLastShot += time.ElapsedGameTime.Milliseconds * speedFactor;
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