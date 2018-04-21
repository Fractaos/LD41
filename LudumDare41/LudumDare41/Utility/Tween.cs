using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LudumDare41
{
    public class TweenPosition
    {
        public Entity Owner;
        public float Time;
        public Vector2 Begin;
        public Vector2 Change;
        public float Duration;
        public EaseFunction Function;

        public TweenPosition(Entity entity)
        {
            Owner = entity;
            Time = 0;
            Begin = Owner.Position;
            Change = Vector2.Zero;
            Duration = 0;
            Function = EaseFunction.Linear;
        }

        public void Update(float time, ref Vector2 value)
        {
            if (Time < Duration)
            {
                Time += time;
                value.X = Ease.Easing(this.Time, Begin.X, Change.X, Duration, Function);
                value.Y = Ease.Easing(this.Time, Begin.Y, Change.Y, Duration, Function);
            }
        }

        public void Move(Vector2 change, float duration, EaseFunction function = EaseFunction.Linear)
        {
            Begin = Owner.Position;
            Change = new Vector2(change.X - Owner.Position.X, change.Y - Owner.Position.Y);
            Duration = duration;
            Function = function;
            Time = 0;
        }
    }

    public class TweenValue
    {
        public float Value;
        public float Time;
        public float Begin;
        public float Change;
        public float Duration;
        public EaseFunction Function;

        public Action<float> Functor;

        public bool Used, ReadyToRemove;

        public TweenValue()
        {
            Time = 0;
            Duration = 0;
            Function = EaseFunction.Linear;
            Used = ReadyToRemove = false;
        }

        public void Update(float time)
        {
            if (Time < Duration)
            {
                Time += time;
                Value = Ease.Easing(Time, Begin, Change, Duration, Function);
                Functor(Value);
            }

            if (Used = true && Time >= Duration)
            {
                ReadyToRemove = true;
            }
        }

        public void Move(float value, float change, float duration, EaseFunction function, Action<float> functor)
        {
            Functor = functor;
            Begin = value;
            Change = change - Begin;
            Duration = duration;
            Function = function;
            Time = 0;
            Used = true;
        }
    }

    public enum EaseFunction
    {
        Linear,
        EaseInQuad,
        EaseOutQuad,
        EaseInOutQuad,
        EaseInCubic,
        EaseOutCubic,
        EaseInOutCubic,
        EaseInQuart,
        EaseOutQuart,
        EaseInOutQuart,
        EaseInQuint,
        EaseOutQuint,
        EaseInOutQuint,
        EaseInSine,
        EaseOutSine,
        EaseInOutSine,
        EaseInExpo,
        EaseOutExpo,
        EaseInOutExpo,
        EaseInCirc,
        EaseOutCirc,
        EaseInOutCirc
    }

    public static class Ease
    {
        //The TRIGGER
        public static float Easing(float t, float b, float c, float d, EaseFunction f)
        {
            switch (f)
            {
                case EaseFunction.Linear: return LinearTween(t, b, c, d);
                case EaseFunction.EaseInQuad: return EaseInQuad(t, b, c, d);
                case EaseFunction.EaseOutQuad: return EaseOutQuad(t, b, c, d);
                case EaseFunction.EaseInOutQuad: return EaseInOutQuad(t, b, c, d);
                case EaseFunction.EaseInCubic: return EaseInCubic(t, b, c, d);
                case EaseFunction.EaseOutCubic: return EaseOutCubic(t, b, c, d);
                case EaseFunction.EaseInOutCubic: return EaseInOutCubic(t, b, c, d);
                case EaseFunction.EaseInQuart: return EaseInQuart(t, b, c, d);
                case EaseFunction.EaseOutQuart: return EaseOutQuart(t, b, c, d);
                case EaseFunction.EaseInOutQuart: return EaseInOutQuart(t, b, c, d);
                case EaseFunction.EaseInQuint: return EaseInQuint(t, b, c, d);
                case EaseFunction.EaseOutQuint: return EaseOutQuint(t, b, c, d);
                case EaseFunction.EaseInOutQuint: return EaseInOutQuint(t, b, c, d);
                case EaseFunction.EaseInSine: return EaseInSine(t, b, c, d);
                case EaseFunction.EaseOutSine: return EaseOutSine(t, b, c, d);
                case EaseFunction.EaseInOutSine: return EaseInOutSine(t, b, c, d);
                case EaseFunction.EaseInExpo: return EaseInExpo(t, b, c, d);
                case EaseFunction.EaseOutExpo: return EaseOutExpo(t, b, c, d);
                case EaseFunction.EaseInOutExpo: return EaseInOutExpo(t, b, c, d);
                case EaseFunction.EaseInCirc: return EaseInCirc(t, b, c, d);
                case EaseFunction.EaseOutCirc: return EaseOutCirc(t, b, c, d);
                case EaseFunction.EaseInOutCirc: return EaseInOutCirc(t, b, c, d);
            }

            return 0;
        }

        #region AllEase
        public static float LinearTween(float t, float b, float c, float d)
        {
            return c * t / d + b;
        }

        public static float EaseInQuad(float t, float b, float c, float d)
        {
            t /= d;
            return c * t * t + b;
        }

        public static float EaseOutQuad(float t, float b, float c, float d)
        {
            t /= d;
            return -c * t * (t - 2) + b;
        }

        public static float EaseInOutQuad(float t, float b, float c, float d)
        {
            t /= d / 2;
            if (t < 1) return c / 2 * t * t + b;
            t--;
            return -c / 2 * (t * (t - 2) - 1) + b;
        }

        public static float EaseInCubic(float t, float b, float c, float d)
        {
            t /= d;
            return c * t * t * t + b;
        }

        public static float EaseOutCubic(float t, float b, float c, float d)
        {
            t /= d;
            t--;
            return c * (t * t * t + 1) + b;
        }

        public static float EaseInOutCubic(float t, float b, float c, float d)
        {
            t /= d / 2;
            if (t < 1) return c / 2 * t * t * t + b;
            t -= 2;
            return c / 2 * (t * t * t + 2) + b;
        }

        public static float EaseInQuart(float t, float b, float c, float d)
        {
            t /= d;
            return c * t * t * t * t + b;
        }

        public static float EaseOutQuart(float t, float b, float c, float d)
        {
            t /= d;
            t--;
            return -c * (t * t * t * t - 1) + b;
        }

        public static float EaseInOutQuart(float t, float b, float c, float d)
        {
            t /= d / 2;
            if (t < 1) return c / 2 * t * t * t * t + b;
            t -= 2;
            return -c / 2 * (t * t * t * t - 2) + b;
        }

        public static float EaseInQuint(float t, float b, float c, float d)
        {
            t /= d;
            return c * t * t * t * t * t + b;
        }

        public static float EaseOutQuint(float t, float b, float c, float d)
        {
            t /= d;
            t--;
            return c * (t * t * t * t * t + 1) + b;
        }

        public static float EaseInOutQuint(float t, float b, float c, float d)
        {
            t /= d / 2;
            if (t < 1) return c / 2 * t * t * t * t * t + b;
            t -= 2;
            return c / 2 * (t * t * t * t * t + 2) + b;
        }

        public static float EaseInSine(float t, float b, float c, float d)
        {
            return (float)(-c * Math.Cos(t / d * (Math.PI / 2)) + c + b);
        }

        public static float EaseOutSine(float t, float b, float c, float d)
        {
            return (float)(c * Math.Sin(t / d * (Math.PI / 2)) + b);
        }

        public static float EaseInOutSine(float t, float b, float c, float d)
        {
            return (float)(-c / 2 * (Math.Cos(Math.PI * t / d) - 1) + b);
        }

        public static float EaseInExpo(float t, float b, float c, float d)
        {
            return (float)(c * Math.Pow(2, 10 * (t / d - 1)) + b);
        }

        public static float EaseOutExpo(float t, float b, float c, float d)
        {
            return (float)(c * (-Math.Pow(2, -10 * t / d) + 1) + b);
        }

        public static float EaseInOutExpo(float t, float b, float c, float d)
        {
            t /= d / 2;
            if (t < 1) return (float)(c / 2 * Math.Pow(2, 10 * (t - 1)) + b);
            t--;
            return (float)(c / 2 * (-Math.Pow(2, -10 * t) + 2) + b);
        }

        public static float EaseInCirc(float t, float b, float c, float d)
        {
            t /= d;
            return (float)(-c * (Math.Sqrt(1 - t * t) - 1) + b);
        }

        public static float EaseOutCirc(float t, float b, float c, float d)
        {
            t /= d;
            t--;
            return (float)(c * Math.Sqrt(1 - t * t) + b);
        }

        public static float EaseInOutCirc(float t, float b, float c, float d)
        {
            t /= d / 2;
            if (t < 1) return (float)(-c / 2 * (Math.Sqrt(1 - t * t) - 1) + b);
            t -= 2;
            return (float)(c / 2 * (Math.Sqrt(1 - t * t) + 1) + b);
        }
        #endregion
    }
}
