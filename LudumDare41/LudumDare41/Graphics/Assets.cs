﻿using Microsoft.Xna.Framework;
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

        //SON
        public static SoundEffect GunShot, SubMachineShot, SniperShot;

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

            //SON
            GunShot = Main.Content.Load<SoundEffect>("Assets/Sounds/ShooterPhase/gun_shoot");
            SubMachineShot = Main.Content.Load<SoundEffect>("Assets/Sounds/ShooterPhase/submachine_shoot");
            SniperShot = Main.Content.Load<SoundEffect>("Assets/Sounds/ShooterPhase/sniper_shoot");


            //FONT
            Font = Main.Content.Load<SpriteFont>("Assets/Fonts/littlefont");

        }

    
        #endregion
    }
}
