using System;

namespace DataContract.NetworkModel
{
    public class Message
    {
        public Message(string name = "", string data = "")
        {
            
        }
        
        public Guid SessionId { get; set; }
        public MessageType MessageType { get; set; }
    }
}