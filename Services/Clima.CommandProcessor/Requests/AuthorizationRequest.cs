namespace Clima.CommandProcessor.Requests
{
    public class AuthorizationRequest
    {
        public AuthorizationRequest()
        {
            
        }
        public string Function { get; set; }
        public string Login { get; set; }
        public string PasswordHash { get; set; }
    }
}