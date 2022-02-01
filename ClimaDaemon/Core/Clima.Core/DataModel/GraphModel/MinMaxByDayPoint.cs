namespace Clima.Core.DataModel.GraphModel
{
    public class MinMaxByDayPoint : GraphPointBase
    {
        private int _pointIndex;
        private int _day;
        private float _maxValue;
        private float _minValue;

        public MinMaxByDayPoint()
        {
        }

        public MinMaxByDayPoint(int day = 0, float min = 0, float max = 0)
        {
            Day = day;
            MinValue = min;
            MaxValue = max;
        }

        public override int PointIndex
        {
            get => _pointIndex;
            set => _pointIndex = value;
        }

        public int Day
        {
            get => _day;
            set
            {
                _day = value;
                IsModified = true;
                OnModified();
            }
        }

        public float MaxValue
        {
            get => _maxValue;
            set
            {
                _maxValue = value;
                IsModified = true;
                OnModified();
            }
        }

        public float MinValue
        {
            get => _minValue;
            set
            {
                _minValue = value; 
                IsModified = true;
                OnModified();
            }
        }
    }
}