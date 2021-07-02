using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using Clima.DataModel.Configurations;

namespace Clima.Services.Configuration
{
    public class DefaultConfigurationStorage:IConfigurationStorage
    {
        private readonly IConfigurationSerializer _serializer;
        private readonly IFileSystem _fs;
        private Dictionary<string, ConfigItemBase> _loaded;
        
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

            RegisterConfig(defConfig);
        }

        public void RegisterConfig<ConfigT>(string name) where ConfigT : ConfigItemBase, new()
        {
            var defConfig = new ConfigT();

            RegisterConfig(name,defConfig);
        }

        public void RegisterConfig<ConfigT>(ConfigT instance) where ConfigT : ConfigItemBase, new()
        {
            string configName = typeof(ConfigT).FullName;
            RegisterConfig(configName, instance);
        }

        public void RegisterConfig<ConfigT>(string name, ConfigT instance) where ConfigT : ConfigItemBase, new()
        {
            string fileName = name + _serializer.DataExtension;
            if (instance != null)
            {
                string data = _serializer.Serialize(instance);
                string fullPath = Path.Combine(_fs.ConfigurationPath, fileName);
                _fs.WriteTextFile(fullPath, data);
            }
        }

        public ConfigT GetConfig<ConfigT>() where ConfigT : ConfigItemBase, new()
        {
            string configName = typeof(ConfigT).FullName;

            return GetConfig<ConfigT>(configName);
        }

        public ConfigT GetConfig<ConfigT>(string name) where ConfigT : ConfigItemBase, new()
        {
            string filePath = Path.Combine(_fs.ConfigurationPath, (name + _serializer.DataExtension));
            ConfigT retVavule = null;
            if (_fs.FileExist(filePath))
            {
                string data = _fs.ReadTextFile(filePath);
                try
                {
                    retVavule = _serializer.Deserialize<ConfigT>(data);
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                    throw;
                }
            }

            return retVavule;
        }

        public bool Exist<ConfigT>() where ConfigT : ConfigItemBase, new()
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