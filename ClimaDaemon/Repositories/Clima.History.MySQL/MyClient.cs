using System;
using System.Data;
using System.Threading.Tasks;
using Clima.Basics.Services;
using Clima.Core.DataModel.History;
using Clima.Core.Hystory;
using Clima.History.MySQL.Configurations;
using MySql.Data.MySqlClient;

namespace Clima.History.MySQL
{
    public class MyClient:IHistoryService
    {
        private readonly HistoryMySQLConfig _config;
        private MySqlConnection _conn;
        private ISystemLogger _log;
        public MyClient(HistoryMySQLConfig config)
        {
            _config = config;
            var sb = new MySqlConnectionStringBuilder()
            {
                Server = _config.ServerHost,
                Port = _config.ServerPort,
                UserID = _config.UserName,
                Password = _config.Password,
                Database = "ClimaDB",
                ConnectionTimeout = 2
            };
            sb.SslMode = MySqlSslMode.None;
            _conn = new MySqlConnection(sb.ConnectionString);
            
            //Check connection
            try
            {
                _conn.Open();
            }
            catch (Exception e)
            {
                Log.Error(e.ToString());
            }


            if (_conn.State == ConnectionState.Open)
            {
                _conn.Close();
            }
        }

        public ISystemLogger Log
        {
            get => _log;
            set => _log = value;
        }

        public void ConnectToServer()
        {
            MySqlConnection conn = null;
            var sb = new MySqlConnectionStringBuilder()
            {
                Server = "10.0.10.147",
                Port = 3306,
                UserID = "root",
                Password = "041087",
                Database = "ClimaDB",
                ConnectionTimeout = 2
            };

            sb.SslMode = MySqlSslMode.None;
            
            conn = new MySqlConnection(sb.ConnectionString);
            try
            {
                conn.Open();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return;
            }
            

            var cmd = conn.CreateCommand();
            cmd.CommandText = "SELECT * FROM AuthUsers";
            var reader = cmd.ExecuteReader(System.Data.CommandBehavior.CloseConnection);
            while (reader.Read())
            {
                Console.WriteLine("id={0}, value={1}", reader.GetInt32("idAuthUsers"), reader.GetString("UserName"));
            }
        }

        public void AddBootRecord()
        {
            var query = @"INSERT INTO ClimaDB.HistoryBootLog(ControllerID, BootTime)
                        VALUES(?ControllerID,?BootTime);";

            var cmd = new MySqlCommand(query, _conn);

            cmd.Parameters.AddWithValue("?ControllerID", _config.ControllerID);
            cmd.Parameters.AddWithValue("?BootTime", DateTime.Now);
            try
            {
                if(_conn.State != ConnectionState.Open)
                    _conn.Open();
                
                cmd.ExecuteNonQuery();
                _conn.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }
        public void AddClimatPoint(ClimatStateHystoryItem point)
        {
            var query = @"INSERT INTO ClimaDB.HistorySensors (
                FrontTemperature, RearTemperature, OutdoorTemperature, TemperatureSetPoint,
                VentilationSetPoint, Humidity, Pressure, ValveSetPoint, 
                MineSetPoint, PointDate, ControllerID, ValvePosition, MinePosition) 
            VALUES (?FrontTemperature, ?RearTemperature, ?OutdoorTemperature, ?TemperatureSetPoint, 
                ?VentilationSetPoint, ?Humidity, ?Pressure, ?ValveSetPoint, ?MineSetPoint, ?PointDate, ?ControllerID, ?ValvePosition, ?MinePosition);";
            
            var cmd = new MySqlCommand(query, _conn);
            cmd.Parameters.AddWithValue("?FrontTemperature", point.FrontTemperature);
            cmd.Parameters.AddWithValue("?RearTemperature", point.RearTemperature);
            cmd.Parameters.AddWithValue("?OutdoorTemperature", point.OutdoorTemperature);
            cmd.Parameters.AddWithValue("?TemperatureSetPoint", point.TemperatureSetPoint);
            cmd.Parameters.AddWithValue("?VentilationSetPoint", point.VentilationSetPoint);
            cmd.Parameters.AddWithValue("?Humidity", point.Humidity);
            cmd.Parameters.AddWithValue("?Pressure", point.Pressure);
            cmd.Parameters.AddWithValue("?ValveSetPoint", point.ValveSetPoint);
            cmd.Parameters.AddWithValue("?MineSetPoint", point.MineSetPoint);
            cmd.Parameters.AddWithValue("?PointDate", DateTime.Now);
            cmd.Parameters.AddWithValue("?ControllerID", _config.ControllerID);
            cmd.Parameters.AddWithValue("?ValvePosition", point.ValvePosition);
            cmd.Parameters.AddWithValue("?MinePosition", point.MinePosition);

            Task result = ExecuteInsertClimatePoint(_conn, cmd);
        }

        private async Task ExecuteInsertClimatePoint(MySqlConnection conn, MySqlCommand command)
        {
            if (conn.State != ConnectionState.Open)
            {
                try
                {
                    await conn.OpenAsync();
                }
                catch (Exception e)
                {
                    Log.Error(e.ToString());
                    return;
                }
            }

            try
            {
                await command.ExecuteNonQueryAsync();
            }
            catch (Exception e)
            {
                Log.Error(e.ToString());
            }
        }
    }
}