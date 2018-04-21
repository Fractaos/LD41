using LudumDare41.Graphics;
using LudumDare41.Screens;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace LudumDare41.Utility
{
    public class Transition
    {
        public Texture2D Texture;
        public Color Color;
        public bool Action;
        public bool Change;
        public Screen ScreenToChange;

        int _alphaValue = 1;
        int _fadeIncrement = 3;
        double _fadeDelay = 0.35;
        int _cptFade = 0;

        public Transition()
        {
            Texture = Assets.PixelB;
            Color = Color.Transparent;
            Action = false;
        }

        public void Update(float time)
        {
            if (Action)
            {
                _fadeDelay -= time;

                if (_fadeDelay <= 0)
                {
                    _fadeDelay = 3;
                    _alphaValue += _fadeIncrement;

                    if (_alphaValue >= 255 || _alphaValue <= 0)
                    {
                        if (_alphaValue >= 255)
                            Change = true;
                        _fadeIncrement *= -1;
                        _cptFade++;
                    }
                }

                if (Change)
                {
                    Change = false;
                    Main.SetScreen(ScreenToChange);
                }

                if (_cptFade == 2)
                {
                    Action = false;
                    _cptFade = 0;
                }
            }
        }

        public void Fade(Screen newScreen)
        {
            ScreenToChange = newScreen;
            Action = true;
        }

        public void Draw(SpriteBatch batch)
        {
            batch.Draw(Texture, new Rectangle(0, 0, Utils.WIDTH, Utils.HEIGHT), new Color(0, 0, 0, MathHelper.Clamp(_alphaValue, 0, 255)));
        }
    }
}
