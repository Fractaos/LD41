using System.Collections.Generic;

namespace LudumDare41
{

    public static class TimerManager
    {
        public static List<Timer> Timers = new List<Timer>();

        public static void Update(float time)
        {
            for (var i = 0; i < Timers.Count; i++)
            {
                Timers[i].Update(time);
            }
            Timers.RemoveAll(k => k.Ended == true);
        }
    }

    public class Timer
    {
        OnEnd _action;
        float _time, _delay;
        bool _counted;
        int _count;

        public bool Ended;


        public Timer(float delay, OnEnd action, int count = 0)
        {
            this._delay = delay;
            this._action = action;
            if (count != 0)
            {
                _counted = true;
                this._count = count;
            }
            else
                _counted = false;

            Ended = false;
        }


        public void Update(float time)
        {
            this._time += time;
            if (this._time >= _delay)
            {
                _action.Invoke();
                this._time = 0;
                //if (!Counted)
                //    Ended = true;
                if (_counted)
                {
                    if (_count > 0)
                        _count--;
                    else
                        Ended = true;
                }
            }
        }

        public delegate void OnEnd();
    }
}
