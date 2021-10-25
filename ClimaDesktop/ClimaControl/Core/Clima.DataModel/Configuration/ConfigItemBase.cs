using System.Collections.Generic;
using Clima.Basics;

namespace Clima.DataModel.Configuration
{
    public abstract class ConfigItemBase : ObservableObject
    {
        private string _header;

        public string Header
        {
            get => _header;
            set => Update(ref _header, value);
        }

        public List<ConfigItemBase> ChildItems { get; set; }
    }
}