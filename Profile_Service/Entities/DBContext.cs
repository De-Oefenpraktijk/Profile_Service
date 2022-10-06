using Microsoft.EntityFrameworkCore;
using Profile_Service.Models;


namespace Profile_Service.Entities
{
    public partial class DBContext : DbContext
    {
        public DbSet<User> Users { get; set; }

        public DbSet<Institutions> Institutions { get; set; }

        public DbSet<Themes> Themes { get; set; }

        public DBContext(DbContextOptions<DBContext> options) : base(options) { }
    }
}