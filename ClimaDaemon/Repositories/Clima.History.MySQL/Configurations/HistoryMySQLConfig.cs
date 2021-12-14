using Clima.Basics.Configuration;

namespace Clima.History.MySQL.Configurations
{
    public class HistoryMySQLConfig:IConfigurationItem
    {
        private string _configurationName;

        public string ServerHost { get; set; }
        public uint ServerPort { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }

        public int ControllerID { get; set; }
        public static HistoryMySQLConfig CreateDefault()
        {
            return new HistoryMySQLConfig()
            {
                ServerHost = "localhost",
                ServerPort = 3306,
                UserName = "root",
                Password = "123"
            };
        }
        public string ConfigurationName => _configurationName;
    }
}