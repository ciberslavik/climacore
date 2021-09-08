using System.Collections.Generic;
using Clima.Basics.Services;
using Clima.Core.DataModel;

namespace Clima.Core.Controllers.Heater
{
    public interface IHeaterController:IService
    {
        public HeaterState GetHeaterState(string key);
        public Dictionary<string, HeaterState> States { get; }
        public void SetHeaterState(HeaterState newState);
        public HeaterInfo UpdateHeaterInfo(HeaterInfo info);
        public void Process(float setpoint);
    }
}