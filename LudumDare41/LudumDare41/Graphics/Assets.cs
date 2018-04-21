﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

namespace LudumDare41.Graphics
{
    public class Assets
    {
        #region Variable
        public static Texture2D PixelW, PixelB;

        //SPRITE
        //SON
        //FONT
        public static SpriteFont Font;
        #endregion

        #region Methode
        public static void LoadAll()
        {
            PixelW = Utils.CreateTexture(1, 1, Color.White);
            PixelB = Utils.CreateTexture(1, 1, Color.Black);

            //SPRITE

            //SON

            //FONT
            Font = Main.Content.Load<SpriteFont>("Assets/Fonts/littlefont");

        }

    
        #endregion
    }
}