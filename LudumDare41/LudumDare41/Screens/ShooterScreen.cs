using LudumDare41.ShooterPhase;
using Microsoft.Xna.Framework;

namespace LudumDare41.Screens
{
    public class ShooterScreen : Screen
    {

        private Player _player;

        public override void Create()
        {
            Main.Instance.IsMouseVisible = false;
            _player = new Player(Utils.CreateTexture(50, 50, Color.Blue), new Vector2(Utils.WIDTH/2-25, Utils.HEIGHT/2-25));
        }

        public override void Update(GameTime time)
        {

            _player.Update(time);
        }

        public override void Draw()
        {
            spriteBatch.Begin();
            {

                _player.Draw(spriteBatch);
            }
            spriteBatch.End();
        }
    }
}