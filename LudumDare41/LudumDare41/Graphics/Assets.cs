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
        public static Texture2D Heart, Bouton, Skull, ButtonSet, ButtonSet2;
        public static Dictionary<string, Texture2D> ButtonDic, ButtonDic2;
        //SON
        //FONT
        public static SpriteFont Font;
        #endregion

        #region Methode
        public static void LoadAll()
        {
            PixelW = Utils.CreateTexture(1, 1, Color.White);
            PixelB = Utils.CreateTexture(1, 1, Color.Black);

            #region DEMOGUI
            LoadGuiDemo();
            #endregion

            //SPRITE

            //SON

            //FONT
            Font = Main.Content.Load<SpriteFont>("Assets/Fonts/littlefont");

            LoadGuiDemo();
        }

        private static void LoadGuiDemo()
        {
            Heart = Main.Content.Load<Texture2D>("DEMOS/DemoGUI/heart");
            Skull = Main.Content.Load<Texture2D>("DEMOS/DemoGUI/skull");
            ButtonSet = Main.Content.Load<Texture2D>("DEMOS/DemoGUI/buttonset");
            ButtonSet2 = Main.Content.Load<Texture2D>("DEMOS/DemoGUI/buttonset2");

            ButtonDic = new Dictionary<string, Texture2D>
            {
                { "topleft", Utils.Slice(new Rectangle(0, 0, 32, 32), ButtonSet) },
                { "top", Utils.Slice(new Rectangle(32, 0, 32, 32), ButtonSet) },
                { "topright", Utils.Slice(new Rectangle(64, 0, 32, 32), ButtonSet) },
                { "left", Utils.Slice(new Rectangle(0, 32, 32, 32), ButtonSet) },
                { "middle", Utils.Slice(new Rectangle(32, 32, 32, 32), ButtonSet) },
                { "right", Utils.Slice(new Rectangle(64, 32, 32, 32), ButtonSet) },
                { "bottomleft", Utils.Slice(new Rectangle(0, 64, 32, 32), ButtonSet) },
                { "bottom", Utils.Slice(new Rectangle(32, 64, 32, 32), ButtonSet) },
                { "bottomright", Utils.Slice(new Rectangle(64, 64, 32, 32), ButtonSet) }
            };

            ButtonDic2 = new Dictionary<string, Texture2D>
            {
                { "topleft", Utils.Slice(new Rectangle(0, 0, 32, 32), ButtonSet2) },
                { "top", Utils.Slice(new Rectangle(32, 0, 32, 32), ButtonSet2) },
                { "topright", Utils.Slice(new Rectangle(64, 0, 32, 32), ButtonSet2) },
                { "left", Utils.Slice(new Rectangle(0, 32, 32, 32), ButtonSet2) },
                { "middle", Utils.Slice(new Rectangle(32, 32, 32, 32), ButtonSet2) },
                { "right", Utils.Slice(new Rectangle(64, 32, 32, 32), ButtonSet2) },
                { "bottomleft", Utils.Slice(new Rectangle(0, 64, 32, 32), ButtonSet2) },
                { "bottom", Utils.Slice(new Rectangle(32, 64, 32, 32), ButtonSet2) },
                { "bottomright", Utils.Slice(new Rectangle(64, 64, 32, 32), ButtonSet2) }
            };
        }
        #endregion
    }
}
