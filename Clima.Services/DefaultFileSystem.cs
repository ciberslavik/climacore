using System.IO;
using System.Reflection;
using System.Text;

namespace Clima.Services
{
    public class DefaultFileSystem:IFileSystem
    {
        public DefaultFileSystem()
        {
            _appBasePath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            _dataStoragePath = Path.Combine(_appBasePath, "Data");
            _configurationPath = Path.Combine(_dataStoragePath, "Config");
        }
        private readonly string _appBasePath;
        private readonly string _dataStoragePath;
        private string _configurationPath;

        public string AppBasePath => _appBasePath;

        public string DataStoragePath => _dataStoragePath;

        public string ConfigurationPath => _configurationPath;

        public bool FolderExsist(string path)
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
    }
}