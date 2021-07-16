using System;
using System.Collections.Generic;
using System.IO;
using Clima.Basics.Configuration;
using Clima.Basics.Services;

namespace Clima.Configuration.FileSystem
{
    public class FSConfigurationStorage : IConfigurationStorage
    {
        private readonly IConfigurationSerializer _serializer;
        private readonly IFileSystem _fs;
        private Dictionary<string, IConfigurationItem> _loaded;

        public FSConfigurationStorage(IConfigurationSerializer serializer, IFileSystem fs)
        {
            _serializer = serializer;
            _fs = fs;

            if (!_fs.FolderExist(_fs.ConfigurationPath))
            {
                _fs.CreateDirectory(_fs.ConfigurationPath);
            }
        }


        public void RegisterConfig<ConfigT>() where ConfigT : IConfigurationItem, new()
        {
            var defConfig = new ConfigT();

            RegisterConfig(defConfig);
        }

        public void RegisterConfig<ConfigT>(string name) where ConfigT : IConfigurationItem, new()
        {
            var defConfig = new ConfigT();

            RegisterConfig(name, defConfig);
        }

        public void RegisterConfig<ConfigT>(ConfigT instance) where ConfigT : IConfigurationItem, new()
        {
            string configName = typeof(ConfigT).FullName;
            RegisterConfig(configName, instance);
        }

        public void RegisterConfig<ConfigT>(string name, ConfigT instance) where ConfigT : IConfigurationItem, new()
        {
            string fileName = name + _serializer.DataExtension;
            if (instance != null)
            {
                string data = _serializer.Serialize(instance);
                string fullPath = Path.Combine(_fs.ConfigurationPath, fileName);
                _fs.WriteTextFile(fullPath, data);
            }
        }

        public ConfigT GetConfig<ConfigT>() where ConfigT : IConfigurationItem, new()
        {
            string configName = typeof(ConfigT).FullName;

            return GetConfig<ConfigT>(configName);
        }

        public ConfigT GetConfig<ConfigT>(string name) where ConfigT : IConfigurationItem, new()
        {
            string filePath = Path.Combine(_fs.ConfigurationPath, (name + _serializer.DataExtension));
            var retValue = default(ConfigT);
            if (_fs.FileExist(filePath))
            {
                string data = _fs.ReadTextFile(filePath);
                try
                {
                    retValue = _serializer.Deserialize<ConfigT>(data);
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                    throw;
                }
            }

            return retValue;
        }

        public bool Exist<ConfigT>() where ConfigT : IConfigurationItem, new()
        {
            string configName = typeof(ConfigT).FullName;
            return Exist(configName);
        }

        public bool Exist(string name)
        {
            string filePath = Path.Combine(_fs.ConfigurationPath, name + _serializer.DataExtension);
            if (_fs.FileExist(filePath))
                return true;
            else
                return false;
        }

        public void Save()
        {

        }
    }
}