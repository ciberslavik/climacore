namespace Clima.Basics.Security
{
    public interface IAuthorizationProvider
    {
        User GetUser(string userName);
        bool Authorize(string userName, string passwordHash);
    }
}