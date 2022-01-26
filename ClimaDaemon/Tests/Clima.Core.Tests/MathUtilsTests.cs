using System;
using Clima.Basics;
using NUnit.Framework;

namespace Clima.Core.Tests
{
    public class MathUtilsTests
    {
        public MathUtilsTests()
        {
        }

        [SetUp]
        public void Setup()
        {
            
        }

        [TestCase(22), TestCase(23), TestCase(24), TestCase(25)]
        public void Lerp_Test(int currentDay)
        {
            float largeDayTemp = 100f;
            float smallerDayTemp = 10f;

            int startDay = 22;
            int endDay = 25;
            
            int period = endDay - startDay;

            float diff = (currentDay - startDay);
            Console.WriteLine(diff);
            Console.WriteLine(diff/(float)period);
            Console.WriteLine(MathUtils.Lerp(smallerDayTemp,largeDayTemp,diff/period));
        }
    }
}