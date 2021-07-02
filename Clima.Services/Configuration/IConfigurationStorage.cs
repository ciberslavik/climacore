using Clima.DataModel.Configurations;

namespace Clima.Services.Configuration
{
    public interface IConfigurationStorage
    {
        void RegisterConfig<ConfigT>() where ConfigT : ConfigItemBase, new();
        void RegisterConfig<ConfigT>(string name) where ConfigT : ConfigItemBase, new();
        void RegisterConfig<ConfigT>(ConfigT instance) where ConfigT : ConfigItemBase, new();
        void RegisterConfig<ConfigT>(string name, ConfigT instance) where ConfigT : ConfigItemBase, new();

        ConfigT GetConfig<ConfigT>() where ConfigT : ConfigItemBase, new();
        ConfigT GetConfig<ConfigT>(string name) where ConfigT : ConfigItemBase, new();
        bool Exist<ConfigT>() where ConfigT : ConfigItemBase, new();
        bool Exist(string name);
        void Save();
    }
}