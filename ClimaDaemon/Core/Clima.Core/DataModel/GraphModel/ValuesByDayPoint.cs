namespace Clima.Core.DataModel.GraphModel
{
    public class ValueByDayPoint:GraphPointBase
    {
        public ValueByDayPoint()
        {
            DayNumber = 1;
            Value = 0;
        }
        public ValueByDayPoint(int day = 1, float value = 0)
        {
            DayNumber = day;
            Value = value;
        }


        public override int PointIndex { get; set; }
        
        public int DayNumber { get; set; }
        public float Value { get; set; }
    }
}