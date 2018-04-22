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
        Shooting
    }

    public class Enemy : Sprite
    {
        private const int MAX_LIFE = 50;
        private Weapon _weaponHolded;
        private float _rotation = 0;
        private Player _thePlayer;
        private bool _alive = true;
        private int _life;

        private float _speed = 0.1f;
        private EnemyState _enemyState;
        private LootType _affiliatedLoot;


        private Camera _currentCamera;

        public Enemy(Texture2D texture, Vector2 position, Player thePlayer, Camera currentCamera) : base(texture, position)
        {
            _currentCamera = currentCamera;
            _thePlayer = thePlayer;
            _life = MAX_LIFE;
            _enemyState = EnemyState.LookingForPlayer;
            int rnd = Utils.RANDOM.Next(Utils.NUMBER_TYPE_WEAPON);
            switch (rnd)
            {
                case 0:
                    _weaponHolded = new Gun(Position, 150, WeaponState.Holded, _currentCamera);
                    break;
                case 1:
                    _weaponHolded = new SubMachine(Position, 150, WeaponState.Holded, _currentCamera);
                    break;
                case 2:
                    _weaponHolded = new Sniper(Position, 150, WeaponState.Holded, _currentCamera);
                    break;
                default:
                    _weaponHolded = new Gun(Position, 150, WeaponState.Holded, _currentCamera);
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
                    if (_weaponHolded != null)
                    {
                        _weaponHolded.WeaponState = WeaponState.OnFloor;
                        _weaponHolded.Position.X += 100;
                        currentScreen.CreateWeapon(_weaponHolded);
                    }
                }
                Assets.EnemyDead.Play();
            }
        }

        public void Update(GameTime time)
        {
            ShooterScreen currentScreen = (ShooterScreen)Main.CurrentsScreens[0];
            var speedFactor = currentScreen.TimeScale;
            switch (_enemyState)
            {
                case EnemyState.LookingForPlayer:
                    var direction = _thePlayer.Position - Position;
                    var distanceWithPlayer = (float)Math.Sqrt(Math.Pow(direction.X, 2) + Math.Pow(direction.Y, 2));
                    direction.Normalize();
                    if ((distanceWithPlayer <= 500 && distanceWithPlayer >= 400))
                        _enemyState = EnemyState.Shooting;
                    else
                    {
                        _speed = Math.Abs(distanceWithPlayer - 500) / 1000;
                        if (_speed <= 0.1f)
                            _speed = 0.1f;
                        else if (_speed >= 2f)
                            _speed = 2f;
                        if (distanceWithPlayer < 400)
                            direction *= -1;
                        Position.X += direction.X * time.ElapsedGameTime.Milliseconds * _speed;
                        Position.Y += direction.Y * time.ElapsedGameTime.Milliseconds * _speed;
                        _weaponHolded.Position = Position;
                        if (currentScreen.AreneBounds.Top + Hitbox.Height / 2 >= Position.Y)
                            Position.Y = currentScreen.AreneBounds.Top + Hitbox.Height / 2;

                        if (currentScreen.AreneBounds.Bottom - Hitbox.Height / 2 <= Position.Y)
                            Position.Y = currentScreen.AreneBounds.Bottom - Hitbox.Height / 2;

                        if (currentScreen.AreneBounds.Left + Hitbox.Width / 2 >= Position.X)
                            Position.X = currentScreen.AreneBounds.Left + Hitbox.Height / 2;

                        if (currentScreen.AreneBounds.Right - Hitbox.Width / 2 <= Position.X)
                            Position.X = currentScreen.AreneBounds.Right - Hitbox.Height / 2;


                        UpdateHitbox(new Vector2(Position.X - (float)Texture.Width / 2, Position.Y - (float)Texture.Height / 2));




                    }
                    break;
                case EnemyState.Shooting:
                    _weaponHolded.FireSpeedModifier = 0.5f;
                    _weaponHolded?.Fire(_thePlayer.Position, this, speedFactor);
                    _enemyState = EnemyState.LookingForPlayer;
                    break;
            }


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