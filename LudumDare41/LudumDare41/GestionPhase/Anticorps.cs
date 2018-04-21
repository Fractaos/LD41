using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using LudumDare41.Graphics;
using Microsoft.Xna.Framework;

namespace LudumDare41.GestionPhase
{
    public enum Organ { None, Head, Arms, Corps, Legs}

    public class Anticorps : Sprite
    {
        public int level;
        public Bodypart ActualPart;

        public bool End;

        public Anticorps(Vector2 position, Bodypart part = null) : base(Utils.CreateTexture(40, 40, Color.Red), position)
        {
            End = false;
            ActualPart = part;
        }

        public void Update(float time)
        {

        }

    }
}
