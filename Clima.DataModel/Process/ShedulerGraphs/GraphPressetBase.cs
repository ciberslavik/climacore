using System.Collections.Generic;

namespace Clima.DataModel.Process.ShedulerGraphs
{
    public abstract class GraphPressetBase
    {
        public delegate void PressetModifiedHandler(PressetModifiedEventArgs ea);

        public event PressetModifiedHandler PressetModified;
        private string _name;

        protected GraphPressetBase()
        {
        }
        public virtual int GraphPressetId { get; set; }

        public virtual string Name
        {
            get => _name;
            set => _name = value;
        }

        protected virtual void OnPressetModified(PressetModifiedEventArgs ea)
        {
            PressetModified?.Invoke(ea);
        }
    }
}