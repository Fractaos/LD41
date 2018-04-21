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

        Anticorps isDragged;

        //FACTORY
        bool showFactory;
        AntiFactory factory;

        //BODYPARTS
        public static List<BodyPart> Parts;
        BodyPart Head, Arms, Corps, Legs;

        public override void Create()
        {
            //isDragged = false;
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

            #region Ajout des Boutons
            manager.AddParticle(new UiButton(new Vector2(50, 300), 100, 50, () => { lol["None"]++; }, Color.White));


            manager.AddParticle(new UiButton(new Vector2(300, 180), 50, 50, () => { GestionAnti(true, Organ.Head); }, Color.White));
            manager.AddParticle(new UiButton(new Vector2(719, 176), 50, 50, () => { GestionAnti(true, Organ.Arms); }, Color.White));
            manager.AddParticle(new UiButton(new Vector2(1010, 193), 50, 50, () => { GestionAnti(true, Organ.Corps); }, Color.White));
            manager.AddParticle(new UiButton(new Vector2(1507, 182), 50, 50, () => { GestionAnti(true, Organ.Legs); }, Color.White));

            manager.AddParticle(new UiButton(new Vector2(300, 236), 50, 50, () => { GestionAnti(false, Organ.Head); }, Color.Red));
            manager.AddParticle(new UiButton(new Vector2(719, 232), 50, 50, () => { GestionAnti(false, Organ.Arms); }, Color.Red));
            manager.AddParticle(new UiButton(new Vector2(1010, 249), 50, 50, () => { GestionAnti(false, Organ.Corps); }, Color.Red));
            manager.AddParticle(new UiButton(new Vector2(1507, 238), 50, 50, () => { GestionAnti(false, Organ.Legs); }, Color.Red));
            #endregion
            #region Ajout des parties du corps

            Head = new BodyPart(new Rectangle(148, 163, 148, 135), "Head");
            Arms = new BodyPart(new Rectangle(587, 166, 128, 133), "Arms");
            Corps = new BodyPart(new Rectangle(898, 191, 107, 108), "Corps");
            Legs = new BodyPart(new Rectangle(1383, 179, 120, 116), "Legs");

            Parts = new List<BodyPart> { Head, Arms, Corps, Legs };

            manager.AddParticle(new UiButton(new Vector2(50, 50), 100, 50, () => { AddAntiCorps(Head); }, Color.White));

            #endregion 
        }

        public override void Update(GameTime time)
        {
            #region Factory
            if (Input.KeyPressed(Keys.A, true))
                showFactory = !showFactory;

            if (showFactory)
                factory.Update(time.ElapsedGameTime.Milliseconds);
            else
                manager.Update(time.ElapsedGameTime.Milliseconds);
            #endregion
            #region Gestion Anticorps/BodyParts
            foreach (var item in Parts)
            {
                item.Update(time.ElapsedGameTime.Milliseconds);
            }

            foreach (var item in anticorps)
            {
                item.Update(time.ElapsedGameTime.Milliseconds);
            }
            #endregion
            #region DragnDrop

            if (isDragged != null)
            {
                if (Input.Left(true))
                {
                    BodyPart buffer = Parts.Find(part => part.Bounds.Contains(Input.MousePos));
                    if (buffer != null)
                    {
                        isDragged.ChangePart(buffer);
                    }
                    else
                    {
                        isDragged.ChangePart(null);
                    }
                    isDragged.Dragged = false;
                    isDragged = null;
                }
            }
            else if (isDragged == null)
            {
                foreach (var item in anticorps)
                {
                    if (Input.Left(true) && Input.MouseOn(item.Hitbox))
                    {
                        isDragged = item;
                    }
                }
                if (isDragged != null)
                {
                    isDragged.Dragged = true;
                }
            }
            #endregion
            #region Update NBR body parts
            Head.AntiNbr = anticorps.FindAll(anti => anti.ActualPart == Head).Count;
            Arms.AntiNbr = anticorps.FindAll(anti => anti.ActualPart == Arms).Count;
            Corps.AntiNbr = anticorps.FindAll(anti => anti.ActualPart == Corps).Count;
            Legs.AntiNbr = anticorps.FindAll(anti => anti.ActualPart == Legs).Count;
            #endregion  

        }

        public override void Draw()
        {
            spriteBatch.Begin();
            spriteBatch.Draw(Assets.backgroundGestion, Vector2.Zero, Color.White);
            manager.Draw(spriteBatch);

            if (showFactory)
                factory.Draw(spriteBatch);

            #region Draw Anticorps/BodyParts
            foreach (var item in anticorps)
            {
                item.Draw(spriteBatch);
            }

            foreach (var item in Parts)
            {
                item.Draw(spriteBatch);
            }
            #endregion
            #region Draw BodyPartNbr
            spriteBatch.DrawString(Assets.Font, "Head " + Head.AntiNbr, new Vector2(10, 10), Color.White);
            spriteBatch.DrawString(Assets.Font, "Arms" + Arms.AntiNbr, new Vector2(100, 10), Color.White);
            spriteBatch.DrawString(Assets.Font, "Corps" + Corps.AntiNbr, new Vector2(200, 10), Color.White);
            spriteBatch.DrawString(Assets.Font, "Legs" + Legs.AntiNbr, new Vector2(300, 10), Color.White);
            #endregion
            spriteBatch.End();
        }

        public void GestionAnti(bool operation, Organ organ)
        {
            if (operation && lol["None"] > 0)
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
            else if (!operation && lol[organ.ToString()] > 0)
            {
                switch (organ)
                {
                    case Organ.Head:
                        if (lol["Head"] > 0)
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

        public void AddAntiCorps(BodyPart part)
        {
            Anticorps buffer = new Anticorps(new Vector2(900, 50));
            TimerManager.Timers.Add(new Timer(200, () => { buffer.Position += new Vector2(Main.Rand.Next(0, 3) - 1, Main.Rand.Next(0, 3) - 1); Console.WriteLine("lol"); }));
            anticorps.Add(buffer);
        }
    }
}
