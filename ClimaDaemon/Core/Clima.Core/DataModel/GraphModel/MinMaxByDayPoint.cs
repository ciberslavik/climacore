namespace Clima.Core.DataModel.GraphModel
{
    public class MinMaxByDayPoint : GraphPointBase
    {
        public MinMaxByDayPoint()
        {
        }

        public MinMaxByDayPoint(int day = 0, float min = 0, float max = 0)
        {
            Day = day;
            MinValue = min;
            MaxValue = max;
        }

        public override int PointIndex { get; set; }

        public int Day { get; set; }
        public float MaxValue { get; set; }
        public float MinValue { get; set; }
    }
}