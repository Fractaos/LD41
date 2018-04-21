﻿using System;
using LudumDare41.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace LudumDare41.ShooterPhase
{
    public enum EnemyState
    {
        LookingForPlayer,
        Aiming,
        Shooting
    }

    public class Enemy : Sprite
    {
        private Weapon _weaponHolded;
        private float _rotation;
        private Player _thePlayer;

        public Enemy(Texture2D texture, Vector2 position, Player thePlayer) : base(texture, position)
        {
            _thePlayer = thePlayer;
            int rnd = Utils.RANDOM.Next(Utils.NUMBER_TYPE_WEAPON);
            switch (rnd)
            {
                case 0:
                    _weaponHolded = new Gun(Position, 30, WeaponState.Holded);
                    break;
                case 1:
                    _weaponHolded = new SubMachine(Position, 30, WeaponState.Holded);
                    break;
                case 2:
                    _weaponHolded = new Sniper(Position, 30, WeaponState.Holded);
                    break;
                default:
                    _weaponHolded = new Gun(Position, 30, WeaponState.Holded);
                    break;
            }
        }

        public void Update(GameTime time)
        {
            Vector2 direction = _thePlayer.Position - Position;
            direction.Normalize();

            _rotation = (float)Math.Atan2(direction.Y, direction.X);
        }

        public new void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(Texture, Position, null, Color.White, _rotation,
                new Vector2((float)Texture.Width / 2, (float)Texture.Height / 2), 1f,
                SpriteEffects.None, 1f);
            _weaponHolded?.Draw(spriteBatch, _thePlayer.Position);
        }
    }
}