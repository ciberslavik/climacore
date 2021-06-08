namespace Clima.DataModel.Security
{
    public class User
    {
        public User()
        {
        }

        public int UserId { get; set; }
        public string Login { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public byte[] PasswordHash { get; set; }
    }
}