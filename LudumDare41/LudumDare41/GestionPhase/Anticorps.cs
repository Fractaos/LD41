using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using LudumDare41.Graphics;
using Microsoft.Xna.Framework;
using LudumDare41.Utility;

namespace LudumDare41.GestionPhase
{
    public enum Organ { None, Head, Arms, Corps, Legs}

    public class Anticorps : Sprite
    {
        public int level;
        public Bodypart ActualPart;
        public bool Dragged;

        public bool End;

        public Anticorps(Vector2 position, Bodypart part = null) : base(Utils.CreateTexture(40, 40, Color.Red), position)
        {
            End = false;
            ActualPart = part;
        }

        public void Update(float time)
        {
            if (ActualPart != null)
                Position = Vector2.Clamp(Position, new Vector2(ActualPart.Bounds.X, ActualPart.Bounds.Y),
                    new Vector2(ActualPart.Bounds.X + ActualPart.Bounds.Width, ActualPart.Bounds.Y + ActualPart.Bounds.Height));
            if (Input.Left(true) && Dragged)
                Dragged = false;
            else if (Input.Left(true) && Input.MouseOn(Hitbox))
                Dragged = true;

            if (Dragged)
            {
                Position = Input.MousePos;
            }
        }

    }
}
