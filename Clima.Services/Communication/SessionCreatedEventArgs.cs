using System;

namespace Clima.Services.Communication
{
    public delegate void SessionCreatedEventHandler(object sender, SessionCreatedEventArgs ea);
    public class SessionCreatedEventArgs:EventArgs
    {
        
    }
}