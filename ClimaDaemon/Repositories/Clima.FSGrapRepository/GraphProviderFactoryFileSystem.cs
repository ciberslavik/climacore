using System;
using Clima.Basics.Configuration;
using Clima.Core.DataModel.GraphModel;
using Clima.FSGrapRepository.Configuration;

namespace Clima.FSGrapRepository
{
    public class GraphProviderFactoryFileSystem:IGraphProviderFactory
    {
        private readonly IConfigurationStorage _configStorgae;
        private readonly IGraphProvider<TemperatureGraph, ValueByDayPoint> _temperatureProvider;
        private readonly IGraphProvider<VentilationGraph, MinMaxByDayPoint> _ventilationProvider;
        private readonly IGraphProvider<ValvePerVentilationGraph, ValueByValuePoint> _valveProvider;
        
        private GraphProviderConfig<TemperatureGraphPointConfig> _tempProviderConfig;
        private GraphProviderConfig<VentilationGraphPointConfig> _ventProviderConfig;
        private GraphProviderConfig<ValveGraphPointConfig> _valveProviderConfig;
        //private TemperatureGraphProviderConfig _tempProviderConfig;
        public GraphProviderFactoryFileSystem(IConfigurationStorage configStorgae)
        {
            _configStorgae = configStorgae;
            
            LoadOrCreateTempGraphProviderConfig(); 
            _temperatureProvider = new TemperatureGraphProvider(_tempProviderConfig);

            LoadOrCreateVentilationProviderConfig();
            _ventilationProvider = new VentilationGraphProvider(_ventProviderConfig);

            LoadOrCreateValveGraphProviderConfig();
            _valveProvider = new ValveGraphProvider(_valveProviderConfig);
        }

        private void TpOnSaveNeeded(object? sender, EventArgs e)
        {
            _configStorgae.Save();
        }

        public void Save()
        {
            _configStorgae.Save();
        }

        protected void LoadOrCreateVentilationProviderConfig()
        {
            var configName = "VentilationGraphProvider";
            if (!_configStorgae.Exist(configName))
            {
                var ventProviderConfig = new GraphProviderConfig<VentilationGraphPointConfig>();
                ventProviderConfig.ConfigurationName = "DefaultConfig";
                ventProviderConfig.CurrentGraph = "Default";
                ventProviderConfig.Graphs.Add("Default", new GraphConfig<VentilationGraphPointConfig>()
                {
                    Info = new ProfileInfo()
                    {
                        Key="Default",
                        Name="Default Graph",
                        CreationTime = DateTime.Now,
                        ModifiedTime = DateTime.Now
                    },
                    Points =
                    {
                        new VentilationGraphPointConfig(1,10,80),
                        new VentilationGraphPointConfig(50,10,80)
                    }
                });
                _configStorgae.RegisterConfig(configName, ventProviderConfig);
                _configStorgae.Save();
            }
            _ventProviderConfig = _configStorgae.GetConfig<GraphProviderConfig<VentilationGraphPointConfig>>(configName);

        }
        protected void LoadOrCreateTempGraphProviderConfig()
        {
            var configName = "TemperatureGraphProvider";
            if (!_configStorgae.Exist(configName))
            {
                var tempProviderConfig = new GraphProviderConfig<TemperatureGraphPointConfig>();
                tempProviderConfig.ConfigurationName = "DefaultConfig";
                tempProviderConfig.CurrentGraph = "Default";
                tempProviderConfig.Graphs.Add("Default", new GraphConfig<TemperatureGraphPointConfig>()
                {
                    Info = new ProfileInfo()
                    {
                        Key="Default",
                        Name="Default Graph",
                        CreationTime = DateTime.Now,
                        ModifiedTime = DateTime.Now
                    }
                });
                _configStorgae.RegisterConfig(configName, tempProviderConfig);
                _configStorgae.Save();
            }
            _tempProviderConfig = _configStorgae.GetConfig<GraphProviderConfig<TemperatureGraphPointConfig>>(configName);
        }

        protected void LoadOrCreateValveGraphProviderConfig()
        {
            var configName = "ValveGraphProvider";
            if (!_configStorgae.Exist(configName))
            {
                var valveProviderConfig = new GraphProviderConfig<ValveGraphPointConfig>();
                valveProviderConfig.ConfigurationName = "DefaultConfig";
                valveProviderConfig.CurrentGraph = "Default";
                valveProviderConfig.Graphs.Add("Default", new GraphConfig<ValveGraphPointConfig>()
                {
                    Info = new ProfileInfo()
                    {
                        Key="Default",
                        Name="Default Graph",
                        CreationTime = DateTime.Now,
                        ModifiedTime = DateTime.Now
                    },
                    Points =
                    {
                        new ValveGraphPointConfig(0,0),
                        new ValveGraphPointConfig(100,100)
                    }
                });
                _configStorgae.RegisterConfig(configName, valveProviderConfig);
                _configStorgae.Save();
            }
            _tempProviderConfig = _configStorgae.GetConfig<GraphProviderConfig<TemperatureGraphPointConfig>>(configName);
        }
        public IGraphProvider<TemperatureGraph, ValueByDayPoint> TemperatureGraphProvider()
        {
            return _temperatureProvider;
        }

        public IGraphProvider<VentilationGraph,MinMaxByDayPoint> VentilationGraphProvider()
        {
            return _ventilationProvider;
        }

        public IGraphProvider<ValvePerVentilationGraph,ValueByValuePoint> ValveGraphProvider()
        {
            return _valveProvider;
        }
    }
}