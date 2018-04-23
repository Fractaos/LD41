
using LudumDare41.Screens;
using LudumDare41.Utility;
using LudumDare41.Graphics;
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
        public int maxSucre, maxGras, maxVitC;

        public AntiFactory(GestionScreen instance)
        {
            sucre = vitC = gras = maxSucre = maxGras = maxVitC = 100;
            manager = new UiManager();
            _instance = instance;
            position = new Vector2(200, 200);
            texture = Utils.CreateTexture(1000, 400, Color.Gray);
            manager.AddParticle(new UiButton(new Vector2(position.X + 300, position.Y + 100), () => { BuyAnticorps(AntiType.Normal); }, Assets.AddAnti));
            manager.AddParticle(new UiButton(new Vector2(position.X + 300, position.Y + 200), () => { BuyAnticorps(AntiType.Neighbour); }, Assets.AddAnti));
            manager.AddParticle(new UiButton(new Vector2(position.X + 300, position.Y + 300), () => { BuyAnticorps(AntiType.Leader); }, Assets.AddAnti));


            listProgress = new ProgressBar[3];
            sucreBar = new ProgressBar(new Vector2(position.X + 50, position.Y + 100), 200, 50, Color.Red, maxSucre, true);
            grasBar = new ProgressBar(new Vector2(position.X + 50, position.Y + 200), 200, 50, Color.Yellow, maxGras, true);
            vitCBar = new ProgressBar(new Vector2(position.X + 50, position.Y + 300), 200, 50, Color.Green, maxVitC, true);

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
            batch.DrawString(Assets.Font, "Sucre", new Vector2(sucreBar.Position.X, sucreBar.Position.Y + 30), Color.White);
            batch.DrawString(Assets.Font, "Gras", new Vector2(grasBar.Position.X, grasBar.Position.Y + 30), Color.Black);
            batch.DrawString(Assets.Font, "Vitamine C", new Vector2(vitCBar.Position.X, vitCBar.Position.Y + 30), Color.White);

            batch.DrawString(Assets.Font, "- Cellule basique, ajoutant 10% d'avantage la ou vous la posez. Coute 20 Sucre", new Vector2(position.X + 400, position.Y + 125), Color.White);
            batch.DrawString(Assets.Font, "- Coute 20 Sucre", new Vector2(position.X + 400, position.Y + 145), Color.Red);
            batch.DrawString(Assets.Font, "- Cellule bonne voisine, ajoutant 5% la ou vous la posez,", new Vector2(position.X + 400, position.Y + 225), Color.White);
            batch.DrawString(Assets.Font, " et 5% aux parties du corps adjacentes .", new Vector2(position.X + 400, position.Y + 245), Color.White);
            batch.DrawString(Assets.Font, " - Coute 10 gras et 20 sucre", new Vector2(position.X + 400, position.Y + 265), Color.Red);
            batch.DrawString(Assets.Font, "- Cellule Leader, n'apporte aucun avantage seul, mais ajoute 2%.", new Vector2(position.X + 400, position.Y + 325), Color.White);
            batch.DrawString(Assets.Font, "pour chaque cellule dans la partie du corps ou elle se trouve.", new Vector2(position.X + 400, position.Y + 345), Color.White);
            batch.DrawString(Assets.Font, "- Coute 10 Vitamine C, 20 Gras et 30 Sucre", new Vector2(position.X + 400, position.Y + 365), Color.Red);

        }
    }
}
