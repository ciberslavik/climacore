using System;
using System.Collections.Generic;

namespace Clima.Core.DataModel.GraphModel
{
    public abstract class GraphBase<TPoint> where TPoint : GraphPointBase
    {
        protected List<TPoint> _points;
        private ProfileInfo _info = new ProfileInfo();
        protected bool _isModified = false;
        protected GraphBase()
        {
            _points = new List<TPoint>();
        }

        private event EventHandler? PointModified;
        public event EventHandler? GraphModified;

        public ProfileInfo Info
        {
            get => _info;
            set
            {
                if (!_info.Equals(value))
                {
                    _info = value;
                    OnGraphModified();
                }
            }
        }

        public List<TPoint> Points
        {
            get => _points;
            set
            {
                if (!_points.Equals(value))
                {
                    _points = value;
                    OnGraphModified();
                }
            }
        }

        public virtual bool IsModified
        {
            get => _isModified;
            set => _isModified = value;
        }

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
            IsModified = true;
            GraphModified?.Invoke(this, EventArgs.Empty);
        }

        private void PointOnModified(object? sender, EventArgs e)
        {
            _isModified = true;
            PointModified?.Invoke(sender, e);
        }
        
    }
}