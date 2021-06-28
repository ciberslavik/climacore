using System;
using System.Collections.Generic;
using Clima.Services.Authorization;

namespace Clima.Authorization
{
    public class DefaultAuthorizationService:IAuthorizationService
    {
        private readonly IAuthorizationRepository _repo;

        public DefaultAuthorizationService(IAuthorizationRepository repo)
        {
            _repo = repo;
        }
        public IList<User> GetUsers()
        {
            return _repo.GetUsers();
        }

        public User GetUserByLogin(string login)
        {
            return _repo.GetUserFromLogin(login);
        }

        public bool AuthorizeUser(User user)
        {
            if (!_repo.UserExist(user.Login))
                return false;
            
            User localUser = _repo.GetUserFromLogin(user.Login);
            
            
            return true;
        }
    }
}