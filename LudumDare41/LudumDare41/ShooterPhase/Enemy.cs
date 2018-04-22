using LudumDare41.Graphics;
using LudumDare41.Screens;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace LudumDare41.ShooterPhase
{
    public enum EnemyState
    {
        LookingForPlayer,
        Aiming,
        Shooting
    }

    public class Enemy : Sprite
    {
        private const int MAX_LIFE = 50;
        private Weapon _weaponHolded;
        private float _rotation;
        private Player _thePlayer;
        private bool _alive = true;
        private int _life;
        private LootType _affiliatedLoot;


        private Camera _currentCamera;

        public Enemy(Texture2D texture, Vector2 position, Player thePlayer, Camera currentCamera) : base(texture, position)
        {
            _currentCamera = currentCamera;
            _thePlayer = thePlayer;
            _life = MAX_LIFE;
            int rnd = Utils.RANDOM.Next(Utils.NUMBER_TYPE_WEAPON);
            switch (rnd)
            {
                case 0:
                    _weaponHolded = new Gun(Position, 30, WeaponState.Holded, _currentCamera);
                    break;
                case 1:
                    _weaponHolded = new SubMachine(Position, 30, WeaponState.Holded, _currentCamera);
                    break;
                case 2:
                    _weaponHolded = new Sniper(Position, 30, WeaponState.Holded, _currentCamera);
                    break;
                default:
                    _weaponHolded = new Gun(Position, 30, WeaponState.Holded, _currentCamera);
                    break;
            }

            Array values = Enum.GetValues(typeof(LootType));
            _affiliatedLoot = (LootType)values.GetValue(Utils.RANDOM.Next(values.Length));

            _hitbox = new Rectangle(_hitbox.X - Texture.Width / 2, _hitbox.Y - Texture.Height / 2, _hitbox.Width, _hitbox.Height);
        }

        public int Life
        {
            get => _life;
        }

        public bool Alive
        {
            get => _alive;
        }

        public LootType AffiliatedLoot
        {
            get => _affiliatedLoot;
        }

        public void TakeDamage(float amount)
        {
            _life -= (int)amount;
            if (_life <= 0)
            {
                _life = 0;
                _alive = false;
                if (Main.CurrentsScreens[0] is ShooterScreen currentScreen)
                {
                    currentScreen.CreateLootAtPosition(Position, _affiliatedLoot);
                }
                Assets.EnemyDead.Play();
            }
        }

        public void Update(GameTime time)
        {

            float speedFactor = 1f;
            if (Main.CurrentsScreens[0] is ShooterScreen currentScreen)
            {
                speedFactor = currentScreen.TimeScale;
            }

            float distanceWithPlayer;
            Vector2 direction = _thePlayer.Position - Position;
            distanceWithPlayer = (float)Math.Sqrt(Math.Pow(direction.X, 2) + Math.Pow(direction.Y, 2));
            direction.Normalize();

            _rotation = (float)Math.Atan2(direction.Y, direction.X);
            _weaponHolded.FireSpeedModifier = 0.5f;

            if (distanceWithPlayer <= 500)
                _weaponHolded?.Fire(_thePlayer.Position, this, speedFactor);
            if (_weaponHolded != null && _weaponHolded.CanDestroy)
                _weaponHolded = null;
            _weaponHolded?.Update(time);
        }

        public new void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(Texture, Position, null, Color.White, _rotation,
                new Vector2((float)Texture.Width / 2, (float)Texture.Height / 2), 1f,
                SpriteEffects.None, 1f);
            _weaponHolded?.Draw(spriteBatch, _thePlayer.Position);

            //spriteBatch.Draw(_hitboxTexture, _hitbox, Color.White);
        }
    }
}