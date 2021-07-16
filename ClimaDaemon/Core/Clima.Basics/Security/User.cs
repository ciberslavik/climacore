#nullable enable
using System.Security.Principal;

namespace Clima.Basics.Security
{
    public class User:IIdentity
    {
        private readonly string? _authenticationType;
        private readonly bool _isAuthenticated;

        public User(string userName)
        {
            _authenticationType = "";
            _isAuthenticated = false;
            Name = userName;
        }


        public string? AuthenticationType => _authenticationType;

        public bool IsAuthenticated => _isAuthenticated;

        public string? Name { get; }
    }
}