namespace Clima.Core.DataModel.GraphModel
{
    public class MinMaxByDayPoint : GraphPointBase
    {
        public MinMaxByDayPoint()
        {
        }


        public override int PointIndex { get; set; }

        public int DayNumber { get; set; }
        public float MaxValue { get; set; }
        public float MinValue { get; set; }
    }
}