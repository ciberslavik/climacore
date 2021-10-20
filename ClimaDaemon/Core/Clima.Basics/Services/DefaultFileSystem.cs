using System.IO;
using System.Reflection;
using System.Text;

namespace Clima.Basics.Services
{
    public class DefaultFileSystem : IFileSystem
    {
        public DefaultFileSystem()
        {
            _appBasePath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            _dataStoragePath = Path.Combine(_appBasePath, "Data");
            _configurationPath = Path.Combine(_dataStoragePath, "Config");

            var dbdir = Path.Combine(_dataStoragePath, "database");
            if (!Directory.Exists(dbdir))
                Directory.CreateDirectory(dbdir);
            
            LocalDatabasePath = Path.Combine(_dataStoragePath, "database", "local.db");
        }

        private readonly string _appBasePath;
        private readonly string _dataStoragePath;
        private string _configurationPath;

        public string AppBasePath => _appBasePath;

        public string DataStoragePath => _dataStoragePath;

        public string ConfigurationPath => _configurationPath;
        public string LocalDatabasePath { get; private set; }
        public bool FolderExist(string path)
        {
            return Directory.Exists(path);
        }


        public bool FileExist(string path)
        {
            return File.Exists(path);
        }

        public void CreateDirectory(string path)
        {
            Directory.CreateDirectory(path);
        }

        public void WriteTextFile(string filePath, string data)
        {
            File.WriteAllText(filePath, data, Encoding.UTF8);
        }

        public string ReadTextFile(string filePath)
        {
            return File.ReadAllText(filePath);
        }
    }
}