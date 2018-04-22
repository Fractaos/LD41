﻿using LudumDare41.Graphics;
using LudumDare41.Utility;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;

namespace LudumDare41.ShooterPhase
{
    public class Player : Sprite
    {

        #region Fields

        private const int MAX_LIFE = 100;

        private float _moveSpeed = .6f, _accuracy = 1f, _shootSpeed = 1f;
        private float _rotation;

        private int _life;

        private ProgressBar _lifeBar;

        private bool _canGrabWeapon;
        private Weapon _currentWeapon, _grabbableWeapon;

        private CrossAim _crossAim;

        #endregion

        #region Constructors

        public Player(Texture2D texture, Vector2 position) : base(texture, position)
        {
            _crossAim = new CrossAim(Assets.CrossAim, new Vector2((float)Utils.WIDTH / 2, (float)Utils.HEIGHT / 2), this);
            _life = MAX_LIFE;
            _lifeBar = new ProgressBar(new Vector2(10, 20), 100, 15, Color.Green, MAX_LIFE, true);
            _hitbox = new Rectangle(_hitbox.X - Texture.Width / 2, _hitbox.Y - Texture.Height / 2, _hitbox.Width, _hitbox.Height);
        }

        #endregion



        #region Properties

        public int Life
        {
            get => _life;
        }

        public float Accuracy
        {
            get => _accuracy;
        }

        #endregion

        #region Public Methods

        public void TakeDamage(float amount)
        {
            _life -= (int)amount;
            _lifeBar.DecreaseBar((int)amount);
            if (_life <= 0)
            {
                _life = 0;
            }
        }


        public void CanGrabWeapon(Weapon grabbableWeapon)
        {
            _canGrabWeapon = true;
            _grabbableWeapon = grabbableWeapon;
        }

        public void Update(GameTime time)
        {
            //Récupération du temps écoulé (en ms)
            float elapsedGameTimeMillis = time.ElapsedGameTime.Milliseconds;

            //Contrôle du personnage
            if (Input.KeyPressed(Keys.Z, false) || Input.KeyPressed(Keys.Up, false))
                Position.Y -= _moveSpeed * elapsedGameTimeMillis;
            if (Input.KeyPressed(Keys.S, false) || Input.KeyPressed(Keys.Down, false))
                Position.Y += _moveSpeed * elapsedGameTimeMillis;
            if (Input.KeyPressed(Keys.Q, false) || Input.KeyPressed(Keys.Left, false))
                Position.X -= _moveSpeed * elapsedGameTimeMillis;
            if (Input.KeyPressed(Keys.D, false) || Input.KeyPressed(Keys.Right, false))
                Position.X += _moveSpeed * elapsedGameTimeMillis;

            //Ramasser une arme
            if (_canGrabWeapon && Input.KeyPressed(Keys.E, true))
            {
                _currentWeapon = _grabbableWeapon;
                _currentWeapon.PlayerHold = true;
                _currentWeapon.WeaponState = WeaponState.Holded;
                _grabbableWeapon = null;
                _canGrabWeapon = false;
            }


            //Déclenchement des diverses actions liées à l'arme actuellement équipée (s'il y en a une)
            if (_currentWeapon != null)
            {
                _currentWeapon.Position = Position;
                if (Input.Left(false))
                {
                    _currentWeapon.Fire(Input.MousePos, this);
                }

                if (Input.KeyPressed(Keys.R, true))
                    _currentWeapon.WeaponState = WeaponState.Reload;
                if (_currentWeapon.CanDestroy)
                    _currentWeapon = null;
                _currentWeapon?.Update(time);
            }


            //Calcul de l'angle de rotation (visant le curseur de la souris)
            Vector2 direction = Input.MousePos - Position;
            direction.Normalize();
            _rotation = (float)Math.Atan2(direction.Y, direction.X);


            //Update barre de vie
            _lifeBar.Update(elapsedGameTimeMillis);

            //UpdateHitbox(Position);
            UpdateHitbox(new Vector2(Position.X - (float)Texture.Width / 2, Position.Y - (float)Texture.Height / 2));

            //Update du viseur
            _crossAim.Update(time);

            base.Update(elapsedGameTimeMillis);
        }

        public new void Draw(SpriteBatch spriteBatch)
        {
            //Affichage du viseur
            _crossAim.Draw(spriteBatch);

            //Affichage de la barre de vie
            _lifeBar.Draw(spriteBatch);

            //Affichage des munitions actuelles de l'armes (si arme équipée)
            if (_currentWeapon != null)
                spriteBatch.DrawString(Assets.Font,
                    _currentWeapon.NumberBulletInLoader + "/" + _currentWeapon.TotalBullet, new Vector2(130, 25),
                    Color.White);

            //Affichage du joueur
            spriteBatch.Draw(Texture, Position, null, Color.White, _rotation,
                new Vector2((float)Texture.Width / 2, (float)Texture.Height / 2), 1f,
                SpriteEffects.None, 1f);

            //Affichage de l'arme tenue
            _currentWeapon?.Draw(spriteBatch);

            //Affichage de la hitbox (uniquement pour debug, à commenter sinon)
            //spriteBatch.Draw(_hitboxTexture, _hitbox, Color.White);
        }

        #endregion
    }
}