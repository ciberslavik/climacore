using System;
using Clima.TcpServer;

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
            void OnHostMessage(string input)
            {
                PeriodicallyClearScreen();
                Console.WriteLine(input);
            }

            var BLL = new Host(OnHostMessage);
            BLL.RunServerThread(); //Server runs in a dedicated thread seperate from mains thread

            while (Console.ReadKey().Key != ConsoleKey.Escape)
            {
                Console.Clear();
                Console.WriteLine("Press esc key to stop");
            }

            Console.WriteLine("Attempting clean exit");
            BLL.WaitForServerThreadToStop();

            Console.WriteLine("Exiting console Main.");
        }
    }
}