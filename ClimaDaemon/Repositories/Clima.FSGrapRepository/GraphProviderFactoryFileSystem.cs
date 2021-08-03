using System;
using Clima.Basics.Configuration;
using Clima.Core.DataModel.GraphModel;
using Clima.FSGrapRepository.Configuration;

namespace Clima.FSGrapRepository
{
    public class GraphProviderFactoryFileSystem:IGraphProviderFactory
    {
        private readonly IConfigurationStorage _configStorgae;
        private readonly IGraphProvider<TemperatureGraph> _temperatureProvider;


        private TemperatureGraphProviderConfig _tempProviderConfig;
        public GraphProviderFactoryFileSystem(IConfigurationStorage configStorgae)
        {
            _configStorgae = configStorgae;
            LoadOrCreateTempGraphProviderConfig(); 
            var tp = new TemperatureGraphProvider(_tempProviderConfig);
            tp.SaveNeeded += TpOnSaveNeeded;

            _temperatureProvider = tp;
        }

        private void TpOnSaveNeeded(object? sender, EventArgs e)
        {
            _configStorgae.Save();
        }

        protected void LoadOrCreateTempGraphProviderConfig()
        {
            var configName = TemperatureGraphProviderConfig.FileName;
            if (!_configStorgae.Exist(configName))
            {
                var tempProviderConfig = new TemperatureGraphProviderConfig();
                tempProviderConfig.Graphs.Add("default", new TemperatureGraphConfig());
                _configStorgae.RegisterConfig<TemperatureGraphProviderConfig>(TemperatureGraphProviderConfig.FileName);
            }
            _tempProviderConfig = _configStorgae.GetConfig<TemperatureGraphProviderConfig>(configName);
        }

        public IGraphProvider<TemperatureGraph> TemperatureGraphProvider()
        {
            return _temperatureProvider;
        }

        public IGraphProvider<VentilationMinMaxGraph> VentilationMinMaxGraphProvider()
        {
            throw new System.NotImplementedException();
        }

        public IGraphProvider<ValvePerVentilationGraph> ValvePerVentilationGraphProvider()
        {
            throw new System.NotImplementedException();
        }
    }
}