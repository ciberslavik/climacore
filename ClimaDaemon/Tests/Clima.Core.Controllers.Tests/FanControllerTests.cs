using Clima.Core.Conrollers.Ventilation;
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
            
            var ventController = new VentilationController();
            
            FillController(ref ventController);
            
            ventController.RebuildControllerTable();
            
            Assert.Pass();
        }

        [Test]
        public void SetPerformance_Test()
        {
            var ventController = new VentilationController();

            FillController(ref ventController);

            ventController.RebuildControllerTable();
            
            ventController.SetPerformance(100000);
            Assert.Pass();
        }

        private void FillController(ref VentilationController controller)
        {
            var analogFan = new StubAnalogFan();
            analogFan.State = new FanState()
            {
                Disabled = false,
                FanId = 1,
                FanName = "Analog fan1",
                FansCount = 2,
                Hermetise = false,
                Performance = 15000,
                Priority = 1,
                StartValue = 0.10f,
                StopValue = 0.05f
            };
            controller.AddFan(analogFan);

            for (int i = 2; i <= 10; i++)
            {
                var discreteFan = new StubDiscreteFan();
                discreteFan.State = new FanState()
                {
                    Disabled = false,
                    FanId = i,
                    FanName = "Discrete fan1",
                    FansCount = 2,
                    Hermetise = false,
                    Performance = 15000,
                    Priority = i,
                    StartValue = 0.10f,
                    StopValue = 0.05f
                };
                
                controller.AddFan(discreteFan);
            }
        }
    }
}