using Microsoft.Xna.Framework;

namespace LudumDare41.ShooterPhase
{
    public class Camera
    {
        private Vector2 _position; // DEFAULT = (0, 0)
        private float _zoom; // NORMAL = 1F
        private float _angle; // IN RADIANS

        public Camera()
        {
            _position = Vector2.Zero;
            _zoom = 1f;
            _angle = 0f;
        }

        public Vector2 Position
        {
            get => _position;
            set => _position = value;
        }

        public float Zoom
        {
            get => _zoom;
            set => _zoom = value;
        }

        public float Angle
        {
            get => _angle;
            set => _angle = value;
        }

        public Vector2 ScreenToWorld(Vector2 sPos)
        {
            return Vector2.Transform(sPos, Matrix.Invert(GetTransformationMatrix));
        }

        public Matrix GetTransformationMatrix
        {
            get
            {
                return Matrix.CreateTranslation(-Position.X, -Position.Y, 0) * Matrix.CreateScale(Zoom, Zoom, 1f) * Matrix.CreateRotationZ(Angle);
            }
        }

    }
}