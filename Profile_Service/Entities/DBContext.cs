using Microsoft.EntityFrameworkCore;
using Profile_Service.Models;


namespace Profile_Service.Entities
{
    public partial class DBContext : DbContext
    {
        public DbSet<User> Users { get; set; }

        public DBContext(DbContextOptions<DBContext> options) : base(options) { }
    }
}