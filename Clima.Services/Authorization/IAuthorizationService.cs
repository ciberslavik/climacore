using System.Collections.Generic;

namespace Clima.Services.Authorization
{
    public interface IAuthorizationService
    {
        IList<User> GetUsers();
        User GetUserByLogin(string login);
        bool AuthorizeUser(User user);
    }
}