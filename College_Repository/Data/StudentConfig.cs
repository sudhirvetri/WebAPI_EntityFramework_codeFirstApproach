using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace College_Repository.Data
{
    public class StudentConfiguration : IEntityTypeConfiguration<Student>
    {
        public void Configure(EntityTypeBuilder<Student> builder)
        {
            builder.Property(n => n.Name).IsRequired();
            builder.Property(n => n.Name).HasMaxLength(250);
            builder.Property(n => n.Email).IsRequired().HasMaxLength(250);
            builder.Property(n => n.Phone).IsRequired(false).HasMaxLength(12);
            builder.Property(n => n.AdmissionDate).IsRequired();
            builder.Property(n => n.DateofBirth).IsRequired();
            builder.Property(n => n.Status).IsRequired(true).HasMaxLength(10).HasDefaultValue("Active");
        }
    }
}