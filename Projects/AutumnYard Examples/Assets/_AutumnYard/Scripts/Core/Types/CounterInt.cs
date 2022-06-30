
namespace AutumnYard.Tools
{
    /// <summary> Count an int, until a threshold is reached. </summary>
    public struct CounterInt
    {
        private readonly int _threshold;
        private int _value;

        public int Remaining => _value;
        public bool IsReady => _value <= 0;
        public override string ToString() => $"CounterInt: {IsReady} ({_value} of {_threshold})";

        public CounterInt(int threshold)
        {
            this._threshold = threshold;
            _value = threshold;
        }

        public CounterInt(int threshold, int value)
        {
            this._threshold = threshold;
            this._value = value;
        }

        public void Reset()
        {
            _value = _threshold;
        }

        public bool AdvanceOne()
        {
            if (_value > 0)
                _value--;

            return _value <= 0;
        }

    }
}
