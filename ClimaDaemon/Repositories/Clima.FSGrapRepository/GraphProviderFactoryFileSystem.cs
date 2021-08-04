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

        private GraphProviderConfig<TemperatureGraphPointConfig> _tempProviderConfig;
        //private TemperatureGraphProviderConfig _tempProviderConfig;
        public GraphProviderFactoryFileSystem(IConfigurationStorage configStorgae)
        {
            _configStorgae = configStorgae;
            LoadOrCreateTempGraphProviderConfig(); 
            var tp = new TemperatureGraphProvider(_tempProviderConfig);
            //tp.SaveNeeded += TpOnSaveNeeded;

            _temperatureProvider = tp;
        }

        private void TpOnSaveNeeded(object? sender, EventArgs e)
        {
            _configStorgae.Save();
        }

        protected void LoadOrCreateTempGraphProviderConfig()
        {
            var configName = "TemperatureGrapProvider";
            if (!_configStorgae.Exist(configName))
            {
                var tempProviderConfig = new GraphProviderConfig<TemperatureGraphPointConfig>();
                _configStorgae.RegisterConfig(configName, tempProviderConfig);
            }
            _tempProviderConfig = _configStorgae.GetConfig<GraphProviderConfig<TemperatureGraphPointConfig>>(configName);
        }

        public IGraphProvider<TemperatureGraph, ValueByDayPoint> TemperatureGraphProvider()
        {
            return _temperatureProvider;
        }

        public IGraphProvider<VentilationMinMaxGraph,MinMaxByDayPoint> VentilationMinMaxGraphProvider()
        {
            throw new System.NotImplementedException();
        }

        public IGraphProvider<ValvePerVentilationGraph,ValueByValuePoint> ValvePerVentilationGraphProvider()
        {
            throw new System.NotImplementedException();
        }
    }
}