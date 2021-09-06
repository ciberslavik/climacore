    /*using System;
    using Clima.Core.Controllers;
    using Clima.Core.DataModel.GraphModel;
    using Moq;
    using NUnit.Framework;

    namespace Clima.Core.Scheduler.Tests
    {
        [TestFixture]
        public class SchedulerMathTests
        {
            [Test]
            public void CreateScheduler_Test()
            {
                            
            }

            [Test]
            public void GetCurrentDayTemperature_Test()
            {
                var tempGraph = new TemperatureGraph();
                var pt1 = new ValueByDayPoint();
                pt1.DayNumber = 1;
                pt1.Value = 20;
                var pt2 = new ValueByDayPoint();
                pt2.DayNumber = 20;
                pt2.Value = 39.5f;
                tempGraph.AddPoint(pt1);
                tempGraph.AddPoint(pt2);

                var controllerFactory = Mock.Of<IControllerFactory>();
                var timeProvider = new StubTimeProvider();
                var sched = new ClimaScheduler(controllerFactory, timeProvider);
                sched.SetTemperatureGraph(tempGraph);

                string debugData = "";

                for (int i = 1; i < 10; i++)
                {
                    float value = sched.GetDayTemperature(i);
                    debugData += $"Day: {i} Value {value} \n";
                }
                
                Console.WriteLine(debugData);
                Assert.Pass();
            }

            private GraphBase<ValueByDayPoint> CreateGraph()
            {
                var tempGraph = new TemperatureGraph();
                Random rand = new Random();

                var days = new float[] {1, 2};

                int x = 0;
                for (int i = 1; i < 50; i += 2)
                {
                    var point = new ValueByDayPoint();
                    point.DayNumber = i;
                    point.Value = rand.Next(20, 40);
                    tempGraph.AddPoint(point);
                }
                
                return tempGraph;
            }
        }
    }*/