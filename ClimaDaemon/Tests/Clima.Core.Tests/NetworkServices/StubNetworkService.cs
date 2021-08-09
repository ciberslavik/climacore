using Clima.Basics.Services.Communication;

namespace Clima.Core.Tests.NetworkServices
{
    public class StubNetworkService : INetworkService
    {
        public StubNetworkService()
        {
        }

        [ServiceMethod]
        public StubNetworkResponse GetData(StubNetworkRequest request)
        {
            return new StubNetworkResponse();
        }

        [ServiceMethod]
        public StubNetworkResponse SetData(StubNetworkRequest request)
        {
            return new StubNetworkResponse();
        }

        public void TestMethodNotMarking()
        {
        }

        public string ServiceName { get; }
    }
}