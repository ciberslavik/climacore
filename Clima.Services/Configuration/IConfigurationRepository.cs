namespace Clima.Services.Configuration
{
    public interface IConfigurationRepository
    {
        void RegisterConfig<ConfigT>() where ConfigT : ConfigItemBase;
        void RegisterConfig<ConfigT>(string name) where ConfigT : ConfigItemBase;
        void RegisterConfig<ConfigT>(ConfigT instance) where ConfigT : ConfigItemBase;
        void RegisterConfig<ConfigT>(string name, ConfigT instance) where ConfigT : ConfigItemBase;

        ConfigT GetConfig<ConfigT>() where ConfigT : ConfigItemBase;
        ConfigT GetConfig<ConfigT>(string name) where ConfigT : ConfigItemBase;
        
    }
}