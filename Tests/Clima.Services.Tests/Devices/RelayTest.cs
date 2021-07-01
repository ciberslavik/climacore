using System.Threading;
using Clima.Services.Devices;
using Clima.Services.IO;
using NSubstitute;
using NSubstitute.ReceivedExtensions;
using NUnit.Framework;

namespace Clima.Services.Tests.Devices
{
    public class RelayTest
    {
        public RelayTest()
        {
        }

        [Test]
        public void InitRelay_Test()
        {
            Relay relay = new Relay(new FakeTimer());
            DiscreteOutput enablePin = Substitute.For<DiscreteOutput>();
            enablePin.State = true;
            
            DiscreteInput monitorPin = Substitute.For<DiscreteInput>();
            relay.EnablePin = enablePin;
            relay.MonitorPin = monitorPin;
            
            relay.InitDevice();

            Assert.AreEqual(enablePin.State, false);
        }

        [Test]
        public void RelayToOnState_Test()
        {
            Relay relay = new Relay(new FakeTimer());
            DiscreteOutput enablePin = Substitute.For<DiscreteOutput>();
            DiscreteInput monitorPin = Substitute.For<DiscreteInput>();
            relay.EnablePin = enablePin;
            relay.MonitorPin = monitorPin;
            
            relay.InitDevice();
            
            relay.On();

            monitorPin.State = true;
            
            Assert.AreEqual(enablePin.State, true);
        }

        [Test]
        public void RealayMonitorTimeoutException_Test()
        {
            var timer = new FakeTimer();
            Relay relay = new Relay(timer);
            DiscreteOutput enablePin = Substitute.For<DiscreteOutput>();
            DiscreteInput monitorPin = Substitute.For<DiscreteInput>();
            relay.EnablePin = enablePin;
            relay.MonitorPin = monitorPin;
            relay.Configuration.MonitorTimeout = 10;
            relay.InitDevice();
            
            relay.On();
            
            Assert.Throws<DeviceException>(() => timer.InvokeElapsed());
        }
    }
}