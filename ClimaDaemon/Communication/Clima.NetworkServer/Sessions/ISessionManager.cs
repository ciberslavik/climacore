using System.Security.Principal;

namespace Clima.NetworkServer.Sessions
{
    public interface ISessionManager
    {
        Session CreateSession(IIdentity currentUser);

        Session TryGetSession(string sessionId);

        void RenewSession(string sessionId);

        void DeleteSession(string sessionId);
    }
}