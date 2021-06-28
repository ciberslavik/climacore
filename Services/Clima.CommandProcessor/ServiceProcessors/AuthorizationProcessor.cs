using Clima.Services.Authorization;
using Clima.Services.Communication;

namespace Clima.CommandProcessor.ServiceProcessors
{
    public class AuthorizationProcessor:IAuthorizationProcessor
    {
        private readonly IAuthorizationService _authService;
        private readonly IServer _server;
        private readonly ICommunicationSerializer _serializer;

        public AuthorizationProcessor(IAuthorizationService authService,IServer server,ICommunicationSerializer serializer)
        {
            _authService = authService;
            _server = server;
            _serializer = serializer;
        }

        public void Process(string data)
        {
            throw new System.NotImplementedException();
        }
    }
}