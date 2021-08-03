namespace Clima.Core.DataModel.GraphModel
{
    public class ValueByDayPoint:GraphPointBase
    {
        public ValueByDayPoint()
        {
        }

        
        public override int PointIndex { get; set; }
        
        public int DayNumber { get; set; }
        public float Value { get; set; }
    }
}