using System;
using Clima.CommandProcessor.Requests;
using Clima.CommandProcessor.ServiceProcessors;
using Clima.NewtonSoftJsonSerializer;
using Clima.Services.Communication;
using NSubstitute;
using NUnit.Framework;

namespace Clima.CommandProcessor.Tests
{
    public class ServiceProcessor_Tests
    {
        private ICommunicationSerializer _serializer;
        private ICommandProcessor _processor;
        private IServer _server;
        private IServiceProcessorFactory _factory;
        [SetUp]
        public void Setup()
        {
            _serializer = new NewtonsoftCommunicationSerializer();
            _server = Substitute.For<IServer>();
            _factory = Substitute.For<IServiceProcessorFactory>();
            _processor = new CommandProcessor(_server, _serializer, _factory);
            
            
        }

        [Test]
        public void AuthorizationProcessor_GetUsers_Test()
        {
            NetworkRequest request = new NetworkRequest();
            request.RequestName = "AuthorizationRequest";
            
            AuthorizationRequest authRequest = new AuthorizationRequest();
            authRequest.Function = "GetUsers";
            request.Data = _serializer.Serialize(authRequest);
            
            string data = _serializer.Serialize(request);

            _processor.ProcessCommand(Guid.NewGuid(), data);
            
            Assert.Pass();
        }
        [Test]
        public void Test_Devider()
        {
            for (int i = 0; i < 64; i++)
            {
                int ostatok = i % 16;
                int b1 = i / 16;
                Console.WriteLine($"Остаток:{ostatok}:{b1}");    
            }
                
        }
    }
}