using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace LudumDare41.Graphics
{
    public class AnimatedSprite : Sprite
    {
        public int FrameWidth, FrameHeight, Frame, FrameNumber;
        public float AnimationTimer, Delay;
        public bool Loop, InAnim, BackTo;

        public Rectangle FrameBox
        {
            get { return new Rectangle(Frame * FrameWidth, 0, FrameWidth, FrameHeight); }
        }

        public new Rectangle Hitbox
        {
            get { return new Rectangle((int)Position.X, (int)Position.Y, FrameWidth, FrameHeight); }
        }

        public AnimatedSprite(Texture2D texture, Vector2 position, int frameWidth, int frameHeight, int frameNumber)
            : base(texture, position)
        {
            Frame = 0;
            FrameWidth = frameWidth;
            FrameHeight = frameHeight;
            FrameNumber = frameNumber;
        }

        public override void Update(float time)
        {
            if (InAnim)
            {
                AnimationTimer += time;
                if (AnimationTimer >= Delay)
                {
                    Anim();
                }
            }
        }

        private void Anim()
        {
            if (Frame == FrameNumber - 1)
            {
                if (Loop)
                    Frame = 0;
                else
                {
                    if (BackTo)
                        Frame = 0;
                    InAnim = false;
                }
            }
            else
                Frame++;

            AnimationTimer = 0;
        }

        public void Animate(bool loop, float delay, bool backto)
        {
            Loop = loop;
            Delay = delay;
            BackTo = backto;
            Anim();
            InAnim = true;
        }

        public override void Draw(SpriteBatch batch)
        {
            batch.Draw(Texture, Position, FrameBox, Color.White);
        }
    }
}
