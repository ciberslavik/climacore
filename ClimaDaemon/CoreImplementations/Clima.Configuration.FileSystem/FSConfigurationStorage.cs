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
            
            _loaded = new Dictionary<string, IConfigurationItem>();
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
            if(_loaded.ContainsKey(name))
                return;
            
            string fileName = name + _serializer.DataExtension;
            if (instance != null)
            {
                string data = _serializer.Serialize(instance);
                string fullPath = Path.Combine(_fs.ConfigurationPath, fileName);
                _fs.WriteTextFile(fullPath, data);
                _loaded.Add(name, instance);
            }
        }

        public ConfigT GetConfig<ConfigT>() where ConfigT : IConfigurationItem, new()
        {
            string configName = typeof(ConfigT).FullName;

            return GetConfig<ConfigT>(configName);
        }

        public ConfigT GetConfig<ConfigT>(string name) where ConfigT : IConfigurationItem, new()
        {
            if (_loaded.ContainsKey(name))
                return (ConfigT)_loaded[name];
            
            string filePath = Path.Combine(_fs.ConfigurationPath, (name + _serializer.DataExtension));
            var retValue = default(ConfigT);
            if (_fs.FileExist(filePath))
            {
                string data = _fs.ReadTextFile(filePath);
                try
                {
                    retValue = _serializer.Deserialize<ConfigT>(data);
                    _loaded.Add(name, retValue);
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                    throw;
                }
            }

            return retValue;
        }

        private void SaveToFile(IConfigurationItem item, string name)
        {
            string fileName = name + _serializer.DataExtension;

            string data = _serializer.Serialize(item);
            string fullPath = Path.Combine(_fs.ConfigurationPath, fileName);
            _fs.WriteTextFile(fullPath, data);
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
            foreach (var configItem in _loaded)
            {
                SaveToFile(configItem.Value, configItem.Key);
            }
        }
    }
}