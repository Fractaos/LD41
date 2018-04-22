using LudumDare41.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace LudumDare41.Utility
{
    public class ProgressBar
    {

        #region Fields

        // Processing fields
        float _width, _height, _percentUp, _maxValue, _currentValue;
        bool _withLabel = false;


        // Graphics fields
        Color _color;
        Texture2D _barTexture, _barBackgroundTexture;
        Vector2 _position;
        SpriteFont _font = Assets.Font;

        #endregion



        #region Constructor(s)


        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="position">La position de la progress bar</param>
        /// <param name="width">La largeur</param>
        /// <param name="height">La hauteur</param>
        /// <param name="color">La couleur</param>
        /// <param name="maxValue">La valeur maximume possiblement atteinte</param>
        /// <param name="withLabel">Si la valeur est affichée au dessus de la progress bar sous ce format : valeur courante / valeur maximale</param>
        public ProgressBar(Vector2 position, float width, float height, Color color, float maxValue, bool withLabel)
        {
            _position = position;
            _width = width;
            _height = height;
            _color = color;
            _maxValue = maxValue;
            _withLabel = withLabel;
            _currentValue = maxValue;
            _percentUp = (_currentValue / _maxValue) * width;
            _barBackgroundTexture = Utils.CreateTexture((int)(width + 4), (int)(height + 4), color * 0.5f);
            _barTexture = Utils.CreateTexture((int)_percentUp, (int)height, color);

        }

        #endregion

        #region Properties

        public Vector2 Position
        {
            get => _position;
            set => _position = value;
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Décrémente la progressBar
        /// </summary>
        /// <param name="value">La valeur à décrémenter</param>
        public void DecreaseBar(int value)
        {
            _currentValue -= value;
            if (_currentValue <= 0)
            {
                _currentValue = 0;
            }
        }

        /// <summary>
        /// Incrémente la progressBar
        /// </summary>
        /// <param name="value">La valeur à incrémenter</param>
        public void IncreaseBar(int value)
        {
            _currentValue += value;
            if (_currentValue >= _maxValue)
            {
                _currentValue = _maxValue;
            }
        }

        public void Reset()
        {
            _currentValue = _maxValue;
        }

        public void Update(GameTime time, int value)
        {
            _currentValue = value;
            _percentUp = (_currentValue / _maxValue) * _width;
            if (_percentUp > 0)
            {
                _barTexture = Utils.CreateTexture((int)_percentUp, (int)_height, _color);
            }
            else
            {
                _barTexture = Utils.CreateTexture(1, (int)_height, _color);
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {

            if (_withLabel)
            {
                var text = _currentValue + "/" + _maxValue;
                spriteBatch.DrawString(_font, text, new Vector2(((_position.X + _barBackgroundTexture.Width / 2) - (_font.MeasureString(text) / 2).X), _position.Y - (_height + _font.MeasureString(text).Y) / 2), Color.White);
            }
            spriteBatch.Draw(_barBackgroundTexture, _position, null, Color.White, 0f, Vector2.Zero, 1, SpriteEffects.None, 1);
            if (_percentUp > 0)
            {
                spriteBatch.Draw(_barTexture, new Vector2(_position.X + 2, _position.Y + 2), null, Color.White, 0f, Vector2.Zero, 1, SpriteEffects.None, 1);
            }
        }

        public void Draw(SpriteBatch spriteBatch, Vector2 position)
        {

            if (_withLabel)
            {
                var text = _currentValue + "/" + _maxValue;
                spriteBatch.DrawString(_font, text, new Vector2(((position.X + _barBackgroundTexture.Width / 2) - (_font.MeasureString(text) / 2).X), position.Y - (_height + _font.MeasureString(text).Y) / 2), Color.White);
            }
            spriteBatch.Draw(_barBackgroundTexture, position, null, Color.White, 0f, Vector2.Zero, 1, SpriteEffects.None, 1);
            if (_percentUp > 0)
            {
                spriteBatch.Draw(_barTexture, new Vector2(position.X + 2, position.Y + 2), null, Color.White, 0f, Vector2.Zero, 1, SpriteEffects.None, 1);
            }
        }

        #endregion
    }
}
