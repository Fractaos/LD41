﻿using LudumDare41.GestionPhase;
using LudumDare41.Graphics;
using LudumDare41.ShooterPhase;
using LudumDare41.Utility;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;

namespace LudumDare41.Screens
{
    public class GestionScreen : Screen
    {
        UiManager manager;
        List<Anticorps> anticorps;
        List<string> phraseAnti;
        private float _timeElapsedSinceOnScreen;

        public Player _instancePlayer;
        private bool _isActive;

        Anticorps isDragged;
        ProgressBar playerLife;

        //FACTORY
        bool showFactory;
        public AntiFactory factory;

        //BODYPARTS
        public List<BodyPart> Parts;
        public BodyPart Head, Arms, Corps, Legs, None;

        //PROPERTIES
        public int Sucre
        {
            get => factory.sucre;
            set
            {
                factory.sucre = value;
                if (factory.sucre > factory.maxSucre)
                    factory.sucre = factory.maxSucre;
            }
        }

        public int Gras
        {
            get => factory.gras;
            set
            {
                factory.gras = value;
                if (factory.gras > factory.maxGras)
                    factory.gras = factory.maxGras;
            }
        }

        public int VitC
        {
            get => factory.vitC;
            set
            {
                factory.vitC = value;
                if (factory.vitC > factory.maxVitC)
                    factory.vitC = factory.maxVitC;
            }
        }

        public override void Create()
        {
            phraseAnti = new List<string> { "he", "ok", "how are you today ?", "it's ok", "time to go", "good bye", "hey frank !", "hey mathieu", "celui qui ca est un con", "comment va ?", "la peche", "qu'est ce qu'on est serre", "haha" };
            manager = new UiManager();
            Assets.MusicGestion.Volume = 0.5f;
            Assets.MusicGestion.IsLooped = true;
            Assets.MusicGestion.Play();
            showFactory = false;
            factory = new AntiFactory(this);
            if (Main.CurrentsScreens[0] is ShooterScreen currentScreen)
                _instancePlayer = currentScreen.Player;

            playerLife = new ProgressBar(new Vector2(1228, 547), 303, 50, Color.Red, _instancePlayer.MaxLife, true);

            anticorps = new List<Anticorps>();

            #region Ajout des parties du corps

            Head = new BodyPart(new Rectangle(300, 135, 267, 169), "Head", this, 0);
            Arms = new BodyPart(new Rectangle(300, 312, 267, 169), "Arms", this, 1);
            Corps = new BodyPart(new Rectangle(300, 490, 267, 169), "Corps", this, 2);
            Legs = new BodyPart(new Rectangle(300, 670, 267, 169), "Legs", this, 3);
            None = new BodyPart(new Rectangle(597, 134, 347, 705), "None", this, 4);

            Parts = new List<BodyPart> { Head, Arms, Corps, Legs, None };

            manager.AddParticle(new UiButton(new Vector2(50, 150), () => { showFactory = !showFactory; }, Assets.FactoryButton));

            #endregion 
        }

        public bool IsActive
        {
            get => _isActive;
            set => _isActive = value;
        }

        public override void Update(GameTime time)
        {
            #region Factory


            if (showFactory)
            {
                factory.Update(time);
                if(!Input.MouseOn(factory.Bounds) && Input.Left(true))
                {
                    showFactory = false;
                }
            }
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

            if (_isActive)
            {
                if (isDragged != null)
                {
                    if (!Input.Left(false))
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
            }

            #endregion

            #region Update NBR body parts

            Head.list = anticorps.FindAll(anti => anti.ActualPart == Head);
            Arms.list = anticorps.FindAll(anti => anti.ActualPart == Arms);
            Corps.list = anticorps.FindAll(anti => anti.ActualPart == Corps);
            Legs.list = anticorps.FindAll(anti => anti.ActualPart == Legs);

            #endregion

            #region Update BodyPart on Player Stats

            _instancePlayer.Accuracy = 1f + (Head.effect / 100);
            _instancePlayer.VisionRange = 200 + (int)Head.effect;
            _instancePlayer.ShootSpeed = 1f + (Arms.effect / 100);
            _instancePlayer.MoveSpeed = 0.6f + ((Legs.effect / 2) / 100);
            _instancePlayer.MaxLife = 50 + (int)Corps.effect;

            #endregion

            if (_isActive)
                _timeElapsedSinceOnScreen += time.ElapsedGameTime.Milliseconds;

            if (_timeElapsedSinceOnScreen > Utils.TIME_ON_SCREEN)
            {
                if (Input.KeyPressed(Keys.Tab, true))
                {
                    _isActive = false;
                    Assets.MusicGestion.Volume = 0;
                    _timeElapsedSinceOnScreen = 0;
                    Main.SetScreenWithoutReCreating(Main.CurrentsScreens[0]);
                }
            }

            playerLife.Update(time, _instancePlayer.Life);
            playerLife.MaxValue = _instancePlayer.MaxLife;


        }

        public override void Draw()
        {
            spriteBatch.Begin();
            spriteBatch.Draw(Assets.BackgroundGestion, Vector2.Zero, Color.White);
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
            spriteBatch.DrawString(Assets.BigFont, "Accuracy : " + _instancePlayer.Accuracy, new Vector2(1220, 220), Color.White);
            spriteBatch.DrawString(Assets.BigFont, "Vision Range : " + _instancePlayer.VisionRange, new Vector2(1220, 320), Color.White);
            spriteBatch.DrawString(Assets.BigFont, "Shoot Speed : " + _instancePlayer.ShootSpeed, new Vector2(1220, 420), Color.White);

            spriteBatch.DrawString(Assets.Font, "Head +" + Head.effect + "%", new Vector2(10, 30), Color.White);
            spriteBatch.DrawString(Assets.Font, "Arms +" + Arms.effect + "%", new Vector2(100, 30), Color.White);
            spriteBatch.DrawString(Assets.Font, "Corps +" + Corps.effect + "%", new Vector2(200, 30), Color.White);
            spriteBatch.DrawString(Assets.Font, "Legs +" + Legs.effect + "%", new Vector2(300, 30), Color.White);

            ShooterScreen shooterScreen = (ShooterScreen)Main.CurrentsScreens[0];
            spriteBatch.DrawString(Assets.BigFont, "Time elapsed in other mode : " + (_timeElapsedSinceOnScreen / 1000 * shooterScreen.TimeScale).ToString("0.0") + " (in seconds)", new Vector2(1170, 30), Color.White);

            #endregion
            if (showFactory)
                factory.Draw(spriteBatch);
            playerLife.Draw(spriteBatch);
            spriteBatch.End();

        }

        public override void Resume()
        {
            _isActive = true;

            Assets.MusicGestion.Volume = 0.5f;
            Main.Instance.IsMouseVisible = true;
        }

        public void AddAntiCorps(AntiType type, BodyPart part)
        {
            Anticorps buffer = new Anticorps(new Vector2(Main.Rand.Next(None.Bounds.X, None.Bounds.X + None.Bounds.Width), Main.Rand.Next(None.Bounds.Y, None.Bounds.Y + None.Bounds.Height)), type, None);
            TimerManager.Timers.Add(new Timer(3500, () => { manager.AddParticle(new UiLabel(phraseAnti[Main.Rand.Next(phraseAnti.Count)], buffer.Position, 400, 1, Color.White)); }));
            TimerManager.Timers.Add(new Timer(1500, () => { buffer.Position += new Vector2(Main.Rand.Next(0, 3) - 1, Main.Rand.Next(0, 3) - 1); }));
            anticorps.Add(buffer);
        }
    }
}
