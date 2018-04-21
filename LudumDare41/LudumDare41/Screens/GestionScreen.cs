using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

using LudumDare41.GestionPhase;
using LudumDare41.Utility;
using LudumDare41.GestionPhase;

using Microsoft.Xna.Framework.Graphics;
using LudumDare41.Graphics;
using Microsoft.Xna.Framework.Input;

namespace LudumDare41.Screens
{
    public class GestionScreen : Screen
    {
        UiManager manager = new UiManager();
        List<Anticorps> anticorps;
        Texture2D textAnti;
        public Dictionary<string, int> lol;

        //FACTORY
        bool showFactory;
        AntiFactory factory;

        public int sucre, gras, vitc;

        public int Vision
        {
            get { return 50 + (lol["Head"] * 10); }
        }

        public int VitesseTir
        {
            get { return 50 + (lol["Arms"] * 10); }
        }

        public int VieMax
        {
            get { return 50 + (lol["Corps"] * 10); }
        }

        public int VitesseDeplacement
        {
            get { return 50 + (lol["Legs"] * 10); }
        }

        public override void Create()
        {
            showFactory = false;
            factory = new AntiFactory(this);

            textAnti = Utils.CreateTexture(40, 40, Color.Red);
            anticorps = new List<Anticorps>();
            lol = new Dictionary<string, int>
            {
                {"None", 5 },
                { "Head", 0 },
                {"Arms", 0 },
                {"Corps", 0 },
                { "Legs", 0 }
            };

            manager.AddParticle(new UiButton(new Vector2(50, 300), 100, 50, () => { lol["None"]++; }, Color.White));


            manager.AddParticle(new UiButton(new Vector2(300, 180), 50, 50, () => { GestionAnti(true, Organ.Head); }, Color.White));
            manager.AddParticle(new UiButton(new Vector2(719, 176), 50, 50, () => { GestionAnti(true, Organ.Arms); }, Color.White));
            manager.AddParticle(new UiButton(new Vector2(1010, 193), 50, 50, () => { GestionAnti(true, Organ.Corps); }, Color.White));
            manager.AddParticle(new UiButton(new Vector2(1507, 182), 50, 50, () => { GestionAnti(true, Organ.Legs); }, Color.White));

            manager.AddParticle(new UiButton(new Vector2(300, 236), 50, 50, () => { GestionAnti(false, Organ.Head); }, Color.Red));
            manager.AddParticle(new UiButton(new Vector2(719, 232), 50, 50, () => { GestionAnti(false, Organ.Arms); }, Color.Red));
            manager.AddParticle(new UiButton(new Vector2(1010, 249), 50, 50, () => { GestionAnti(false, Organ.Corps); }, Color.Red));
            manager.AddParticle(new UiButton(new Vector2(1507, 238), 50, 50, () => { GestionAnti(false, Organ.Legs); }, Color.Red));
        }

        public override void Update(GameTime time)
        {
            if (Input.KeyPressed(Keys.A, true))
                showFactory = !showFactory;
            if (showFactory)
                factory.Update(time.ElapsedGameTime.Milliseconds);
            else
                manager.Update(time.ElapsedGameTime.Milliseconds);

        }

        public override void Draw()
        {
            spriteBatch.Begin();
            spriteBatch.Draw(Assets.backgroundGestion, Vector2.Zero, Color.White);
            manager.Draw(spriteBatch);

            #region Affichage nombre anti
            spriteBatch.DrawString(Assets.Font, lol["None"].ToString(), new Vector2(50, 50), Color.White);
            spriteBatch.DrawString(Assets.Font, lol["Head"].ToString(), new Vector2(192, 111), Color.White);
            spriteBatch.DrawString(Assets.Font, lol["Arms"].ToString(), new Vector2(629, 210), Color.White);
            spriteBatch.DrawString(Assets.Font, lol["Corps"].ToString(), new Vector2(933, 227), Color.White);
            spriteBatch.DrawString(Assets.Font, lol["Legs"].ToString(), new Vector2(1423, 221), Color.White);

            spriteBatch.DrawString(Assets.Font, "vision : " + Vision, new Vector2(50, 10), Color.White);
            spriteBatch.DrawString(Assets.Font, "vitessetir : " + VitesseTir, new Vector2(150, 10), Color.White);
            spriteBatch.DrawString(Assets.Font, "vieMax : " + VieMax, new Vector2(300, 10), Color.White);
            spriteBatch.DrawString(Assets.Font, "vitesseDeplacement : " + VitesseDeplacement, new Vector2(400, 10), Color.White);
            #endregion

            #region Affichage Anti
            //for (int i = 0; i < lol["None"]; i++)
            //{
            //    int x = i % 2;
            //    int y = i / 2;

            //    spriteBatch.Draw(textAnti, new Vector2(50 + (x * 40) + 5, 100 + (y * 40) + 5), Color.White);
            //}

            //for (int i = 0; i < lol["Head"]; i++)
            //{
            //    int x = i % 2;
            //    int y = i / 2;

            //    spriteBatch.Draw(textAnti, new Vector2(200 + (x * 40) + 5, 100 + (y * 40) + 5), Color.White);
            //}

            //for (int i = 0; i < lol["Arms"]; i++)
            //{
            //    int x = i % 2;
            //    int y = i / 2;

            //    spriteBatch.Draw(textAnti, new Vector2(400 + (x * 40) + 5, 100 + (y * 40) + 5), Color.White);
            //}

            //for (int i = 0; i < lol["Corps"]; i++)
            //{
            //    int x = i % 2;
            //    int y = i / 2;

            //    spriteBatch.Draw(textAnti, new Vector2(600 + (x * 40) + 5, 100 + (y * 40) + 5), Color.White);
            //}

            //for (int i = 0; i < lol["Legs"]; i++)
            //{
            //    int x = i % 2;
            //    int y = i / 2;

            //    spriteBatch.Draw(textAnti, new Vector2(800 + (x * 40) + 5, 100 + (y * 40) + 5), Color.White);
            //}
            #endregion

            if (showFactory)
                factory.Draw(spriteBatch);

            spriteBatch.End();
        }

        public void GestionAnti(bool operation, Organ organ)
        {
            if (operation && lol["None"]>0)
            {
                switch (organ)
                {
                    case Organ.Head:
                        lol["Head"]++;
                        break;
                    case Organ.Arms:
                        lol["Arms"]++;
                        break;
                    case Organ.Corps:
                        lol["Corps"]++;
                        break;
                    case Organ.Legs:
                        lol["Legs"]++;
                        break;
                }

                lol["None"]--;
            }
            else if(!operation && lol[organ.ToString()]>0)
            {
                switch (organ)
                {
                    case Organ.Head:
                        if(lol["Head"]>0)
                            lol["Head"]--;
                        break;
                    case Organ.Arms:
                        if (lol["Arms"] > 0)
                            lol["Arms"]--;
                        break;
                    case Organ.Corps:
                        if (lol["Corps"] > 0)
                            lol["Corps"]--;
                        break;
                    case Organ.Legs:
                        if (lol["Legs"] > 0)
                            lol["Legs"]--;
                        break;
                }
                lol["None"]++;
            }
        }
    }
}
