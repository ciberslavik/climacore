using System;

namespace Clima.History.Service
{
    public delegate void RepoStateChangedEventHandler(object sender, RepoStateChangedEventArgs ea);
    public class RepoStateChangedEventArgs:EventArgs
    {
        
    }
}