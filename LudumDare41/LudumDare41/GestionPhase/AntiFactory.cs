using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using LudumDare41.Utility;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using LudumDare41.Screens;

namespace LudumDare41.GestionPhase
{
    public class AntiFactory
    {
        UiManager manager;
        private GestionScreen _instance;
        public Vector2 position;
        public Texture2D texture;

        public int sucre, vitC, gras;

        public AntiFactory(GestionScreen instance)
        {
            manager = new UiManager();
            _instance = instance;
            position = new Vector2(200, 200);
            texture = Utils.CreateTexture(1000, 600, Color.Gray);
            manager.AddParticle(new UiButton(new Vector2(position.X + 50, position.Y + 50), 50, 50, () => { instance.lol["None"]++; }, Color.Red));
            manager.AddParticle(new UiButton(new Vector2(position.X + 150, position.Y + 50), 50, 50, () => { instance.lol["None"]++; }, Color.Red));
            manager.AddParticle(new UiButton(new Vector2(position.X + 250, position.Y + 50), 50, 50, () => { instance.lol["None"]++; }, Color.Red));
        }


        public void Update(float time)
        {
            manager.Update(time);
        }


        public void Draw(SpriteBatch batch)
        {
            batch.Draw(texture, position, Color.White);
            manager.Draw(batch);
        }
    }
}
