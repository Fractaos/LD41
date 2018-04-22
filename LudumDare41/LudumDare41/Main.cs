using LudumDare41.Graphics;
using LudumDare41.Screens;
using LudumDare41.Utility;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;


namespace LudumDare41
{
    class Main
    {

        public static GraphicsDeviceManager Graphics;
        public static GraphicsDevice Device;
        public static Game Instance;
        public static ContentManager Content;
        public static Random Rand;
        public static Screen CurrentScreen;

        public Main(GraphicsDeviceManager graphics, Game game)
        {
            Main.Graphics = graphics;
            Device = graphics.GraphicsDevice;
            Instance = game;
            Content = game.Content;
            Rand = new Random();
        }

        public void Initialize()
        {
            Assets.LoadAll();

            Graphics.PreferredBackBufferWidth = Utils.WIDTH;
            Graphics.PreferredBackBufferHeight = Utils.HEIGHT;
            Graphics.SynchronizeWithVerticalRetrace = false;
            Graphics.ApplyChanges();
            Instance.IsMouseVisible = true;

            SetScreen(new ShooterScreen());

        }

        public void Update(GameTime time)
        {
            TimerManager.Update(time.ElapsedGameTime.Milliseconds);
            Input.Update();

            CurrentScreen?.Update(time);
        }

        public void Draw()
        {
            Device.Clear(Color.Black);

            CurrentScreen?.Draw();
        }

        public static void SetScreen(Screen screen)
        {
            CurrentScreen = screen;
            CurrentScreen.Create();
        }
    }
}
