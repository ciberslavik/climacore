using Clima.AgavaModBusIO.Model;
using NUnit.Framework;

namespace AgavaIOService.Test
{
    public class AgavaIoModuleTests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void CreateModule_Test()
        {
            var module = AgavaIoModule.CreateIoModule(
                1,
                new ushort[] {0x03, 0x03, 0x06, 0x03, 0x03, 0x06});
            
            Assert.Pass();
        }
    }
}