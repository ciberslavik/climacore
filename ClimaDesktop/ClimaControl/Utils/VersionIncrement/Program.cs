using System;
using System.IO;
using System.Xml.Serialization;

namespace VersionIncrement
{
    class Program
    {
        static void Main(string[] args)
        {
            if (args.Length == 0)
            {
                Console.WriteLine("Invalid path to version file");
                return;
            }
            string verFile = args[0];

            if (!File.Exists(verFile))
            {
                XmlSerializer serializer = new XmlSerializer(typeof(VersionRecord));

                using (FileStream fs = new FileStream(verFile, FileMode.OpenOrCreate))
                {
                    serializer.Serialize(fs,new VersionRecord()
                    {
                        VersionMinor = 0,
                        VersionMajor = 0,
                        BuildNumber = 0
                    });
                }
            }

            VersionRecord currentVer;
            XmlSerializer ser = new XmlSerializer(typeof(VersionRecord));
            
            using (FileStream fs = new FileStream(verFile, FileMode.Open))
            {
                currentVer = ser.Deserialize(fs) as VersionRecord;
            }

            if (currentVer is not null)
            {
                currentVer.BuildNumber++;

                using (FileStream wr = new FileStream(verFile, FileMode.OpenOrCreate))
                {
                    ser.Serialize(wr, currentVer);
                }
            }
        }
    }
}
