using Clima.Services.IO;

namespace FakeIOService
{
    public class FakeFC
    {
        private readonly FakeIO _io;
        private readonly int _fcNumber;
        private DiscreteOutput _enPin;
        private DiscreteInput _alarmPin;
        private AnalogOutput _analogPin;
        
        public FakeFC(FakeIO io, int fcNumber)
        {
            _io = io;
            _fcNumber = fcNumber;
            string enPinName = $"DO:{fcNumber}";
            string alarmPinName = $"DI:{fcNumber}";
            string analogPinName = $"AO:{fcNumber}";

            _enPin = io.DiscreteOutputs[enPinName];
            _alarmPin = io.DiscreteInputs[alarmPinName];
            _analogPin = io.AnalogOutputs[analogPinName];
        }
        
        
        
    }
}