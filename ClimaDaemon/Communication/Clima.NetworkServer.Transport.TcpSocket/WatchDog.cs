using System;
using System.Diagnostics;
using System.Timers;

namespace Clima.NetworkServer.Transport.TcpSocket
{
    public class WatchDog
    {
        static object locker = new object();
         long lastPacketReceived;
         Stopwatch stopWatch = new Stopwatch();
         long threshold = 5000;
        public WatchDog()
        {
            Timer watchDogTimer = new Timer(1000);
            watchDogTimer.Elapsed += new ElapsedEventHandler(watchDogTimer_Elapsed);
            watchDogTimer.Start();
            stopWatch.Start();
        }

        public event EventHandler Alarm;
        private void watchDogTimer_Elapsed(object sender, ElapsedEventArgs e)
        {
            lock (locker)
            {
                if ((stopWatch.ElapsedMilliseconds - lastPacketReceived) > threshold)
                {
                    OnAlarm();
                }
            }
        }

        public void Reset()
        {
            lock (locker)
            {
                lastPacketReceived = stopWatch.ElapsedMilliseconds;
            }
        }


        protected virtual void OnAlarm()
        {
            Alarm?.Invoke(this, EventArgs.Empty);
        }
    }
}