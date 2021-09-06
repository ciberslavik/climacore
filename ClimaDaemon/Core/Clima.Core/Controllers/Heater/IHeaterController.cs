namespace Clima.Core.Controllers.Heater
{
    public interface IHeaterController
    {
        public void Init(object config);
        public HeaterState State1 { get; }
        public HeaterState State2 { get; }
        public void SetHeater1State(HeaterState newState);
        public void SetHeater2State(HeaterState newState);
        public void Process(float setpoint);
    }
}