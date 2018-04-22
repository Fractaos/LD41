﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;

namespace LudumDare41.Graphics
{
    public class Assets
    {
        #region Variable

        //SPRITE
        public static Texture2D PixelW, PixelB, CrossAim, Gun, SubMachine, Sniper, Bullet;

        public static Texture2D backgroundGestion;
        //SON
        //SHOOTERPHASE
        public static SoundEffectInstance GunShot, SubMachineShot, SniperShot, EnemyHitted, EnemyDead, PlayerHitted, MusicGestion, MusicShooter;
        //GESTIONPHASE
        public static SoundEffect Drag, Drop;


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
            GunShot = Main.Content.Load<SoundEffect>("Assets/Sounds/ShooterPhase/gun_shoot").CreateInstance();
            SubMachineShot = Main.Content.Load<SoundEffect>("Assets/Sounds/ShooterPhase/submachine_shoot").CreateInstance();
            SniperShot = Main.Content.Load<SoundEffect>("Assets/Sounds/ShooterPhase/sniper_shoot").CreateInstance();
            Drag = Main.Content.Load<SoundEffect>("Assets/Sounds/GestionPhase/anti_drag");
            Drop = Main.Content.Load<SoundEffect>("Assets/Sounds/GestionPhase/anti_drop");
            MusicGestion = Main.Content.Load<SoundEffect>("Assets/Sounds/GestionPhase/music_gestion").CreateInstance();

            PlayerHitted = Main.Content.Load<SoundEffect>("Assets/Sounds/ShooterPhase/player_hitted").CreateInstance();
            EnemyHitted = Main.Content.Load<SoundEffect>("Assets/Sounds/ShooterPhase/enemy_hitted").CreateInstance();

            EnemyDead = Main.Content.Load<SoundEffect>("Assets/Sounds/ShooterPhase/enemy_dead").CreateInstance();

            MusicShooter = Main.Content.Load<SoundEffect>("Assets/Sounds/ShooterPhase/music_shooter").CreateInstance();


            //FONT
            Font = Main.Content.Load<SpriteFont>("Assets/Fonts/littlefont");

            Utils.Load();

        }


        #endregion
    }
}
