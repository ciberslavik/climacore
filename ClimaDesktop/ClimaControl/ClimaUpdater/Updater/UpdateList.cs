using System;
using System.Collections.Generic;

namespace ClimaUpdater.Updater
{
    [Serializable]
    public class UpdateList
    {
        public List<string> Directories { get; set; } = new List<string>();
        public List<FileItem> Files { get; set; } = new List<FileItem>();
    }
}