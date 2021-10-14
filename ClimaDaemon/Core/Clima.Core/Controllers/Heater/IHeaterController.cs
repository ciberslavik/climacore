using System.Collections.Generic;
using Clima.Basics.Services;
using Clima.Core.DataModel;

namespace Clima.Core.Controllers.Heater
{
    public interface IHeaterController:IService
    {
        public HeaterState GetHeaterState(string key);
        public Dictionary<string, HeaterState> States { get; }
        public Dictionary<string, HeaterParams> Params { get; }
        public float SetPoint { get; }
        public void UpdateHeaterState(string key, HeaterState newState);
        public List<HeaterParams> UpdateHeaterParams(List<HeaterParams> heaterParams);
        
        public void StopHeater();
        public void Process(float setpoint);
    }
}