using Clima.DataModel.Configurations.IOSystem;
using Clima.Services.Devices;
using Clima.Services.IO;
using NSubstitute;
using NUnit.Framework;

namespace Clima.Services.Tests.Devices
{
    public class FrequencyConverterTests
    {
        public FrequencyConverterTests()
        {
        }
        [Test]
        public void FCStart_Test()
        {
            var fcConfig = FrequencyConverterConfig.CreateDefault();
            
            var enablePin = Substitute.For<IDiscreteOutput>();
            enablePin.PinName.Returns(fcConfig.EnablePinName);
            
            var alarmPin = Substitute.For<IDiscreteInput>();
            alarmPin.PinName.Returns(fcConfig.AlarmPinName);
            
            var analogPin = Substitute.For<IAnalogOutput>();
            analogPin.PinName.Returns(fcConfig.AnalogPinName);

            var converter = new FrequencyConverter();

            converter.EnablePin = enablePin;
            converter.AlarmPin = alarmPin;
            converter.AnalogPin = analogPin;
            
            converter.InitDevice(fcConfig);
        }
        
    }
}