using System;
using Avalonia.Threading;
using Clima.Basics;
using Clima.Core.IO;

namespace Clima.Core.Tests.IOService
{
    public class StubDiscreteOutput :ObservableObject, IDiscreteOutput
    {
        private bool _state;
        public PinType PinType => PinType.Discrete;
        public PinDir Direction => PinDir.Output;
        public string Description { get; set; }
        public string PinName { get; set; }
        public bool IsModified { get; }
        public event DiscretePinStateChangedEventHandler PinStateChanged;

        public bool State
        {
            get => _state;
            private set => this.Update(ref _state, value);
        }

        protected override bool Update<T>(ref T prop, T value, string propertyName = null)
        {
            if (!Equals(prop, value))
            {
                prop = value;
                if(Dispatcher.UIThread.CheckAccess())
                    OnPropertyChanged(propertyName);
                else
                {
                    Dispatcher.UIThread.InvokeAsync(() => { OnPropertyChanged(propertyName);});
                }
                return true;
            }
            return false;
        }

        public void SetState(bool state, bool queued = true)
        {
            State = state;
            ClimaContext.Logger.System($"Pin:{PinName} to {state}");
            /*if (MonitorPin is not null)
            {
                MonitorPin.SetState(state);
            }*/
        }
    }
}