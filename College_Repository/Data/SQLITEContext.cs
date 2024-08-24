using Microsoft.EntityFrameworkCore;

namespace College_Repository.Data
{
    public class SQLITEContext : DbContext
    {
        public SQLITEContext(DbContextOptions<SQLITEContext> options) : base(options)
        {

        }
        public DbSet<Student> Student { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new StudentConfiguration());
            
        }
    }


}
