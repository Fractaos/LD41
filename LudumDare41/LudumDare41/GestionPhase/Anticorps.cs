using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using LudumDare41.Graphics;
using Microsoft.Xna.Framework;
using LudumDare41.Utility;
using LudumDare41.Screens;
using LudumDare41.GestionPhase;
using Microsoft.Xna.Framework.Graphics;

namespace LudumDare41.GestionPhase
{
    public enum Organ { None, Head, Arms, Corps, Legs }

    public class Anticorps : Sprite
    {
        Texture2D secondTexture;
        public BodyPart ActualPart;
        public bool Dragged;

        public bool End;

        public Anticorps(Vector2 position, BodyPart part = null) : base(Utils.CreateTexture(40, 40, Color.Red), position)
        {
            secondTexture = Utils.CreateContouringTexture(40, 40, Color.White);
            End = false;
            ActualPart = part;
        }

        public void Update(float time)
        {
            if (ActualPart != null)
                Position = Vector2.Clamp(Position, new Vector2(ActualPart.Bounds.X, ActualPart.Bounds.Y),
                    new Vector2(ActualPart.Bounds.X + ActualPart.Bounds.Width - 40, ActualPart.Bounds.Y + ActualPart.Bounds.Height - 40));

            ////DRAGNDROP
            //if(Input.Left(true) && Dragged)
            //{
            //    BodyPart buffer = GestionScreen.Parts.Find(part => part.Bounds.Contains(Input.MousePos));
            //    if (buffer != null)
            //    {
            //        ChangePart(buffer);
            //    }

            //    Dragged = false;
            //}
            //else if(Input.Left(true) && Input.MouseOn(Hitbox) && !Dragged)
            //{
            //    Dragged = true;
            //}

            if(Dragged)
            {
                Position = Input.MousePos;
            }
        }


        public void ChangePart(BodyPart newPart)
        {
            ActualPart = newPart;
        }


        public override void Draw(SpriteBatch batch)
        {
            base.Draw(batch);
            batch.Draw(secondTexture, Position, Color.White);
        }
    }
}
