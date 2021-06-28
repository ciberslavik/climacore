using System.Collections.Generic;

namespace Clima.Services.Authorization
{
    public interface IAuthorizationRepository
    {
        IList<User> GetUsers();
        User GetUserFromLogin(string login);
        bool UserExist(string login);
    }
}