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
    public class Bodypart
    {
        List<Anticorps> anticorps;
        public Rectangle Bounds;
        float OnBoundsTimer;
        bool ShowInfo;

        Texture2D textBound;


        public Bodypart(Rectangle bounds)
        {
            ShowInfo = false;
            Bounds = bounds;
            anticorps = new List<Anticorps>();
            textBound = Utils.CreateTexture(Bounds.Width, Bounds.Height, Color.Yellow);

        }

        public void AddAnti()
        {
            Anticorps buffer = new Anticorps(new Vector2(Main.Rand.Next(Bounds.X, (Bounds.X + Bounds.Width) - 40), Main.Rand.Next(Bounds.Y, (Bounds.Y + Bounds.Height) - 40)), this);
            //TimerManager.Timers.Add(new Timer(200, () => { buffer.Position += new Vector2(Main.Rand.Next(0, 3) - 1, Main.Rand.Next(0, 3) - 1); Console.WriteLine("lol"); }));
            anticorps.Add(buffer);
        }

        public void RemoveAnti()
        {
            if (anticorps.Count > 0)
            {
                anticorps[0].End = true;
            }

        }


        public void Update(float time)
        {
            foreach (var item in anticorps)
            {
                item.Update(time);
            }
            anticorps.RemoveAll(k => k.End == true);

            if (Input.MouseOn(Bounds))
            {
                OnBoundsTimer += time;
                if (OnBoundsTimer >= 2000)
                {
                    AddAnti();
                    OnBoundsTimer = 0;
                }
            }
            else
                OnBoundsTimer = 0;
        }


        public void Draw(SpriteBatch batch)
        {
            batch.Draw(textBound, new Vector2(Bounds.X, Bounds.Y), Color.White);
            foreach (var item in anticorps)
            {
                item.Draw(batch);
            }
        }
    }
}
