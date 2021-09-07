namespace Clima.Core.DataModel.GraphModel
{
    public class ValueByValuePoint : GraphPointBase
    {
        public ValueByValuePoint()
        {
        }
        public ValueByValuePoint(float value_x = 0, float value_y = 0)
        {
            ValueX = value_x;
            ValueY = value_y;
        }

        public override int PointIndex { get; set; }
        public float ValueX { get; set; }
        public float ValueY { get; set; }
    }
}