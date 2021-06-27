using System;
using Clima.TcpServer.CoreServer;
using DataContract;
using NewtonsoftJsonSerializer;

namespace ConsoleServer
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Press esc key to stop");

            int i = 0;
            void PeriodicallyClearScreen()
            {
                i++;
                if (i > 15)
                {
                    Console.Clear();
                    Console.WriteLine("Press esc key to stop");
                    i = 0;
                }
            }

            //Write the host messages to the console
            /*void OnHostMessage(string input)
            {
                PeriodicallyClearScreen();
                Console.WriteLine(input);
            }*/

            ServerConfig config = new ServerConfig();

            var serverOpt = new ServerOption();
            serverOpt.Config = config;
            serverOpt.Host = "127.0.0.1";
            serverOpt.Port = 5911;
            
            IDataSerializer serializer = new NewtonsoftSerializer();
            var BLL = new Server(serverOpt);
            
            /*BLL.MessageReceived += new Server.MessageReceivedHandler(message =>
            {
                Console.WriteLine($"Message name:{message.Name}");
                Console.WriteLine($"    Data:{message.Data}");
            });   */
            BLL.StartServer();//Server runs in a dedicated thread seperate from mains thread

            while (Console.ReadKey().Key != ConsoleKey.Escape)
            {
                Console.Clear();
                Console.WriteLine("Press esc key to stop");
            }

            Console.WriteLine("Attempting clean exit");
            BLL.WaitStopServer();

            Console.WriteLine("Exiting console Main.");
        }
    }
}