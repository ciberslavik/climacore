using System.Collections.Generic;

namespace Clima.Services.Authorization
{
    public interface IAuthRepository
    {
        IList<User> GetUsers();
        User GetUserFromLogin(string login);
    }
}