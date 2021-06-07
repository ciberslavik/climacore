using System.Diagnostics;
using System.IO;
using System.Reflection;

namespace Clima.Services.Configuration
{
    public class DefaultConfigurationStorage:IConfigurationStorage
    {
        private readonly IConfigurationSerializer _serializer;
        private readonly IFileSystem _fs;

        public DefaultConfigurationStorage(IConfigurationSerializer serializer, IFileSystem fs)
        {
            _serializer = serializer;
            _fs = fs;

            if (!_fs.FolderExsist(_fs.ConfigurationPath))
            {
                _fs.CreateDirectory(_fs.ConfigurationPath);
            }
        }


        public void RegisterConfig<ConfigT>() where ConfigT : ConfigItemBase, new()
        {
            var defConfig = new ConfigT();

            RegisterConfig<ConfigT>(defConfig);
        }

        public void RegisterConfig<ConfigT>(string name) where ConfigT : ConfigItemBase, new()
        {
            throw new System.NotImplementedException();
        }

        public void RegisterConfig<ConfigT>(ConfigT instance) where ConfigT : ConfigItemBase, new()
        {
            string fileName = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) 
                              + _serializer.DataExtension;
            if (instance != null)
            {
                string data = _serializer.Serialize(instance);
            }
        }

        public void RegisterConfig<ConfigT>(string name, ConfigT instance) where ConfigT : ConfigItemBase, new()
        {
            throw new System.NotImplementedException();
        }

        public ConfigT GetConfig<ConfigT>() where ConfigT : ConfigItemBase, new()
        {
            throw new System.NotImplementedException();
        }

        public ConfigT GetConfig<ConfigT>(string name) where ConfigT : ConfigItemBase, new()
        {
            throw new System.NotImplementedException();
        }
    }
}