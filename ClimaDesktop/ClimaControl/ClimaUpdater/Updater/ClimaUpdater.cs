using System;
using System.IO;
using System.Reflection;
using System.Windows;
using System.Xml.Serialization;

namespace ClimaUpdater.Updater
{
    public class ClimaUpdater
    {
        private string _configFile;
        private UpdaterConfig _config;

        public ClimaUpdater()
        {
            var localPath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            _configFile = Path.Combine(localPath, "config.xml");
        }

        public void Init()
        {
            XmlSerializer formatter = new XmlSerializer(typeof(UpdaterConfig));

            UpdaterConfig cfg = new UpdaterConfig();
            cfg.RemoteDirectory = @"d:\Projects\ClimaControl\ClimaDesktop\ClimaControl\ClimaLauncher\bin\Debug\net5.0-windows\";
            cfg.LocalDirectory = @"D:\Projects\UpdaterTest\";
            using (FileStream fs = new FileStream(_configFile, FileMode.OpenOrCreate))
            {
                formatter.Serialize(fs, cfg);
            }

            UpdateList lst = new UpdateList();
            _config = cfg;

            TraverseDirectory(ref lst, cfg.RemoteDirectory);

            CreateLocalDirectories(lst);

            CopyFromRemote(lst);
        }

        private void TraverseDirectory(ref UpdateList list, string directory)
        {
            var dir = new DirectoryInfo(directory);
            try
            {
                foreach (var f in dir.GetFiles())
                {
                    var srcFile = f.FullName.Replace(_config.RemoteDirectory, "",
                        StringComparison.InvariantCultureIgnoreCase);
                    list.Files.Add(new FileItem()
                    {
                        Source = srcFile,
                        Name = f.Name
                    });
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }

            foreach (var subDir in dir.GetDirectories())
            {
                var dirName = subDir.FullName.Replace(_config.RemoteDirectory, "",
                    StringComparison.InvariantCultureIgnoreCase);

                list.Directories.Add(dirName);
                TraverseDirectory(ref list, subDir.FullName);
            }
        }

        private void CreateLocalDirectories(UpdateList list)
        {
            foreach (var dirName in list.Directories)
            {
                var localDirPath = Path.Combine(_config.LocalDirectory, dirName);
                if (!Directory.Exists(localDirPath))
                {
                    Directory.CreateDirectory(localDirPath);
                }
            }
        }

        private void CopyFromRemote(UpdateList list)
        {
            foreach (var fileItem in list.Files)
            {
                var localFilePath = Path.Combine(_config.LocalDirectory, fileItem.Source);
                var remoteFilePath = Path.Combine(_config.RemoteDirectory, fileItem.Source);
                if(File.Exists(localFilePath))
                    File.Delete(localFilePath);

                File.Copy(remoteFilePath, localFilePath);
            }
        }
    }
}