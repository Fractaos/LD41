using LudumDare41.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

namespace LudumDare41.Utility
{
    public class UiManager
    {
        List<Ui> UiList;
        public UiManager()
        {
            UiList = new List<Ui>();
        }

        public void Update(float time)
        {

            for (var i = 0; i < UiList.Count; i++)
            {
                UiList[i].Update(time);
            }
            UiList.RemoveAll(k => k.Ended == true);
        }

        public void Draw(SpriteBatch batch)
        {

            for (var i = 0; i < UiList.Count; i++)
            {
                UiList[i].Draw(batch);
            }
        }

        //GESTION PARTICLE
        public void AddParticle(Ui par)
        {
            UiList.Add(par);
        }
    }

    public class Ui
    {
        public Vector2 Position;
        public bool Ended = false;
        public float Timer;

        public Ui(Vector2 position)
        {
            this.Position = position;
        }

        public virtual void Update(float time)
        {
        }

        public virtual void Draw(SpriteBatch batch)
        {
            //batch.Draw(Texture, Position, Color.White);
        }

    }

    public class UiLabel : Ui
    {
        Color _color;
        float _delay;
        float _speed;
        string _text;
        Texture2D _texture;

        public UiLabel(string text, Vector2 position, float delay, float speed, Color color) : base(position)
        {
            this._color = color;
            this._delay = delay;
            this._speed = speed;
            this._text = text;
        }

        public UiLabel(Texture2D texture, Vector2 position, float delay, float speed) : base(position)
        {
            this._texture = texture;
            this._delay = delay;
            this._speed = speed;
        }

        public override void Update(float time)
        {
            Timer += time;
            if (Timer < _delay)
            {
                Position.Y -= _speed;
            }
            else
                Ended = true;
        }

        public override void Draw(SpriteBatch batch)
        {
            if (_text != null)
                batch.DrawString(Assets.Font, _text, Position, _color);
            else
                batch.Draw(_texture, Position, Color.White);
        }
    }

    public class UiButton : Ui
    {
        Texture2D _texture;
        Rectangle _bounds;
        Color _normalColor, _survoledColor, _actualColor;
        WhenPressed _action;

        public UiButton(Vector2 position, int width, int height, Color normalColor, Color survoledColor, WhenPressed action, Texture2D texture = null) : base(position)
        {
            _bounds = new Rectangle((int)position.X, (int)position.Y, width, height);
            this._normalColor = normalColor;
            this._survoledColor = survoledColor;
            this._action = action;
            if (texture != null)
                this._texture = texture;
            else
                 _texture = Utils.CreateTexture(width, height, normalColor);
        }

        public UiButton(Vector2 position, WhenPressed action, Texture2D texture) : base(position)
        {
            _action = action;
            _texture = texture;
            _bounds = new Rectangle((int)position.X, (int)position.Y, _texture.Width, _texture.Height);

        }

        public UiButton(Vector2 position, int width, int height, WhenPressed action, Color color) : base(position)
        {
            _texture = Utils.CreateTexture(width, height, color);
            _bounds = new Rectangle((int)position.X, (int)position.Y, width, height);
            this._action = action;
        }

        public override void Update(float time)
        {
            if (Input.MouseBox.Intersects(_bounds))
            {
                _actualColor = _survoledColor;
                if (Input.Left(true))
                {
                    _action.Invoke();
                }
            }
            else
            {
                _actualColor = _normalColor;
            }
        }

        public override void Draw(SpriteBatch batch)
        {
            if (_texture != null)
                batch.Draw(_texture, Position, Color.White);
            else
                batch.Draw(Assets.PixelW, _bounds, _actualColor);

        }

        #region codetest
        //for (int i = 0; i<Width; i++)
        //    {
        //        for (int j = 0; j<Heigth; j++)
        //        {
        //            if(i==0)
        //            {
        //                if(j==0)
        //                    Batch.Draw(actualDic["topleft"], new Vector2(i* 32, j* 32), Color.White);
        //                else if(j== Heigth - 1)
        //                    Batch.Draw(actualDic["bottomleft"], new Vector2(i* 32, j* 32), Color.White);
        //                else
        //                    Batch.Draw(actualDic["left"], new Vector2(i* 32, j* 32), Color.White);
        //            }
        //            else if (i == Width-1)
        //            {
        //                if (j == 0)
        //                    Batch.Draw(actualDic["topright"], new Vector2(i* 32, j* 32), Color.White);
        //                else if (j == Heigth - 1)
        //                    Batch.Draw(actualDic["bottomright"], new Vector2(i* 32, j* 32), Color.White);
        //                else
        //                    Batch.Draw(actualDic["right"], new Vector2(i* 32, j* 32), Color.White);
        //            }
        //            else if (j == 0)
        //            {
        //                    Batch.Draw(actualDic["top"], new Vector2(i* 32, j* 32), Color.White);
        //            }
        //            else if (j == Heigth - 1)
        //            {

        //                    Batch.Draw(actualDic["bottom"], new Vector2(i* 32, j* 32), Color.White);
        //            }
        //            else
        //                Batch.Draw(actualDic["middle"], new Vector2(i* 32, j* 32), Color.White);



        //        }
        //    }
        #endregion

        public delegate void WhenPressed();
    }

    public class UiPopup : Ui
    {
        public enum PopState { Open, Wait, Close };
        PopState _state;
        Color _actualColor, _textColor;
        Rectangle _bounds;
        string _text;
        int _maxHeight;

        public int Height { get; }
        public string Text { get; }

        public UiPopup(Vector2 position, int width, int height, string text, Color color, Color textColor) : base(position)
        {
            _actualColor = color;
            _maxHeight = height;
            var prout = (int)((position.Y + (position.Y + height)) / 2);
            _bounds = new Rectangle((int)position.X, prout, width, 0);
            Height = height;
            Text = text;
            this._text = text;
            this._textColor = textColor;
            _state = PopState.Open;
        }

        public override void Update(float time)
        {
            switch (_state)
            {
                case PopState.Open:
                    _bounds.Y--;
                    _bounds.Height += 2;

                    if (_bounds.Height >= _maxHeight)
                        _state = PopState.Wait;
                    break;
                case PopState.Wait:
                    Timer += time;
                    if (Timer >= 2500)
                    {
                        _state = PopState.Close;
                    }
                    break;
                case PopState.Close:
                    _bounds.Y++;
                    _bounds.Height -= 2;
                    if (_bounds.Height < 0)
                        Ended = true;
                    break;
            }

        }

        public override void Draw(SpriteBatch batch)
        {
            batch.Draw(Assets.PixelW, _bounds, _actualColor);
            if (_state == PopState.Wait)
                batch.DrawString(Assets.Font, _text, new Vector2(_bounds.X, _bounds.Y) + new Vector2((_bounds.Width - Assets.Font.MeasureString(_text).X) / 2, (_bounds.Height - Assets.Font.MeasureString(_text).Y) / 2), _textColor);

        }
    }
}
