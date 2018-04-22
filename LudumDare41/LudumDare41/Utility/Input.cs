using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace LudumDare41.Utility
{
    class Input
    {
        private static KeyboardState _oldK, _currentK;
        private static MouseState _oldM, _currentM;

        public static Rectangle MouseBox;
        public static Vector2 MousePos;

        public static void Update()
        {
            _oldK = _currentK;
            _oldM = _currentM;

            _currentK = Keyboard.GetState();
            _currentM = Mouse.GetState();

            MouseBox = new Rectangle(_currentM.X, _currentM.Y, 1, 1);
            MousePos = new Vector2(_currentM.X, _currentM.Y);
        }

        public static MouseState MouseState
        {
            get => _currentM;
        }

        public static MouseState OldMouseState
        {
            get => _oldM;
        }

        public static bool KeyPressed(Keys k, bool u)
        {
            return u ? (_oldK[k] == KeyState.Up && _currentK[k] == KeyState.Down) : (_currentK[k] == KeyState.Down);
        }

        public static bool Left(bool u)
        {
            return u ? (_oldM.LeftButton == ButtonState.Released && _currentM.LeftButton == ButtonState.Pressed) : (_currentM.LeftButton == ButtonState.Pressed);
        }

        public static bool Right(bool u)
        {
            return u ? (_oldM.RightButton == ButtonState.Released && _currentM.RightButton == ButtonState.Pressed) : (_currentM.RightButton == ButtonState.Pressed);
        }

        public static bool Middle(bool u)
        {
            return u ? (_oldM.MiddleButton == ButtonState.Released && _currentM.MiddleButton == ButtonState.Pressed) : (_currentM.MiddleButton == ButtonState.Pressed);
        }

        public static bool MouseOn(Rectangle source)
        {
            return source.Contains(MousePos);
        }
    }
}
