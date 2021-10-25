using System.Collections.Generic;

namespace Clima.DataModel.Configuration
{
    public interface IConfigurationService
    {
        List<ConfigItemBase> Items { get; }
        void AddItem(ConfigItemBase item);
    }
}