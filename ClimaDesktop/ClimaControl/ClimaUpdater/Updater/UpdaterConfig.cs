using System;

namespace ClimaUpdater.Updater
{
    [Serializable]
    public class UpdaterConfig
    {
        public UpdaterConfig()
        {

        }
        public string RemoteDirectory { get; set; }
        public string LocalDirectory { get; set; }


    }
}