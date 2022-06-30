using System;

namespace AutumnYard.Tools
{
    [Serializable]
    /// <summary> Count an int, until a threshold is reached. Then, there's a chance to trigger. </summary>
    public struct CounterIntWithChance
    {
        private readonly int _threshold; // 2
        private readonly int _triggerChance; // 50
        private readonly int _thresholdForceTrigger; // 5
        private int _value;

        /// <summary>
        /// Accumulate on an integer. After threshold: trigger with triggerChance. Force trigger after thresholdForceTrigger.
        /// </summary>
        /// <param name="threshold">Threshold to begin the chance to trigger.</param>
        /// <param name="triggerChance">Chance to trigger after triggerMinThreshold is exceeded.</param>
        /// <param name="thresholdForceTrigger">Force trigger after this threshold is exceeded.</param>
        public CounterIntWithChance(int threshold, int triggerChance, int thresholdForceTrigger)
        {
            _threshold = threshold;
            _triggerChance = triggerChance;
            _thresholdForceTrigger = thresholdForceTrigger;
            _value = 0;
        }

        public void Reset()
        {
            _value = 0;
        }

        public bool AdvanceOne()
        {
            _value++;

            return Check();
        }

        private bool Check()
        {
            if (_value <= _threshold)
                return false;

            if (_value > _thresholdForceTrigger)
                return true;

            return UnityEngine.Random.Range(0, 100) <= _triggerChance;
        }

    }
}
