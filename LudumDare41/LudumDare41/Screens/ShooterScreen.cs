﻿using System.Collections.Generic;
using LudumDare41.Graphics;
using LudumDare41.ShooterPhase;
using Microsoft.Xna.Framework;

namespace LudumDare41.Screens
{
    public class ShooterScreen : Screen
    {

        private Player _player;
        private List<Weapon> _weapons;

        public override void Create()
        {
            Main.Instance.IsMouseVisible = false;
            _weapons = new List<Weapon>();
            Gun gun = new Gun(Assets.Gun, new Vector2(100, 100), 150);
            _weapons.Add(gun);
            _player = new Player(Utils.CreateTexture(50, 50, Color.Blue), new Vector2(Utils.WIDTH/2-25, Utils.HEIGHT/2-25));

        }

        public override void Update(GameTime time)
        {
            foreach (var weapon in _weapons)
            {
                if (_player.Hitbox.Intersects(weapon.Hitbox))
                {
                    _player.CanGrabWeapon(weapon);
                }
                else
                {
                    _player.CantGrabWeapon();
                }
            }

            for (int i = _weapons.Count - 1; i >= 0; i--)
            {
                if(_weapons[i].PlayerHold)
                    _weapons.RemoveAt(i);
            }

            _player.Update(time);
        }

        public override void Draw()
        {
            spriteBatch.Begin();
            {
                foreach (var weapon in _weapons)
                {
                    weapon.Draw(spriteBatch);
                }
                _player.Draw(spriteBatch);
            }
            spriteBatch.End();
        }
    }
}