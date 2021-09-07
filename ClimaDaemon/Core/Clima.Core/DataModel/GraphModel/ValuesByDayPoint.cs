namespace Clima.Core.DataModel.GraphModel
{
    public class ValueByDayPoint : GraphPointBase
    {
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


        public override int PointIndex { get; set; }

        public int Day { get; set; }
        public float Value { get; set; }
    }
}