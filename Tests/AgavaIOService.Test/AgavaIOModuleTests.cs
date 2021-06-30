using System;
using Clima.AgavaModBusIO;
using Clima.AgavaModBusIO.Model;
using Clima.Services.IO;
using NUnit.Framework;

namespace AgavaIOService.Test
{
    public class AgavaIoModuleTests
    {
        [SetUp]
        public void Setup()
        {
        }
                            //  R     R     DI    R     R     DI
        [TestCase(new ushort[] {0x03, 0x03, 0x06, 0x03, 0x03, 0x06})]
                            //  DI    TMP   AI    DI    TMP   AIO
        [TestCase(new ushort[] {0x06, 0x07, 0x04, 0x06, 0x07, 0x05})]
                            //  
        [TestCase(new ushort[] {0x03, 0x03, 0x03, 0x03, 0x03, 0x03})]
        public void CreateModule_Test(ushort[] caseData)
        {
            AgavaIOModule module = AgavaIOModule.CreateModule(1, caseData);
            
            Assert.NotNull(module);
        }

        [Test]
        public void GetPinByName_Test()
        {
            AgavaIOModule module = AgavaIOModule.CreateModule(
                1,
                new ushort[] {0x03, 0x03, 0x06, 0x03, 0x03, 0x06});

            var pin = module.GetPinByName("DO:1:5");
            
            Assert.IsNotNull(pin);
        }

        [Test]
        public void GetDORawData_Test()
        {
            AgavaIOModule module = AgavaIOModule.CreateModule(
                1,
                new ushort[] {0x03, 0x03, 0x06, 0x03, 0x03, 0x06});

            DiscreteOutput pin = module.Pins.DiscreteOutputs["DO:1:4"];
            pin.State = true;
            
            var result = module.GetDORawData();
            
            Assert.IsTrue(result[0] == 8);
        }

        [TestCase(new ushort[] {0x06, 0x07, 0x04, 0x06, 0x07, 0x05})]
        public void AnalogOutputChangedInvoked_Test(ushort[] caseData)
        {
            AgavaIOModule module = AgavaIOModule.CreateModule(1, caseData);
            bool wasCalled = false;
            module.AnalogOutputChanged += ea =>
            {
                wasCalled = true;
            };
            var pin = module.GetPinByName("AO:1:1") as AgavaAOutput;

            pin.Value = 100.0;

            Assert.IsTrue(wasCalled);
        }
        [Test]
        public void SetDIRawData_Test()
        {
            AgavaIOModule module = AgavaIOModule.CreateModule(
                1,
                new ushort[] {0x03, 0x03, 0x06, 0x03, 0x03, 0x06});
            
            module.SetDIRawData(new ushort[]{8});

            AgavaDInput pin = module.GetPinByName("DI:1:4") as AgavaDInput;
            
            Assert.NotNull(pin);
            Assert.IsTrue(pin.State);
        }
        [Test]
        public void DIPinStateChancgedCalled_Test()
        {
            bool wasCalled = false;
            AgavaIOModule module = AgavaIOModule.CreateModule(
                1,
                new ushort[] {0x03, 0x03, 0x06, 0x03, 0x03, 0x06});

            ((AgavaDInput) module.GetPinByName("DI:1:4")).PinStateChanged += ea =>
            {
                wasCalled = true;
            };
            
            module.SetDIRawData(new ushort[]{8});
            
            
            Assert.IsTrue(wasCalled);
        }
        
    }
}