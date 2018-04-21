using LudumDare41.Graphics;
using LudumDare41.Utility;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace LudumDare41.ShooterPhase
{
    public class Player : Sprite
    {

        private float _moveSpeed = .6f, _aimSpeed = 1, _shootSpeed = 1;

        private CrossAim _crossAim;

        public Player(Texture2D texture, Vector2 position) : base(texture, position)
        {
            _crossAim = new CrossAim(Assets.CrossAim, new Vector2((float)Utils.WIDTH/2, (float)Utils.HEIGHT/2), this);
        }

        public void Update(GameTime time)
        {
            float elapsedGameTimeMillis = time.ElapsedGameTime.Milliseconds;
            if (Input.KeyPressed(Keys.Z, false))
                Position.Y -= _moveSpeed * elapsedGameTimeMillis;
            if (Input.KeyPressed(Keys.S, false))
                Position.Y += _moveSpeed * elapsedGameTimeMillis;
            if (Input.KeyPressed(Keys.Q, false))
                Position.X -= _moveSpeed * elapsedGameTimeMillis;
            if (Input.KeyPressed(Keys.D, false))
                Position.X += _moveSpeed * elapsedGameTimeMillis;

            _crossAim.Update(time);
            base.Update(elapsedGameTimeMillis);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            _crossAim.Draw(spriteBatch);
            base.Draw(spriteBatch);
        }
    }
}