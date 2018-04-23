using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;

namespace LudumDare41.Graphics
{
    public class Assets
    {
        #region Variable

        //SPRITE
        public static Texture2D PixelW, PixelB, CrossAim, Gun, SubMachine, Sniper, Bullet, Enemy, Player;

        public static Texture2D backgroundGestion, factoryButton, GameOver, ReplayButton, QuitButton, Arene;
        //SON
        //SHOOTERPHASE
        public static SoundEffectInstance GunShot, SubMachineShot, SniperShot, EnemyHitted, EnemyDead, PlayerHitted, MusicGestion, MusicShooter;

        //GESTIONPHASE
        public static Texture2D Sucre, Gras, VitC ,/*FACTORY*/ AddAnti;
        public static SoundEffect Drag, Drop;

        //TUTOPHASE
        public static Texture2D Plan1, Plan2, Plan3, Plan4;


        //FONT
        public static SpriteFont Font, BigFont;
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

            Enemy = Utils.CreateTexture(50, 50, Color.Red);
            Player = Utils.CreateTexture(50, 50, Color.Blue);

            factoryButton = Main.Content.Load<Texture2D>("Assets/Graphics/GestionPhase/buttonFactory");
            backgroundGestion = Main.Content.Load<Texture2D>("Assets/Graphics/GestionPhase/corps");

            Sucre = Main.Content.Load<Texture2D>("Assets/Graphics/ShooterPhase/sucre");
            Gras = Main.Content.Load<Texture2D>("Assets/Graphics/ShooterPhase/gras");
            VitC = Main.Content.Load<Texture2D>("Assets/Graphics/ShooterPhase/vitC");
            AddAnti = Main.Content.Load<Texture2D>("Assets/Graphics/GestionPhase/addAnti");

            Plan1 = Main.Content.Load<Texture2D>("Assets/Graphics/TutoPhase/plan1");
            Plan2 = Main.Content.Load<Texture2D>("Assets/Graphics/TutoPhase/plan2");
            Plan3 = Main.Content.Load<Texture2D>("Assets/Graphics/TutoPhase/plan3");
            Plan4 = Main.Content.Load<Texture2D>("Assets/Graphics/TutoPhase/plan4");


            GameOver = Main.Content.Load<Texture2D>("Assets/Graphics/ShooterPhase/gameover");
            ReplayButton = Main.Content.Load<Texture2D>("Assets/Graphics/ShooterPhase/button_playagain");
            QuitButton = Main.Content.Load<Texture2D>("Assets/Graphics/ShooterPhase/button_quit");

            Arene = Main.Content.Load<Texture2D>("Assets/Graphics/ShooterPhase/arene");
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
            BigFont = Main.Content.Load<SpriteFont>("Assets/Fonts/font");

            Utils.Load();

        }


        #endregion
    }
}
