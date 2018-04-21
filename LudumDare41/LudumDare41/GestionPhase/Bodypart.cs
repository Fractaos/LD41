using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LudumDare41.GestionPhase
{
    public class Bodypart
    {
        List<Anticorps> anticorps;
        public Rectangle Bounds;

        public Bodypart(Rectangle bounds)
        {
            Bounds = bounds;
            anticorps = new List<Anticorps>();
        }

        public void AddAnti()
        {
            anticorps.Add(new Anticorps(new Vector2(Main.Rand.Next(Bounds.X, Bounds.Width - 40), Main.Rand.Next(Main.Rand.Next(Bounds.Y, Bounds.Height - 40)))));
        }

        public void RemoveAnti()
        {
            if(anticorps.Count>0)
            {
                anticorps[0].End = true;
            }
                
        }//LOL


        public void Update(float time)
        {
            foreach (var item in anticorps)
            {
                item.Position += new Vector2(Main.Rand.Next(0, 2) - 2, Main.Rand.Next(0, 2) - 1);
            }
            anticorps.RemoveAll(k => k.End = true);
        }


        public void Draw(SpriteBatch batch)
        {

        }
    }
}
