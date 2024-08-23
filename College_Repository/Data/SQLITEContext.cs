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
            modelBuilder.Entity<Student>(entity =>
            {
                entity.Property(n => n.Name).IsRequired();
                entity.Property(n => n.Name).HasMaxLength(250);
                entity.Property(n => n.Email).IsRequired().HasMaxLength(250);
                entity.Property(n => n.Phone).IsRequired(false).HasMaxLength(12);
                entity.Property(n => n.AdmissionDate).IsRequired();
                entity.Property(n => n.DateofBirth).IsRequired();
            });
        }
    }


}
