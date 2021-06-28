using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using Clima.Services.Authorization;

namespace FakeAuthorizationRepository
{
    public class AuthorizationRepository:IAuthorizationRepository
    {
        private Dictionary<string, User> _users;
        
        public AuthorizationRepository()
        {
            _users = new Dictionary<string, User>();
            SHA256 mySHA256 = SHA256.Create();
            string pass = Encoding.UTF8.GetString(mySHA256.ComputeHash(Encoding.UTF8.GetBytes("123")));
            _users.Add("Admin", new User()
            {
                FirstName = "Ильин",
                LastName = "Вячеслав",
                PasswordHash = pass,
                Login = "Admin"
            });
            _users.Add("User", new User()
            {
                FirstName = "Vasiliy",
                LastName = "Pupkin",
                PasswordHash = "",
                Login = "User"
            });
        }
        public IList<User> GetUsers()
        {
            return _users.Values.ToList();
        }

        public User GetUserFromLogin(string login)
        {
            if (_users.ContainsKey(login))
            {
                return _users[login];
            }

            return null;
        }

        public bool UserExist(string login)
        {
            return _users.ContainsKey(login);
        }
    }
}