
using LudumDare41.Graphics;
using LudumDare41.Screens;
using Microsoft.Xna.Framework;
using System;

namespace LudumDare41.ShooterPhase
{
    public enum LootType { Sucre, Gras, VitC }

    public class Loot : Sprite
    {
        private bool _toRemove;
        private Player _instancePlayer;
        private GestionScreen _instanceGestion;
        public LootType _type;
        public Loot(Vector2 position, LootType type) : base(Assets.PixelB, position)
        {
            _toRemove = false;
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
        }

        public Loot(Vector2 position) : base(Assets.PixelB, position)
        {
            _toRemove = false;
            if (Main.CurrentsScreens[0] is ShooterScreen currentScreen)
                _instancePlayer = currentScreen.GetPlayer;
            if (Main.CurrentsScreens[1] is GestionScreen secondCurrentScreen)
                _instanceGestion = secondCurrentScreen;
            Array values = Enum.GetValues(typeof(LootType));
            _type = (LootType)values.GetValue(Utils.RANDOM.Next(values.Length));
            switch (_type)
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
        }

        public bool ToRemove
        {
            get => _toRemove;
            set => _toRemove = value;
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

                _instancePlayer.Life += 10;
                _toRemove = true;
            }

            UpdateHitbox(Position);
        }
    }
}
