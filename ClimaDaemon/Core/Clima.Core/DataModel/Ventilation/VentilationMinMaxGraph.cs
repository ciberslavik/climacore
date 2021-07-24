using System;
using System.Collections.Generic;

namespace Clima.Core.DataModel.Graphs
{
    public class VentilationMinMaxGraph:IGraph<VentilationMinMaxGraphPoint>
    {
        public event EventHandler<VentilationMinMaxGraphPoint> PointModified;
        public event EventHandler GraphModified;

        
        private List<VentilationMinMaxGraphPoint> _points;


        public VentilationMinMaxGraph()
        {
            _points = new List<VentilationMinMaxGraphPoint>();
        }
        public string GraphName { get; set; }
        public IList<VentilationMinMaxGraphPoint> Points => _points;
        public void AddPoint(VentilationMinMaxGraphPoint point)
        {
            _points.Add(point);
        }

        public void RemovePoint(VentilationMinMaxGraphPoint point)
        {
            throw new NotImplementedException();
        }
    }
}