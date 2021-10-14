using System;
using MySql.Data.MySqlClient;

namespace Clima.History.MySQL
{
    public class MyClient
    {
        public void ConnectToServer()
        {
            MySqlConnection conn = null;
            var sb = new MySqlConnectionStringBuilder()
            {
                Server = "127.0.0.1",
                Port = 3306,
                UserID = "root",
                Password = "041087",
                Database = "ClimaDB"
            };

            conn = new MySqlConnection(sb.ConnectionString);
            conn.Open();

            var cmd = conn.CreateCommand();
            cmd.CommandText = "SELECT * FROM AuthUsers";
            var reader = cmd.ExecuteReader(System.Data.CommandBehavior.CloseConnection);
            while (reader.Read())
            {
                Console.WriteLine("id={0}, value={1}", reader.GetInt32("idAuthUsers"), reader.GetString("UserName"));
            }
        }
    }
}