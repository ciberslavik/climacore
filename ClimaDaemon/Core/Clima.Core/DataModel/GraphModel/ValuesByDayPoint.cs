namespace Clima.Core.DataModel.GraphModel
{
    public class ValueByDayPoint : GraphPointBase
    {
        private int _pointIndex;
        private int _day;
        private float _value;

        public ValueByDayPoint()
        {
            Day = 1;
            Value = 0;
        }

        public ValueByDayPoint(int day = 1, float value = 0)
        {
            Day = day;
            Value = value;
        }


        public override int PointIndex
        {
            get => _pointIndex;
            set
            {
                _pointIndex = value;
                IsModified = true;
                OnModified();
            }
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

        public float Value
        {
            get => _value;
            set
            {
                _value = value;
                IsModified = true;
                OnModified();
            }
        }
    }
}