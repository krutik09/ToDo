using AngularAuthAPI.Models;
using Microsoft.EntityFrameworkCore;
namespace AngularAuthAPI.Data
{
    public class DBC: DbContext
    {
        public DBC(DbContextOptions<DBC> options):base(options) { }
        public DbSet<User> Users { get; set; }
        public DbSet<Todo> Todos { get; set; }

    }
}
