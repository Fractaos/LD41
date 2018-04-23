using LudumDare41.Graphics;
using LudumDare41.Utility;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace LudumDare41.Screens
{
    public class TutoScreen : Screen
    {
        Texture2D[] Screens;
        private int _index;

        public override void Create()
        {
            _index = 0;
            Screens = new Texture2D[] { Assets.Plan1, Assets.Plan2, Assets.Plan3, Assets.Plan4 };
        }

        public override void Update(GameTime time)
        {
            if (Input.KeyPressed(Keys.Space, true))
            {
                if (_index == Screens.Length - 1)
                {
                    Main.SetScreen(Main.CurrentsScreens[0]);
                }
                else
                    _index++;
            }
        }

        public override void Draw()
        {
            SpriteBatch.Begin();
            spriteBatch.Draw(Screens[_index], Vector2.Zero, Color.White);
            SpriteBatch.End();
        }

        public override void Resume()
        {

        }
    }
}
