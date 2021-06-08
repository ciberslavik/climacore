using System.Collections.Generic;

namespace Clima.DataModel.Security
{
    public interface ISecurityRepository
    {
        IList<User> GetUsers();
        IList<Group> GetGroups();
        IList<Role> GetRoles();
        IList<Permission> GetPermissions();
    }
}