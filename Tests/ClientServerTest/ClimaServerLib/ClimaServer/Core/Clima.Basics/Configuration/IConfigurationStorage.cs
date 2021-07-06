namespace Clima.Basics.Configuration
{
    public interface IConfigurationStorage
    {
        void RegisterConfig<ConfigT>() where ConfigT : IConfigurationItem, new();
        void RegisterConfig<ConfigT>(string name) where ConfigT : IConfigurationItem, new();
        void RegisterConfig<ConfigT>(ConfigT instance) where ConfigT : IConfigurationItem, new();
        void RegisterConfig<ConfigT>(string name, ConfigT instance) where ConfigT : IConfigurationItem, new();

        ConfigT GetConfig<ConfigT>() where ConfigT : IConfigurationItem, new();
        ConfigT GetConfig<ConfigT>(string name) where ConfigT : IConfigurationItem, new();
        bool Exist<ConfigT>() where ConfigT : IConfigurationItem, new();
        bool Exist(string name);
        void Save();
    }
}