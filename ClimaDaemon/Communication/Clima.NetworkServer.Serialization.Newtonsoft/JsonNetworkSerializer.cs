using System;
using System.IO;
using Clima.Basics.Services.Communication;
using Clima.Basics.Services.Communication.Messages;
using Clima.NetworkServer.Exceptions;
using Clima.NetworkServer.Serialization.Newtonsoft.Internal;
using Clima.NetworkServer.Services;
using Newtonsoft.Json;
using JsonReaderException = Newtonsoft.Json.JsonReaderException;

namespace Clima.NetworkServer.Serialization.Newtonsoft
{
    public class JsonNetworkSerializer:INetworkSerializer
    {
        private JsonSerializer JsonSerializer { get; set; } = JsonSerializer.Create();
        
        public string Serialize(IMessage message)
        {
            using (var sw = new StringWriter())
            {
                JsonSerializer.Serialize(sw, message);
                return sw.ToString();
            }  
        }

        public IMessage Deserialize(string data, IMessageTypeProvider typeProvider, IMessageNameProvider nameProvider)
        {
            using (var sr = new StringReader(data ?? string.Empty))
            {
                var preview = default(GenericMessage);
                try
                {
                    preview = (GenericMessage)JsonSerializer.Deserialize(sr, typeof(GenericMessage));
                }
                catch (JsonReaderException)
                {
                    
                }
                if (preview == null || !preview.IsValid)
                {
                    throw new InvalidRequestException(data)
                    {
                        MessageId = preview?.Id,
                    };
                }

                var serviceName = preview.Name;
                var methodName = preview.Method;
                var isRequest = serviceName != null;
                if (serviceName == null)
                {
                    // server cannot handle a response message
                    if (nameProvider == null)
                    {
                        throw new InvalidRequestException(data)
                        {
                            MessageId = preview.Id,
                        };
                    }

                    // invalid request id
                    serviceName = nameProvider.TryGetMessageName(preview.Id);
                    if (serviceName == null)
                    {
                        throw new InvalidRequestException(serviceName)
                        {
                            MessageId = preview.Id,
                        };
                    }
                }
                try
                {
                    // deserialize request or response message
                    if (isRequest)
                    {
                        return DeserializeRequest(data, serviceName, methodName, preview.Id, typeProvider);
                    }

                    return DeserializeResponse(data, serviceName, methodName, preview.Id, preview.Error, typeProvider);
                }
                catch (JsonServicesException ex)
                {
                    // make sure MessageId is reported
                    if (ex.MessageId == null)
                    {
                        ex.MessageId = preview.Id;
                    }

                    throw;
                }
                catch (Exception ex)
                {
                    throw new InvalidRequestException(data, ex)
                    {
                        MessageId = preview.Id,
                    };
                }
            }
        }
        
        private RequestMessage DeserializeRequest(string data, string serviceName, string methodName, string id, IMessageTypeProvider typeProvider)
        {
            using (var sr = new StringReader(data))
            {
                // get the message request type
                var type = typeProvider.GetRequestType(serviceName, methodName);
                var msgType = typeof(RequestMsg<>).MakeGenericType(new[] { type });

                // deserialize the strong-typed message
                var reqMsg = (IRequestMessage)JsonSerializer.Deserialize(sr, msgType);
                return new RequestMessage
                {
                    Service = serviceName,
                    Method = methodName,
                    Parameters = reqMsg.Parameters,
                    Id = id,
                };
            }
        }

        public ResponseMessage DeserializeResponse(string data, string serviceName, string methodName, string id, Error error, IMessageTypeProvider typeProvider)
        {
            using (var sr = new StringReader(data))
            {
                // pre-deserialize to get the bulk of the message
                var type = typeProvider.GetResponseType(serviceName, methodName);

                // handle void messages
                if (type == typeof(void))
                {
                    return ResponseMessage.Create(null, error, id);
                }

                // deserialize the strong-typed message
                var msgType = typeof(ResponseMsg<>).MakeGenericType(new[] { type });
                var respMsg = (IResponseMessage)JsonSerializer.Deserialize(sr, msgType);
                return ResponseMessage.Create(respMsg.Result, error, id);
            }
        }
    }
}