using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace LudumDare41.GestionPhase
{
    public class BodyPart
    {
        public string name;
        public int AntiNbr;
        //public List<Anticorps> list;
        public float effect;
        public Rectangle Bounds;

        Texture2D textBound;


        public BodyPart(Rectangle bounds, string name)
        {
            effect = 0;
            this.name = name;
            Bounds = bounds;
            textBound = Utils.CreateTexture(Bounds.Width, Bounds.Height, Color.Yellow);
            //list = new List<Anticorps>();
        }

        public void Update(GameTime time)
        {

            //AntiNbr = list.Count;
            //foreach (var anti in list)
            //{
            //    switch (anti.type)
            //    {

            //    }
            //}
        }


        public void Draw(SpriteBatch batch)
        {
            //batch.Draw(textBound, new Vector2(Bounds.X, Bounds.Y), Color.White);
        }
    }
}
