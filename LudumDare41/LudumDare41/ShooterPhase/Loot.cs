using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using LudumDare41.Graphics;
using Microsoft.Xna.Framework;
using LudumDare41.Screens;
using Microsoft.Xna.Framework.Graphics;

namespace LudumDare41.ShooterPhase
{
    public enum LootType { Sucre, Gras, VitC }

    public class Loot : Sprite
    {
        public bool toRemove;
        private Player _instancePlayer;
        private GestionScreen _instanceGestion;
        public LootType _type;
        public Loot(Vector2 position, LootType type) : base(Assets.PixelB, position)
        {
            toRemove = false;
            if (Main.CurrentsScreens[0] is ShooterScreen currentScreen)
                _instancePlayer = currentScreen.GetPlayer;
            if (Main.CurrentsScreens[1] is GestionScreen secondCurrentScreen)
                _instanceGestion = secondCurrentScreen;
            _type = type;
            switch (type)
            {
                case LootType.Sucre:
                    _texture = Assets.Sucre;
                    break;
                case LootType.Gras:
                    _texture = Assets.Gras;
                    break;
                case LootType.VitC:
                    _texture = Assets.VitC;
                    break;
            }
            UpdateHitbox(Position);
        }

        public void Update()
        {
            if (_instancePlayer.Hitbox.Contains(Hitbox))
            {
                switch (_type)
                {
                    case LootType.Sucre:
                        _instanceGestion.Sucre += 30;
                        break;
                    case LootType.Gras:
                        _instanceGestion.Gras += 30;
                        break;
                    case LootType.VitC:
                        _instanceGestion.VitC += 30;
                        break;
                }
                toRemove = true;
            }
        }
    }
}
