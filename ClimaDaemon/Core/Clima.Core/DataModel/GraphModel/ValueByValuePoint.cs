namespace Clima.Core.DataModel.GraphModel
{
    public class ValueByValuePoint : GraphPointBase
    {
        public ValueByValuePoint()
        {
        }


        public override int PointIndex { get; set; }
        public float ValueX { get; set; }
        public float ValueY { get; set; }
    }
}