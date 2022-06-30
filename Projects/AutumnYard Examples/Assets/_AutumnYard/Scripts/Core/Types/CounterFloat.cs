
using System;

namespace AutumnYard.Tools
{
    /// <summary>
    /// Count an float, until a threshold is reached. Is ticked with a delta.
    /// </summary>
    public struct CounterFloat
    {
        private readonly float _threshold;
        private float _value;
        private event Action _onFinish;

        public bool IsReady => _value <= 0f;
        public float Normalized => _value / _threshold;

        public string AsString => $"CounterFloat: {IsReady} ({_value} of {_threshold})";


        public static implicit operator float(CounterFloat reference) => reference._value;
        public static implicit operator string(CounterFloat reference) => reference.ToString();
        public override string ToString() => _value.ToString();
        public string ToString(string format) => _value.ToString(format);


        public CounterFloat(float threshold)
        {
            _threshold = threshold;
            _value = threshold;
            _onFinish = null;
        }

        public CounterFloat(float threshold, Action action)
        {
            this._threshold = threshold;
            _value = threshold;
            _onFinish = action;
        }

        public void Reset()
        {
            _value = _threshold;
        }

        public bool Tick(float delta)
        {
            if (_value > 0)
                _value -= delta;

            if (_value <= 0)
            {
                _value = 0f;
                _onFinish?.Invoke();
                return true;
            }

            return false;
        }

    }
}
