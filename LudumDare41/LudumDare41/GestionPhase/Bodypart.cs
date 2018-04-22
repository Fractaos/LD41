using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using LudumDare41.Utility;

namespace LudumDare41.GestionPhase
{
    public class BodyPart
    {
        public string name;
        public int AntiNbr;
        public Rectangle Bounds;

        Texture2D textBound;


        public BodyPart(Rectangle bounds, string name)
        {
            this.name = name;
            Bounds = bounds;
            textBound = Utils.CreateTexture(Bounds.Width, Bounds.Height, Color.Yellow);

        }

        public void Update(float time)
        {
        }


        public void Draw(SpriteBatch batch)
        {
            //batch.Draw(textBound, new Vector2(Bounds.X, Bounds.Y), Color.White);
        }
    }
}
