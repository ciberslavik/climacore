using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using Clima.Basics.Configuration;
using Clima.Basics.Services;

namespace Clima.Configuration.FileSystem
{
    public class FSConfigurationStorage : IConfigurationStorage
    {
        private readonly IConfigurationSerializer _serializer;
        private readonly IFileSystem _fs;
        private readonly ISystemLogger _logger;
        private Dictionary<string, IConfigurationItem> _loaded;

        public FSConfigurationStorage(IConfigurationSerializer serializer, IFileSystem fs, ISystemLogger logger)
        {
            _serializer = serializer;
            _fs = fs;
            _logger = logger;

            if (!_fs.FolderExist(_fs.ConfigurationPath)) _fs.CreateDirectory(_fs.ConfigurationPath);

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
            var configName = typeof(ConfigT).Name;
            RegisterConfig(configName, instance);
        }

        public void RegisterConfig<ConfigT>(string name, ConfigT instance) where ConfigT : IConfigurationItem, new()
        {
            if (_loaded.ContainsKey(name))
                return;

            var fileName = name + _serializer.DataExtension;
            if (instance != null)
            {
                var data = _serializer.Serialize(instance);
                var fullPath = Path.Combine(_fs.ConfigurationPath, fileName);
                _fs.WriteTextFile(fullPath, data);
                _loaded.Add(name, instance);
            }
        }

        public ConfigT GetConfig<ConfigT>() where ConfigT : IConfigurationItem, new()
        {
            var configName = typeof(ConfigT).Name;

            return GetConfig<ConfigT>(configName);
        }

        public ConfigT GetConfig<ConfigT>(string name) where ConfigT : IConfigurationItem, new()
        {
            if (_loaded.ContainsKey(name))
                return (ConfigT) _loaded[name];

            var filePath = Path.Combine(_fs.ConfigurationPath, name + _serializer.DataExtension);
            var retValue = default(ConfigT);
            if (_fs.FileExist(filePath))
            {
                var data = _fs.ReadTextFile(filePath);
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
            else
            {
                _logger.Debug($"Configuration file: {name} not found, load default config");
                var configType = typeof(ConfigT);
                var defaultMI = configType
                    .GetMethod("CreateDefault", Type.EmptyTypes);
                if (defaultMI is not null)
                {
                    var defConfig = defaultMI.Invoke(null, new object[] { });
                    if (defConfig is not null)
                    {
                        retValue = (ConfigT) defConfig;
                        RegisterConfig<ConfigT>(configType.Name, retValue);
                    }
                }
            }
            return retValue;
        }

        private void SaveToFile(IConfigurationItem item, string name)
        {
            var fileName = name + _serializer.DataExtension;

            var data = _serializer.Serialize(item);
            var fullPath = Path.Combine(_fs.ConfigurationPath, fileName);
            _fs.WriteTextFile(fullPath, data);
        }

        public bool Exist<ConfigT>() where ConfigT : IConfigurationItem, new()
        {
            var configName = typeof(ConfigT).Name;
            return Exist(configName);
        }

        public bool Exist(string name)
        {
            var filePath = Path.Combine(_fs.ConfigurationPath, name + _serializer.DataExtension);
            if (_fs.FileExist(filePath))
                return true;
            else
                return false;
        }

        public void Save()
        {
            foreach (var configItem in _loaded) SaveToFile(configItem.Value, configItem.Key);
        }
    }
}