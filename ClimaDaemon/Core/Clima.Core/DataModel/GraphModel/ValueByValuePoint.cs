namespace Clima.Core.DataModel.GraphModel
{
    public class ValueByValuePoint : GraphPointBase
    {
        private int _pointIndex;
        private float _valueX;
        private float _valueY;

        public ValueByValuePoint()
        {
        }
        public ValueByValuePoint(float value_x = 0, float value_y = 0)
        {
            ValueX = value_x;
            ValueY = value_y;
        }

        public override int PointIndex
        {
            get => _pointIndex;
            set => _pointIndex = value;
        }

        public float ValueX
        {
            get => _valueX;
            set
            {
                _valueX = value;
                IsModified = true;
                OnModified();
            }
        }

        public float ValueY
        {
            get => _valueY;
            set
            {
                _valueY = value;
                IsModified = true;
                OnModified();
            }
        }
    }
}