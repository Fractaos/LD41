
using LudumDare41.Screens;
using LudumDare41.Utility;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace LudumDare41.GestionPhase
{
    public class AntiFactory
    {
        ProgressBar sucreBar, vitCBar, grasBar;
        ProgressBar[] listProgress;
        UiManager manager;
        private GestionScreen _instance;
        public Vector2 position;
        public Texture2D texture;

        public int sucre, vitC, gras;

        public AntiFactory(GestionScreen instance)
        {
            sucre = vitC = gras = 10000;
            manager = new UiManager();
            _instance = instance;
            position = new Vector2(200, 200);
            texture = Utils.CreateTexture(1000, 600, Color.Gray);
            manager.AddParticle(new UiButton(new Vector2(position.X + 300, position.Y + 100), 50, 50, () => { BuyAnticorps(AntiType.Normal); }, Color.Red));
            manager.AddParticle(new UiButton(new Vector2(position.X + 300, position.Y + 200), 50, 50, () => { BuyAnticorps(AntiType.Neighbour); }, Color.Yellow));
            manager.AddParticle(new UiButton(new Vector2(position.X + 300, position.Y + 300), 50, 50, () => { BuyAnticorps(AntiType.Leader); }, Color.Green));


            listProgress = new ProgressBar[3];
            sucreBar = new ProgressBar(new Vector2(position.X + 50, position.Y + 100), 200, 50, Color.Red, 10000, true);
            grasBar = new ProgressBar(new Vector2(position.X + 50, position.Y + 200), 200, 50, Color.Yellow, 10000, true);
            vitCBar = new ProgressBar(new Vector2(position.X + 50, position.Y + 300), 200, 50, Color.Green, 10000, true);

            listProgress[0] = sucreBar;
            listProgress[1] = grasBar;
            listProgress[2] = vitCBar;
        }

        public void BuyAnticorps(AntiType type)
        {
            bool ok = false;
            switch (type)
            {
                case AntiType.Normal:
                    if (sucre >= 20)
                    {
                        ok = true;
                        sucre -= 20;
                    }
                    break;
                case AntiType.Neighbour:
                    if (sucre >= 20 && gras >= 10)
                    {
                        ok = true;
                        sucre -= 20;
                        gras -= 10;
                    }

                    break;
                case AntiType.Leader:
                    if (sucre >= 30 && gras >= 20 && vitC >= 10)
                    {
                        ok = true;
                        sucre -= 30;
                        gras -= 20;
                        vitC -= 10;
                    }
                    break;
            }

            if (ok)
                _instance.AddAntiCorps(type, _instance.None);

        }


        public void Update(GameTime time)
        {
            manager.Update(time.ElapsedGameTime.Milliseconds);

            listProgress[0].Update(time, sucre);
            listProgress[1].Update(time, gras);
            listProgress[2].Update(time, vitC);
        }


        public void Draw(SpriteBatch batch)
        {
            batch.Draw(texture, position, Color.White);
            manager.Draw(batch);
            foreach (var item in listProgress)
            {
                item.Draw(batch);
            }
        }
    }
}
