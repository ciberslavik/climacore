namespace Clima.Core.Devices.Configuration
{
    public class FanConfig
    {
        public string FanName { get; set; }
        public string RelayName { get; set; }
        public string FrequencyConverterName { get; set; }
        public FanType FanType { get; set; }
        public int FanId { get; set; }
        public int Performance { get; set; }
        public int FansCount { get; set; }
        public int FanPriority { get; set; }
        public bool Hermetise { get; set; }
        public bool Disabled { get; set; }
        public double StartPower { get; set; }
        public double StopPower { get; set; }

        public void LoadFromState(FanState state)
        {
            FanId = state.FanId;
            FanName = state.FanName;
            FanPriority = state.Priority;
            Performance = state.Performance;
            FansCount = state.FansCount;
            Hermetise = state.Hermetise;
            Disabled = state.Disabled;
            StartPower = state.StartValue;
            StopPower = state.StopValue;
        }

        public FanState CreateFanState()
        {
            var state = new FanState();
            state.FanId = FanId;
            state.FanName = FanName;
            state.Priority = FanPriority;
            state.Performance = Performance;
            state.FansCount = FansCount;
            state.Hermetise = Hermetise;
            state.Disabled = Disabled;
            state.StartValue = StartPower;
            state.StopValue = StopPower;
            return state;
        }
    }

    public enum FanType : int
    {
        Analog = 0,
        Discrete = 1
    }
}