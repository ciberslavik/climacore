namespace Clima.Core.Conrollers.Ventilation
{
    internal class FanControllerTableItem
    {
        internal FanControllerTableItem()
        {
        }

        internal float StartPerformance { get; set; }
        internal float StopPerformance { get; set; }
        internal float CurrentPerformance { get; set; }
        internal int Priority { get; set; }
        internal bool IsDead { get; set; }
        internal bool IsRunning { get; set; }
        internal bool IsHermetise { get; set; }
    }
}