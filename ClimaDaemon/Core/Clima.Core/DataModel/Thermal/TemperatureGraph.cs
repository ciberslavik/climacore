using System;
using System.Collections.Generic;
using System.Linq;

namespace Clima.Core.DataModel.Graphs
{
    public class TemperatureGraph:IGraph<TemperatureGraphPiont>
    {
        private string _graphName;
        private List<TemperatureGraphPiont> _points;
        public event EventHandler<TemperatureGraphPiont> PointModified;
        public event EventHandler GraphModified;

        public TemperatureGraph()
        {
            _points = new List<TemperatureGraphPiont>();
        }
        public string GraphName
        {
            get => _graphName;
            set => _graphName = value;
        }


        public void AddPoint(TemperatureGraphPiont point)
        {
            _points.Add(point);
        }

        public void RemovePoint(TemperatureGraphPiont point)
        {
            throw new NotImplementedException();
        }

        public IList<TemperatureGraphPiont> Points => _points;

        
        protected virtual void OnPointModified(TemperatureGraphPiont e)
        {
            PointModified?.Invoke(this, e);
        }

        protected virtual void OnGraphModified()
        {
            GraphModified?.Invoke(this, EventArgs.Empty);
        }
    }
}