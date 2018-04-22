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
    public enum AntiType { Normal, Neighbour, Leader }

    public class Anticorps : Sprite
    {
        Texture2D secondTexture;
        public BodyPart ActualPart;
        public bool Dragged;
        AntiType type;

        public bool End;

        public Anticorps(Vector2 position, AntiType type, BodyPart part = null) : base(Assets.PixelB, position)
        {
            switch(type)
            {
                case AntiType.Normal:
                    Texture = Utils.CreateTexture(40, 40, Color.Red);
                    break;
                case AntiType.Neighbour:
                    Texture = Utils.CreateTexture(40, 40, Color.Yellow);
                    break;
                case AntiType.Leader:
                    Texture = Utils.CreateTexture(40, 40, Color.Blue);
                    break;
            }
            _hitbox = new Rectangle((int)Position.X, (int)Position.Y, Texture.Width, Texture.Height);
            this.type = type;
            secondTexture = Utils.CreateContouringTexture(40, 40, Color.White);
            End = false;
            ActualPart = part;
        }

        public void Update(float time)
        {
            UpdateHitbox(Position);
            if (ActualPart != null)
                Position = Vector2.Clamp(Position, new Vector2(ActualPart.Bounds.X, ActualPart.Bounds.Y),
                    new Vector2(ActualPart.Bounds.X + ActualPart.Bounds.Width - 40, ActualPart.Bounds.Y + ActualPart.Bounds.Height - 40));

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
