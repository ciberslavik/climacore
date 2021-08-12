using System;

namespace Clima.Core.DataModel.GraphModel
{
    public abstract class GraphPointBase
    {
        public abstract int PointIndex { get; set; }
        public event EventHandler Modified;
        public bool IsModified { get; set; }

        protected virtual bool Update<T>(ref T prop, T val)
        {
            if (Equals(prop, val))
                return false;

            prop = val;
            IsModified = true;
            OnModified();
            return true;
        }

        protected virtual void OnModified()
        {
            Modified?.Invoke(this, EventArgs.Empty);
        }
    }
}