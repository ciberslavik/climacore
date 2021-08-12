using Clima.Core.Devices.Configuration;
using NUnit.Framework;

namespace Clima.Core.Devices.Tests
{
    public class LinearServoTests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void Test1()
        {
            var config = ServoConfig.CreateDefault(0);

            var servo = new LinearServo();
            servo.Configuration = config;

            servo.ProcessPosition(10, 12.5);
        }

        [Test]
        public void CoarseMove_Test()
        {
            var config = ServoConfig.CreateDefault(0);

            var servo = new LinearServo();
            servo.Configuration = config;

            //servo.MoveCoarse();
        }
    }
}