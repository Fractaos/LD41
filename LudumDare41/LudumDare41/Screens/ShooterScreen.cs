using LudumDare41.Graphics;
using LudumDare41.ShooterPhase;
using LudumDare41.Utility;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;

namespace LudumDare41.Screens
{
    public class ShooterScreen : Screen
    {
        private float _timeScale = 1f;
        private bool _isActive;

        private float _timeElapsedSinceOnScreen;

        private UiManager _uiManager;

        private delegate void ReplayDelegate();

        private delegate void QuitDelegate();

        private Player _player;
        private List<Weapon> _weapons;
        private List<Enemy> _enemies;
        private List<Loot> _loots;
        private Camera _camera;

        private bool _gameOver;

        //ARENE
        Rectangle AreneBounds;
        Texture2D AreneTexture;


        public override void Create()
        {
            AreneTexture = Main.Content.Load<Texture2D>("Assets/Graphics/ShooterPhase/arene");
            AreneBounds = new Rectangle(38, 38, 1520, 820);
            _gameOver = false;
            _camera = new Camera();
            _weapons = new List<Weapon>();
            _enemies = new List<Enemy>();
            _uiManager = new UiManager();
            ReplayDelegate replay = Replay;
            QuitDelegate quit = Quit;
            _uiManager.AddParticle(new UiButton(_camera.ScreenToWorld(new Vector2(Utils.WIDTH / 2 - 250, .7f * Utils.HEIGHT)), Replay, Assets.ReplayButton));
            _uiManager.AddParticle(new UiButton(_camera.ScreenToWorld(new Vector2(Utils.WIDTH / 2 + 50, .7f * Utils.HEIGHT)), Quit, Assets.QuitButton));
            _loots = new List<Loot>();

            Gun gun = new Gun(new Vector2(100, 100), 150, WeaponState.OnFloor, _camera);
            SubMachine subMachine = new SubMachine(new Vector2(650, 700), 150, WeaponState.OnFloor, _camera);
            Sniper sniper = new Sniper(new Vector2(400, 300), 150, WeaponState.OnFloor, _camera);
            _weapons.Add(gun);
            _weapons.Add(subMachine);
            _weapons.Add(sniper);
            _player = new Player(Utils.CreateTexture(50, 50, Color.Blue),
                new Vector2(Utils.WIDTH / 2 - 25, Utils.HEIGHT / 2 - 25), _camera);
            _loots.Add(new Loot(new Vector2(50, 50), LootType.Sucre));

            for (int i = 0; i < 1; i++)
            {
                _enemies.Add(new Enemy(Utils.CreateTexture(50, 50, Color.Red),
                    new Vector2(Utils.RANDOM.Next(100, Utils.WIDTH - 200), Utils.RANDOM.Next(100, Utils.HEIGHT - 200)), _player, _camera));
            }

            Assets.MusicShooter.Volume = 0.5f;
            Assets.MusicShooter.IsLooped = true;
            Assets.MusicShooter.Play();




        }

        private void Quit()
        {
            Main.Instance.Exit();
        }

        private void Replay()
        {
            Main.CurrentsScreens.ForEach(screen => screen.Create());
        }

        public Player Player
        {
            get { return _player; }
        }

        public float TimeScale
        {
            get => _timeScale;
            set => _timeScale = value;
        }
        public Player GetPlayer
        {
            get { return _player; }
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
                if (_isActive && Utils.PlayerHitted != null)
                {
                    if (_timeScale > 1)
                        Utils.PlayerHitted.Pitch = 1f;
                    else
                        Utils.PlayerHitted.Pitch = _timeScale - 1;

                    Utils.PlayerHitted.Play();
                }

                return true;
            }

            foreach (var enemy in _enemies)
            {
                if (bullet.Hitbox.Intersects(enemy.Hitbox) && bullet.Side is Player thePlayer)
                {
                    bullet.ToDestroy = true;
                    enemy.TakeDamage(bullet.FromWeapon.Damage * thePlayer.Accuracy);
                    if (_isActive && Utils.EnemyHitted != null)
                    {
                        if (_timeScale > 1)
                            Utils.EnemyHitted.Pitch = 1f;
                        else
                            Utils.EnemyHitted.Pitch = _timeScale - 1;

                        Utils.EnemyHitted.Play();
                    }
                    return true;
                }
            }


            return false;
        }

        public void CreateLootAtPosition(Vector2 position, LootType type)
        {
            _loots.Add(new Loot(position, type));
        }

        public override void Update(GameTime time)
        {
            if (!_gameOver)
            {
                foreach (var weapon in _weapons)
                {
                    if (_player.Hitbox.Intersects(weapon.Hitbox))
                    {
                        _player.CanGrabWeapon(weapon);
                        break;
                    }
                }

                if (_isActive && _timeElapsedSinceOnScreen <= Utils.TIME_ON_SCREEN)
                    _timeElapsedSinceOnScreen += time.ElapsedGameTime.Milliseconds;
                if (_timeElapsedSinceOnScreen > Utils.TIME_ON_SCREEN)
                {
                    if (Input.KeyPressed(Keys.Tab, true))
                    {
                        TimeScale = 0.1f;
                        _isActive = false;
                        Assets.MusicShooter.Volume = 0;
                        _timeElapsedSinceOnScreen = 0;
                        Main.SetScreenWithoutReCreating(Main.CurrentsScreens[1]);
                    }
                }


                _weapons.RemoveAll(weapon => weapon.PlayerHold);
                _enemies.ForEach(enemy => enemy.Update(time));
                _enemies.RemoveAll(enemy => !enemy.Alive);
                _loots.ForEach(loot => loot.Update());
                _loots.RemoveAll(loot => loot.ToRemove);

                _player.Update(time);
            }
            else
            {
                _uiManager.Update(time.ElapsedGameTime.Milliseconds);
            }

            if (!_player.Alive)
                _gameOver = true;

        }

        public override void Draw()
        {
            spriteBatch.Begin(sortMode: SpriteSortMode.Deferred, transformMatrix: _camera.GetTransformationMatrix);
            {
                spriteBatch.Draw(AreneTexture, Vector2.Zero, Color.White);
                foreach (var weapon in _weapons)
                {
                    weapon.Draw(spriteBatch);
                }
                _enemies.ForEach(enemy => enemy.Draw(spriteBatch));
                _loots.ForEach(loot => loot.Draw(spriteBatch));
                _player.Draw(spriteBatch);
                if (_gameOver)
                {
                    spriteBatch.Draw(Assets.GameOver, _camera.ScreenToWorld(Vector2.Zero), Color.White);
                    _uiManager.Draw(spriteBatch, _camera);
                    Main.Instance.IsMouseVisible = true;

                }

            }
            spriteBatch.End();
        }

        public override void Resume()
        {
            TimeScale = 1f;
            _isActive = true;
            Assets.MusicShooter.Volume = 0.5f;
            Main.Instance.IsMouseVisible = false;
        }
    }
}