using System;
using System.Collections.Generic;

namespace Clima.Core.DataModel.GraphModel
{
    public abstract class GraphBase<TPoint> where TPoint : GraphPointBase
    {
        protected List<TPoint> _points;

        protected GraphBase()
        {
            _points = new List<TPoint>();
        }

        private event EventHandler PointModified;
        public event EventHandler GraphModified;

        public GraphInfo Info { get; set; } = new GraphInfo();
        public IList<TPoint> Points => _points;

        public virtual void AddPoint(TPoint point)
        {
            if (!_points.Contains(point))
            {
                _points.Add(point);
                point.Modified += PointOnModified;
                OnGraphModified();
            }
        }

        public virtual void RemovePoint(TPoint point)
        {
            if (_points.Contains(point))
            {
                point.Modified -= PointOnModified;
                _points.Remove(point);
                OnGraphModified();
            }
        }

        protected virtual void OnGraphModified()
        {
            GraphModified?.Invoke(this, EventArgs.Empty);
        }

        private void PointOnModified(object? sender, EventArgs e)
        {
            PointModified?.Invoke(sender, e);
        }
    }
}