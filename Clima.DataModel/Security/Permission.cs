namespace Clima.DataModel.Security
{
    public class Permission
    {
        public Permission()
        {
        }

        public int PermissionId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public PermissionFlags Flags { get; } = new PermissionFlags();
    }

    public class PermissionFlags
    {
        public bool Create { get; set; }
        public bool Delete { get; set; }
        public bool Modify { get; set; }
    }
}