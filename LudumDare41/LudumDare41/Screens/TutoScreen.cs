using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

using LudumDare41.Graphics;
using Microsoft.Xna.Framework.Graphics;
using LudumDare41.Utility;
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
                if(_index == Screens.Length-1)
                {
                    //CHANGEMENT DECRAN
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
