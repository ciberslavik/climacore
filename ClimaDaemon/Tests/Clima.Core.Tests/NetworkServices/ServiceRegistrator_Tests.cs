using Moq;
using NUnit.Framework;
using Clima.Basics.Services;
using Clima.Basics.Services.Communication;
using Clima.Basics.Services.Communication.Messages;

namespace Clima.Core.Tests.NetworkServices
{
    public class ServiceRegistrator_Tests
    {
        [Test]
        public void RegisterService_Test()
        {
            var serviceProvider = new Mock<IServiceProvider>();
            var messageTypeProvider = new Mock<IMessageTypeProvider>();
            var serviceExecutor = new Mock<IServiceExecutor>();

            var registrator = new DefaultServiceRegistrator(serviceProvider.Object,
                messageTypeProvider.Object,
                serviceExecutor.Object);

            registrator.RegisterNetworkService<StubNetworkService>();

            serviceProvider.VerifyAll();
        }
    }
}