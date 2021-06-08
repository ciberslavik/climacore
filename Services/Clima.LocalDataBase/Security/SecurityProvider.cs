using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Clima.LocalDataBase.Security
{
    public class SecurityProvider:DbContext
    {
        public SecurityProvider()
        {
        }
        public DbSet<User> Users { get; set; }
        public DbSet<Group> Groups { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder options)
            => options.UseSqlite(@"Data Source=C:\blogging.db");
    }
}