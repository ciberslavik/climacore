using System.Threading;
using Clima.DataModel.Configurations.IOSystem;
using Clima.Services.Devices;
using Clima.Services.Devices.Configs;
using Clima.Services.IO;
using NSubstitute;
using NSubstitute.ReceivedExtensions;
using NUnit.Framework;

namespace Clima.Services.Tests.Devices
{
    public class RelayTest
    {
        private Relay _relay;
        public RelayTest()
        {
        }
        [SetUp]
        public void SetupRelay()
        {
            
        }
        [Test]
        public void InitRelay_Test()
        {
            FakeTimer timer = new FakeTimer();
            Relay relay = new Relay(timer);
            
            var enablePin = Substitute.For<IDiscreteOutput>();
            enablePin.State.Returns(false);
            
            var monitorPin = Substitute.For<IDiscreteInput>();
            monitorPin.State.Returns(false);
            
            relay.EnablePin = enablePin;
            relay.MonitorPin = monitorPin;

            _relay = relay;
            
            _relay.InitDevice(RelayConfig.CreateDefaultConfig());
            
            monitorPin.State.Returns(false);
            
            timer.InvokeElapsed();
            
            enablePin.Received().SetState(false,true);
            Assert.AreEqual(_relay.EnablePin.State, false);
        }

        [Test]
        public void RelayToOnState_Test()
        {
            Relay relay = new Relay(new FakeTimer());
            
            var enablePin = Substitute.For<IDiscreteOutput>();
            enablePin.State.Returns(false);
            
            var monitorPin = Substitute.For<IDiscreteInput>();
            monitorPin.State.Returns(false);
            
            relay.EnablePin = enablePin;
            relay.MonitorPin = monitorPin;

            _relay = relay;
            
            _relay.InitDevice(RelayConfig.CreateDefaultConfig());
            
            monitorPin.State.Returns(false);
            
            _relay.On();
            
            _relay.MonitorPin.PinStateChanged +=
                Raise.Event<DiscretePinStateChangedEventHandler>(new object[]
                {
                    new DiscretePinStateChangedEventArgs(_relay.MonitorPin, false, true)
                }); //With(new DiscretePinStateChangedEventArgs(monitorPin, false, true));
            
            enablePin.Received().SetState(true,true);
            Assert.AreEqual(_relay.EnablePin.State, true);
            
        }

        [Test]
        public void RealayMonitorTimeoutException_Test()
        {
            var timer = new FakeTimer();
            Relay relay = new Relay(timer);
            var enablePin = Substitute.For<IDiscreteOutput>();
            var monitorPin = Substitute.For<IDiscreteInput>();
            relay.EnablePin = enablePin;
            relay.MonitorPin = monitorPin;
            relay.Configuration.MonitorTimeout = 10;
            relay.InitDevice(default(DeviceConfigBase));
            
            relay.On();
            
            Assert.Throws<DeviceException>(() => timer.InvokeElapsed());
        }
    }
}