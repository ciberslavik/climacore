namespace Clima.LocalDataBase.Security
{
    public class User
    {
        public User()
        {
            UserId = 0;
            UserLogin = "";
        }

        public User(string userLogin)
        {
            UserId = 0;
            UserLogin = userLogin;
        }
        public int UserId { get; set; }
        public string UserLogin { get; set; }
        
    }
}