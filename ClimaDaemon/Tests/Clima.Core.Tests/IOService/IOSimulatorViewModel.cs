using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;
using System.Reactive.Linq;
using Avalonia.Threading;
using Clima.Basics;
using Clima.Core.IO;
using DynamicData;
using DynamicData.Binding;
using ReactiveUI;

namespace Clima.Core.Tests.IOService
{
    public class IOSimulatorViewModel:ReactiveObject
    {
        private readonly IIOService _ioService;
        private ObservableCollection<IDiscreteOutput> _outputs;
        private ObservableCollection<IDiscreteInput> _inputs;

        public IOSimulatorViewModel()
        {
            
            _inputs = new ObservableCollection<IDiscreteInput>()
            {
                new StubDiscreteInput(),
                new StubDiscreteInput()
            };
        }
        public IOSimulatorViewModel(IIOService ioService)
        {
            _ioService = ioService;

            var stubsDO = _ioService.Pins.DiscreteOutputs.Values.Select(p => p as StubDiscreteOutput);
            _outputs = new ObservableCollection<IDiscreteOutput>(stubsDO);
            _inputs = new ObservableCollection<IDiscreteInput>(_ioService.Pins.DiscreteInputs.Values);
            
            _outputs.CollectionChanged+= OutputsOnCollectionChanged;
        }

        private void OutputsOnCollectionChanged(object? sender, NotifyCollectionChangedEventArgs e)
        {
            throw new System.NotImplementedException();
        }

        public ObservableCollection<IDiscreteOutput> Outputs => _outputs;
        public ObservableCollection<IDiscreteInput> Inputs => _inputs;
    }
}