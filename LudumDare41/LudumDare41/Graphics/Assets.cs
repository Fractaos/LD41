using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

namespace LudumDare41.Graphics
{
    public class Assets
    {
        #region Variable
        public static Texture2D PixelW, PixelB;

        //SPRITE
        public static Texture2D backgroundGestion;
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
            backgroundGestion = Main.Content.Load<Texture2D>("Assets/Graphics/GestionPhase/corps");
            //SON

            //FONT
            Font = Main.Content.Load<SpriteFont>("Assets/Fonts/littlefont");

        }

    
        #endregion
    }
}
