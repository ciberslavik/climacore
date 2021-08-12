using System.Security.Principal;

namespace Clima.NetworkServer.Sessions
{
    public class SessionManagerDefault : ISessionManager
    {
        public SessionManagerDefault()
        {
        }


        public Session CreateSession(IIdentity currentUser)
        {
            throw new System.NotImplementedException();
        }

        public Session TryGetSession(string sessionId)
        {
            throw new System.NotImplementedException();
        }

        public void RenewSession(string sessionId)
        {
            throw new System.NotImplementedException();
        }

        public void DeleteSession(string sessionId)
        {
            throw new System.NotImplementedException();
        }
    }
}