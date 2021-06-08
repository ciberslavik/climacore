using System.Collections.Generic;

namespace Clima.DataModel.Security
{
    public class Group
    {
        public Group()
        {
        }
        public int GroupId { get; set; }
        public string Name { get; set; }
        public List<User> Users { get; } = new List<User>();
        public List<Role> Roles { get; } = new List<Role>();

    }
}