using System.Collections.Generic;
using Clima.Services.Authorization;

namespace Clima.CommandProcessor.Requests
{
    public class ReplyUserList
    {
        public ReplyUserList()
        {
        }
        public string RequestType { get; set; }

        public List<User> Users{ get; set; }
        public User SingleUser { get; set; }
    }
}