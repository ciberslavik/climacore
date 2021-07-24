using System;
using System.Collections;
using System.Collections.Generic;

namespace Clima.Core.DataModel.Graphs
{
    public interface IGraph<TPoint> where TPoint : GraphPointBase
    {
        event EventHandler<TPoint> PointModified;
        event EventHandler GraphModified;
        string GraphName { get; set; }
        IList<TPoint> Points { get; }
        void AddPoint(TPoint point);
        void RemovePoint(TPoint point);
    }
}