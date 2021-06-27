namespace Clima.CommandProcessor.Requests
{
    public class AuthorizationRequest
    {
        public AuthorizationRequest()
        {
            
        }
        public string Login { get; set; }
        public string PasswordHash { get; set; }
    }
}