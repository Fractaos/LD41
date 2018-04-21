using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using LudumDare41.Graphics;

namespace LudumDare41.GestionPhase
{
    public enum Organ { None, Head, Arms, Corps, Legs}

    public class Anticorps
    {
        public int level;
        public Bodypart ActualPart;

        public Anticorps()
        {
            ActualPart = null;
        }

        public void Update(float time)
        {

        }

    }
}
