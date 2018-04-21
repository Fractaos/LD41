using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace LudumDare41
{
    class Utils
    {
        //DESSIN DE HITBOX
        public static Texture2D CreateTexture(int w, int h, Color col)
        {
            var texture = new Texture2D(Main.Device, w, h);
            var cols = new Color[w * h];
            for (var i = 0; i < cols.Length; i++)
            {
                cols[i] = col;
            }
            texture.SetData(cols);
            return texture;
        }

        /// <summary>
        /// Dessine une texture creuse, avec seulement les contours
        /// </summary>
        /// <param name="width">La largeur</param>
        /// <param name="height">La hauteur</param>
        /// <param name="color">La couleur</param>
        /// <returns>Retourne la texture sous forme Texture2D</returns>
        public static Texture2D CreateContouringTexture(int width, int height, Color color)
        {
            var texture = new Texture2D(Main.Device, width, height);
            var colors = new Color[width * height];
            for (var i = 0; i < colors.Length; i++)
            {
                if ((i >= 0 && i < width) || (i > ((colors.Length - 1) - width) && i <= colors.Length - 1))
                {
                    colors[i] = color;
                }
                if (i % width == 0)
                {
                    colors[i] = color;
                    if (i > 0)
                    {
                        colors[i - 1] = color;
                    }
                }
            }

            texture.SetData(colors);

            return texture;
        }

        public static Texture2D Slice(Rectangle region, Texture2D sheet)
        {
            var rawdata = new Color[region.Width * region.Height];
            sheet.GetData(0, region, rawdata, 0, rawdata.Length);
            var bufferTexture = new Texture2D(Main.Device, region.Width, region.Height);
            bufferTexture.SetData(rawdata);
            return bufferTexture;
        }
    }
}
