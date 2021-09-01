using Clima.Core.Conrollers.Ventilation;
using Clima.Core.DataModel;
using Clima.Core.Devices;
using Moq;
using NSubstitute;
using NSubstitute.Exceptions;
using NSubstitute.Extensions;
using NUnit.Framework;

namespace Clima.Core.Controllers.Tests
{
    public class FanControllerTests
    {
        [SetUp]
        public void Setup()
        {
        
        }

        [Test]
        public void FillAndRebuildTable_Test()
        {
            var devProvider = Mock.Of<IDeviceProvider>();
            var ventController = new VentilationController(devProvider);
            
            FillController(ref ventController);
            
            ventController.RebuildControllerTable();
            
            Assert.Pass();
        }

        [TestCase(2000)]
        [TestCase(15000)]
        [TestCase(20000)]
        [TestCase(100000)]
        [TestCase(200000)]
        public void SetPerformance_Test(float performance)
        {
            var devProvider = Mock.Of<IDeviceProvider>();
            var ventController = new VentilationController(devProvider);

            FillController(ref ventController);

            ventController.RebuildControllerTable();
            
            ventController.SetPerformance(performance);
            Assert.Pass();
        }

        private void FillController(ref VentilationController controller)
        {
            var analogFan = new StubAnalogFan();
            analogFan.State = new FanState()
            {
                Info = new FanInfo(){
                    Key = "1",
                    FanName = "Analog fan1",
                    FanCount = 2,
                    Hermetise = false,
                    Performance = 15000,
                    Priority = 1,
                    StartValue = 0.10f,
                    StopValue = 0.05f,
                    IsAnalog = true
                }
            };
            controller.AddFan(analogFan);

            for (int i = 2; i <= 10; i++)
            {
                var discreteFan = new StubDiscreteFan();
                discreteFan.State = new FanState()
                {
                    Info = new FanInfo(){
                    
                    Key = $"DFAN:{i}",
                    FanName = "Discrete fan1",
                    FanCount = 2,
                    Hermetise = false,
                    Performance = 15000,
                    Priority = i,
                    StartValue = 0.30f,
                    StopValue = 0.25f,
                    IsAnalog = false
                    }
                };
                
                controller.AddFan(discreteFan);
            }
        }
    }
}