namespace Clima.Basics
{
    public class MovingAverageFilter
    {
        private float _avgSumm = 0;
        private readonly float[] _values;
        private readonly int _width;
        private int _index = 0;
        /// <summary>
        /// Create new moving average filter
        /// </summary>
        /// <param name="width">number of count filter</param>
        public MovingAverageFilter(int width = 5)
        {
            _width = width;
            _values = new float[_width];
        }

        public float Calculate(float newValue)
        {
            // calculate the new sum
            _avgSumm = _avgSumm - _values[_index] + newValue;

            // overwrite the old value with the new one
            _values[_index] = newValue;

            // increment the index (wrapping back to 0)
            _index = (_index + 1) % _width;

            // calculate the average
            return _avgSumm / _width;
        }
    }
}