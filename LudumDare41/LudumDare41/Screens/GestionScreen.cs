using LudumDare41.GestionPhase;
using LudumDare41.Graphics;
using LudumDare41.Utility;
using Microsoft.Xna.Framework;
using System.Collections.Generic;

namespace LudumDare41.Screens
{
    public class GestionScreen : Screen
    {
        UiManager manager = new UiManager();
        List<Anticorps> anticorps;

        Anticorps isDragged;

        //FACTORY
        bool showFactory;
        AntiFactory factory;

        //BODYPARTS
        public static List<BodyPart> Parts;
        public BodyPart Head, Arms, Corps, Legs, None;

        public override void Create()
        {
            Assets.MusicGestion.Play();
            //isDragged = false;
            showFactory = false;
            factory = new AntiFactory(this);

            anticorps = new List<Anticorps>();

            #region Ajout des parties du corps

            Head = new BodyPart(new Rectangle(300, 135, 267, 169), "Head");
            Arms = new BodyPart(new Rectangle(300, 312, 267, 169), "Arms");
            Corps = new BodyPart(new Rectangle(300, 490, 267, 169), "Corps");
            Legs = new BodyPart(new Rectangle(300, 670, 267, 169), "Legs");
            None = new BodyPart(new Rectangle(597, 134, 347, 705), "None");

            Parts = new List<BodyPart> { Head, Arms, Corps, Legs, None };

            manager.AddParticle(new UiButton(new Vector2(50, 50), 100, 50, () => { showFactory = !showFactory; }, Color.White));

            #endregion 
        }

        public override void Update(GameTime time)
        {
            #region Factory
            if (showFactory)
                factory.Update(time);
            manager.Update(time.ElapsedGameTime.Milliseconds);

            #endregion
            #region Gestion Anticorps/BodyParts
            foreach (var item in Parts)
            {
                item.Update(time);
            }

            foreach (var item in anticorps)
            {
                item.Update(time);
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
                        Assets.Drop.Play();
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
                    Assets.Drag.Play();
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

            spriteBatch.DrawString(Assets.Font, "IsDragged" + anticorps.Find(anti => anti.Hitbox.Contains(Input.MousePos)), new Vector2(450, 10), Color.White);

            #endregion
            if (showFactory)
                factory.Draw(spriteBatch);

            spriteBatch.End();
        }

        public void AddAntiCorps(AntiType type, BodyPart part)
        {
            Anticorps buffer = new Anticorps(new Vector2(Main.Rand.Next(None.Bounds.X, None.Bounds.X + None.Bounds.Width), Main.Rand.Next(None.Bounds.Y, None.Bounds.Y + None.Bounds.Height)), type, None);
            TimerManager.Timers.Add(new Timer(200, () => { buffer.Position += new Vector2(Main.Rand.Next(0, 3) - 1, Main.Rand.Next(0, 3) - 1); }));
            anticorps.Add(buffer);
        }
    }
}
