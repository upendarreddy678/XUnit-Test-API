using Microsoft.EntityFrameworkCore;

namespace DataSrv.Entities
{
    public class UserDbContext : DbContext
    {
        public UserDbContext(){}
        public UserDbContext(DbContextOptions<UserDbContext> options) : base(options) { }

        public virtual DbSet<UserDetails> userDetails { get; set; }
    }
}
