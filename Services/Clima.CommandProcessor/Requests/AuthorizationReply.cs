namespace Clima.CommandProcessor.Requests
{
    public class AuthorizationReply
    {
        public AuthorizationReply()
        {}
        public string Login { get; set; }
        public bool AuthResult { get; set; }
    }
}