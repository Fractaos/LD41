using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using LudumDare41.Screens;

namespace LudumDare41.GestionPhase
{
    public class BodyPart
    {
        private GestionScreen _instance;
        public string name;
        public int AntiNbr;
        public List<Anticorps> list;
        public float effect, effectBuffer;
        public Rectangle Bounds;
        int buffer;

        Texture2D textBound;


        public BodyPart(Rectangle bounds, string name, GestionScreen instance, int index)
        {
            _instance = instance;
            buffer = index;
            effect = 0;
            this.name = name;
            Bounds = bounds;
            textBound = Utils.CreateTexture(Bounds.Width, Bounds.Height, Color.Yellow);
            list = new List<Anticorps>();
        }

        public void Update(GameTime time)
        {
            AntiNbr = list.Count;
            foreach (var anti in list)
            {
                effectBuffer = 0;
                switch (anti.type)
                {
                    case AntiType.Normal:
                        effectBuffer += 10;
                        break;
                    case AntiType.Neighbour:
                        effectBuffer += 5;
                        if (buffer - 1 >= 0 && _instance.Parts[buffer - 1].name != "None")
                            _instance.Parts[buffer - 1].effectBuffer += 5;
                        if(buffer + 1 <= 4 && _instance.Parts[buffer + 1].name != "None")
                            _instance.Parts[buffer +1].effectBuffer += 5;
                        break;
                    case AntiType.Leader:
                        effectBuffer += (list.FindAll(k => k.type != AntiType.Leader).Count * 2);
                        break;
                }
                effect = effectBuffer;
            }
        }


        public void Draw(SpriteBatch batch)
        {
            //batch.Draw(textBound, new Vector2(Bounds.X, Bounds.Y), Color.White);
        }
    }
}
