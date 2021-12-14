using System;
using Clima.Basics.Services;
using Clima.Core.DataModel.History;
using Clima.History.Service;
using System.Data.SQLite;

namespace Clima.History.SQLite
{
    public class SQLiteHistoryRepository:IHistoryRepository,IDisposable
    {
        private SQLiteConnection _connection;
        public SQLiteHistoryRepository(IFileSystem fs)
        {
            if(!fs.FileExist(fs.LocalDatabasePath))
                SQLiteConnection.CreateFile(fs.LocalDatabasePath);

            _connection = new SQLiteConnection($"Data Source={fs.LocalDatabasePath}; Version=3");
            _connection.Open();
            
        }
        public event RepoStateChangedEventHandler StateChanged;
        public void AddClimatePoint(ClimatStateHystoryItem point)
        {
            var cmd = _connection.CreateCommand();
            cmd.CommandText =
                "INSERT INTO ClimateHistory(ControllerID, PointDate, Front, Rear, Outdoor, Humidity, Pressure) " +
                "VALUES(@ControllerID, @pointdate, @front, @rear, @outdoor, @humidity, @pressure)";
            cmd.Parameters.AddWithValue("@ControllerID", 1);
            cmd.Parameters.AddWithValue("@front", point.FrontTemperature);
            cmd.Parameters.AddWithValue("@rear", point.RearTemperature);
            cmd.Parameters.AddWithValue("@outdoor", point.OutdoorTemperature);
            cmd.Parameters.AddWithValue("@humidity", point.Humidity);
            cmd.Parameters.AddWithValue("@pressure", point.Pressure);
            cmd.Parameters.AddWithValue("@pointdate", DateTime.Now);
            var tran = _connection.BeginTransaction();
            for (int i = 0; i < 1000000; i++)
            {
                cmd.ExecuteNonQuery();
            }
            tran.Commit();
            
            
        }

        public void Dispose()
        {
            _connection?.Dispose();
        }
    }
}