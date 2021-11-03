using System;

namespace VersionIncrement
{
    [Serializable]
    public class VersionRecord
    {
        public int VersionMinor { get; set; }
        public int VersionMajor { get; set; }
        public long BuildNumber { get; set; }
    }
}