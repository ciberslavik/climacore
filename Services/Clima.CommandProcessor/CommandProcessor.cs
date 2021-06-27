using System;
using System.Collections.Generic;
using Clima.CommandProcessor.Requests;
using Clima.Services.Authorization;
using Clima.Services.Communication;
using IServiceProvider = Clima.Services.IServiceProvider;

namespace Clima.CommandProcessor
{
    public class CommandProcessor:ICommandProcessor
    {
        private readonly IServiceProvider _container;
        private readonly ICommunicationSerializer _serializer;
        private readonly IServer _server;
        public CommandProcessor(IServiceProvider container)
        {
            _container = container;
            _serializer = _container.Resolve<ICommunicationSerializer>();
            _server = _container.Resolve<IServer>();
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
                Console.WriteLine($"Request type:{request.RequestType}");
                if (request.RequestType == "RequestUserList")
                {
                    RequestUserList requestUL;
                    var authRepo = _container.Resolve<IAuthRepository>();
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
                    }
                }
            }
        }
    }
}