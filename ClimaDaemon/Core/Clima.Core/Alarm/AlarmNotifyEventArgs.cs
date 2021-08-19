using System;

namespace Clima.Core.Alarm
{
    public delegate void AlarmNotifyHandler(object sender, AlarmNotifyEventArgs ea);
    public class AlarmNotifyEventArgs:EventArgs
    {
        public AlarmNotifyEventArgs()
        {
        }

        
    }
}