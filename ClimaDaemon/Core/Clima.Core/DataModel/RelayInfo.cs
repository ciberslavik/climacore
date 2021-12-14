namespace Clima.Core.DataModel
{
    public class RelayInfo
    {
        public RelayInfo(string key = "")
        {
            Key = key;
            Name = "";
            CurrentState = false;
        }
        public string Key { get; set; }
        public string Name { get; set; }
        public bool CurrentState { get; set; }
    }
}