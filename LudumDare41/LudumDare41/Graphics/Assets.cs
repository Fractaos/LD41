using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using Microsoft.Xna.Framework.Audio;

namespace LudumDare41.Graphics
{
    public class Assets
    {
        #region Variable

        //SPRITE
        public static Texture2D PixelW, PixelB, CrossAim, Gun, SubMachine, Sniper, Bullet;

        public static Texture2D backgroundGestion;
        //SON
        public static SoundEffect GunShot, SubMachineShot, SniperShot, EnemyHitted, PlayerHitted;

        //FONT
        public static SpriteFont Font;
        #endregion

        #region Methode
        public static void LoadAll()
        {
            PixelW = Utils.CreateTexture(1, 1, Color.White);
            PixelB = Utils.CreateTexture(1, 1, Color.Black);

            //SPRITE
            CrossAim = Main.Content.Load<Texture2D>("Assets/Graphics/ShooterPhase/viseur");
            Gun = Main.Content.Load<Texture2D>("Assets/Graphics/ShooterPhase/gun");
            SubMachine = Main.Content.Load<Texture2D>("Assets/Graphics/ShooterPhase/machinegun");
            Sniper = Main.Content.Load<Texture2D>("Assets/Graphics/ShooterPhase/sniper");
            Bullet = Main.Content.Load<Texture2D>("Assets/Graphics/ShooterPhase/bullet");

            backgroundGestion = Main.Content.Load<Texture2D>("Assets/Graphics/GestionPhase/corps");
            //SON
            GunShot = Main.Content.Load<SoundEffect>("Assets/Sounds/ShooterPhase/gun_shoot");
            SubMachineShot = Main.Content.Load<SoundEffect>("Assets/Sounds/ShooterPhase/submachine_shoot");
            SniperShot = Main.Content.Load<SoundEffect>("Assets/Sounds/ShooterPhase/sniper_shoot");

            PlayerHitted = Main.Content.Load<SoundEffect>("Assets/Sounds/ShooterPhase/player_hitted");
            EnemyHitted = Main.Content.Load<SoundEffect>("Assets/Sounds/ShooterPhase/enemy_hitted");


            //FONT
            Font = Main.Content.Load<SpriteFont>("Assets/Fonts/littlefont");

            Utils.Load();

        }

    
        #endregion
    }
}
