using System.Collections.Generic;
using System.Runtime.InteropServices;
using LudumDare41.Graphics;
using LudumDare41.ShooterPhase;
using LudumDare41.Utility;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

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
            Gun gun = new Gun(Assets.Gun, new Vector2(100, 100), 150, WeaponState.OnFloor);
            SubMachine subMachine = new SubMachine(Assets.SubMachine, new Vector2(650, 700), 150, WeaponState.OnFloor);
            Sniper sniper = new Sniper(Assets.Sniper, new Vector2(400, 300), 150, WeaponState.OnFloor);
            _weapons.Add(gun);
            _weapons.Add(subMachine);
            _weapons.Add(sniper);
            _player = new Player(Utils.CreateTexture(50, 50, Color.Blue), new Vector2(Utils.WIDTH/2-25, Utils.HEIGHT/2-25));
        }

        public override void Update(GameTime time)
        {
            foreach (var weapon in _weapons)
            {
                if (_player.Hitbox.Intersects(weapon.Hitbox))
                {
                    _player.CanGrabWeapon(weapon);
                    break;
                }
            }

            _weapons.RemoveAll(weapon => weapon.PlayerHold);

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