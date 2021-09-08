namespace Clima.Core.DataModel
{
    public class LivestockState
    {
        public LivestockState()
        {
            
        }

        public int CurrentHeads { get; set; } = 0;
        public int CurrentDay { get; set; } = 0;
        public int TotalPlantedHeads { get; set; } = 0;
        public int TotalDeadHeads { get; set; } = 0;
        public int TotalKilledHeads { get; set; } = 0;
        public int TotalRefracted { get; set; } = 0;
    }
}