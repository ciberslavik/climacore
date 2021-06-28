using System;
using System.Collections.Generic;
using System.Reflection;
using System.Runtime.CompilerServices;
using Clima.CommandProcessor.Requests;
using Clima.CommandProcessor.ServiceProcessors;
using Clima.Services.Authorization;
using Clima.Services.Communication;
using IServiceProvider = Clima.Services.IServiceProvider;

namespace Clima.CommandProcessor
{
    public class CommandProcessor:ICommandProcessor
    {
        //private readonly IServiceProvider _container;
        private readonly ICommunicationSerializer _serializer;
        private readonly IServiceProcessorFactory _factory;
        private readonly IServer _server;
        public CommandProcessor(IServer server,
                                ICommunicationSerializer serializer,
                                IServiceProcessorFactory factory)
        {
            //_container = container;
            _serializer = serializer;
            _factory = factory;
            _server = server;
        }
        public void ProcessCommand(Guid sessionId, string data)
        {
            NetworkRequest request = null;
            try
            {
                request = _serializer.Deserialize<NetworkRequest>(data);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }

            if (request != null)
            {
                Console.WriteLine($"Request type:{request.RequestName}");
                string typeName = "Clima.CommandProcessor.Requests." + request.RequestName;
                Type requestType = Type.GetType(typeName);
                if (requestType != null)
                {
                    MethodInfo MI = typeof(ICommunicationSerializer)
                        .GetMethod("Deserialize")
                        ?.MakeGenericMethod(requestType);
                    if (MI != null)
                    {
                        var serviceRequest = MI.Invoke(_serializer, new object[] {request.Data});
                        if (serviceRequest is AuthorizationRequest authRequest)
                        {
                            
                        }
                    }
                }
            }
        }

        
        /*private void ProcessRequestUserList(Guid sessionId, NetworkRequest request)
        {
            RequestUserList requestUL;
            var authRepo = _container.Resolve<IAuthorizationRepository>();
            try
            {
                requestUL = _serializer.Deserialize<RequestUserList>(request.Data);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }

            if (requestUL.RequestType == "AllUsers")
            {
                NetworkReply reply = new NetworkReply();
                reply.RequestType = request.RequestType;


                ReplyUserList replyUL = new ReplyUserList();
                replyUL.RequestType = requestUL.RequestType;
                replyUL.Users = new List<User>(authRepo.GetUsers());

                reply.Data = _serializer.Serialize(replyUL);

                _server.Send(sessionId, _serializer.Serialize(reply));
            }
            else if (requestUL.RequestType == "SingleUser")
            {
                User user = authRepo.GetUserFromLogin(requestUL.Parameters);
                NetworkReply reply = new NetworkReply();
                reply.RequestType = request.RequestType;


                ReplyUserList replyUL = new ReplyUserList();
                replyUL.RequestType = requestUL.RequestType;
                replyUL.Users = new List<User>();
                replyUL.SingleUser = user;
                reply.Data = _serializer.Serialize(replyUL);

                _server.Send(sessionId, _serializer.Serialize(reply));
            }*/
       // }
    }
}