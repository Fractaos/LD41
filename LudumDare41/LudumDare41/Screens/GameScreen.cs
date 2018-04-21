using LudumDare41.Utility;
using Microsoft.Xna.Framework;

namespace LudumDare41.Screens
{
    public class GameScreen : Screen
    {


        public GameScreen()
            : base()
        {

        }

        public override void Create()
        {

        }

        public override void Update(GameTime time)
        {

            UiManager.Update(time.ElapsedGameTime.Milliseconds);
            TimerManager.Update(time.ElapsedGameTime.Milliseconds);
        }

        public override void Draw()
        {
            spriteBatch.Begin();
            UiManager.Draw(spriteBatch);
            spriteBatch.End();
        }
    }
}