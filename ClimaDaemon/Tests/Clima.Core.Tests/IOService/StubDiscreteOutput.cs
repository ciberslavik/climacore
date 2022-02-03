using System;

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