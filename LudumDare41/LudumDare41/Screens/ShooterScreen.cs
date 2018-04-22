using LudumDare41.Graphics;
using LudumDare41.ShooterPhase;
using LudumDare41.Utility;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;

namespace LudumDare41.Screens
{
    public class ShooterScreen : Screen
    {

        public Player GetPlayer
        {
            get { return _player; }
        }

        private float _timeScale = 1f;
        private bool _isActive;

        private Player _player;
        private List<Weapon> _weapons;
        private List<Enemy> _enemies;
        private Camera _camera;

        private SoundEffect _music = Assets.MusicShooter;


        public override void Create()
        {
            _camera = new Camera();
            _weapons = new List<Weapon>();
            _enemies = new List<Enemy>();
            Gun gun = new Gun(new Vector2(100, 100), 150, WeaponState.OnFloor, _camera);
            SubMachine subMachine = new SubMachine(new Vector2(650, 700), 15, WeaponState.OnFloor, _camera);
            Sniper sniper = new Sniper(new Vector2(400, 300), 150, WeaponState.OnFloor, _camera);
            _weapons.Add(gun);
            _weapons.Add(subMachine);
            _weapons.Add(sniper);
            _player = new Player(Utils.CreateTexture(50, 50, Color.Blue),
                new Vector2(Utils.WIDTH / 2 - 25, Utils.HEIGHT / 2 - 25), _camera);

            for (int i = 0; i < 1; i++)
            {
                _enemies.Add(new Enemy(Utils.CreateTexture(50, 50, Color.Red),
                    new Vector2(Utils.RANDOM.Next(100, Utils.WIDTH - 200), Utils.RANDOM.Next(100, Utils.HEIGHT - 200)), _player, _camera));
            }


            SoundEffectInstance musicInstance = _music.CreateInstance();
            musicInstance.Volume = 0.5f;
            if (_timeScale > 1)
                musicInstance.Pitch = 1f;
            else
                musicInstance.Pitch = _timeScale - 1;
            musicInstance.IsLooped = true;
            musicInstance.Play();




        }

        public float TimeScale
        {
            get => _timeScale;
            set => _timeScale = value;
        }

        public Camera Camera
        {
            get => _camera;
        }

        public bool IsActive
        {
            get => _isActive;
            set => _isActive = value;
        }

        public bool ProcessBulletCollision(Bullet bullet)
        {
            if (bullet.Hitbox.Intersects(_player.Hitbox) && bullet.Side is Enemy theEnemy)
            {
                bullet.ToDestroy = true;
                _player.TakeDamage(bullet.FromWeapon.Damage);
                if (Utils.PlayerHitted != null)
                {
                    SoundEffectInstance playerHittedInstance = Utils.PlayerHitted.CreateInstance();
                    if (_timeScale > 1)
                        playerHittedInstance.Pitch = 1f;
                    else
                        playerHittedInstance.Pitch = _timeScale - 1;
                }

                return true;
            }

            foreach (var enemy in _enemies)
            {
                if (bullet.Hitbox.Intersects(enemy.Hitbox) && bullet.Side is Player thePlayer)
                {
                    bullet.ToDestroy = true;
                    enemy.TakeDamage(bullet.FromWeapon.Damage * thePlayer.Accuracy);
                    if (Utils.EnemyHitted != null)
                    {
                        SoundEffectInstance enemyHittedInstance = Utils.EnemyHitted.CreateInstance();
                        if (_timeScale > 1)
                            enemyHittedInstance.Pitch = 1f;
                        else
                            enemyHittedInstance.Pitch = _timeScale - 1;
                    }
                    return true;
                }
            }


            return false;
        }

        public override void Update(GameTime time)
        {
            foreach (var weapon in _weapons)
            {
                if (_player.Hitbox.Intersects(weapon.Hitbox))
                {
                    _player.CanGrabWeapon(weapon);
                    break;
                }
            }

            if (Input.KeyPressed(Keys.Tab, true))
            {
                TimeScale = 0.5f;
                _isActive = false;
                Main.SetScreenWithoutReCreating(Main.CurrentsScreens[1]);
            }


            _weapons.RemoveAll(weapon => weapon.PlayerHold);
            _enemies.ForEach(enemy => enemy.Update(time));
            _enemies.RemoveAll(enemy => !enemy.Alive);

            _player.Update(time);

        }

        public override void Draw()
        {
            spriteBatch.Begin(sortMode: SpriteSortMode.Immediate, transformMatrix: _camera.GetTransformationMatrix);
            {
                foreach (var weapon in _weapons)
                {
                    weapon.Draw(spriteBatch);
                }
                _enemies.ForEach(enemy => enemy.Draw(spriteBatch));
                _player.Draw(spriteBatch);
            }
            spriteBatch.End();
        }

        public override void Resume()
        {

            _isActive = true;
            Main.Instance.IsMouseVisible = false;
        }
    }
}