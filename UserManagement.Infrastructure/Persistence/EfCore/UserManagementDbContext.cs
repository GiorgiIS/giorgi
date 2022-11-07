using System.Reflection;
using Microsoft.EntityFrameworkCore;
using UserManagement.Domain;

namespace UserManagement.Infrastructure.Persistence.EfCore
{
    public class UserManagementDbContext : DbContext
    {
        public virtual  DbSet<User> Users { get; set; }


        public UserManagementDbContext()
        {
        }

        public UserManagementDbContext(DbContextOptions<UserManagementDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

            modelBuilder.Entity<User>()
                .ToTable("User", "usermanagement");
        }
    }
}
