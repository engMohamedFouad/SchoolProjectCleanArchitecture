using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SchoolProject.Data.Entities;

namespace SchoolProject.Infrustructure.Configurations
{
    public class DepartmentConfigurations : IEntityTypeConfiguration<Department>
    {
        public void Configure(EntityTypeBuilder<Department> builder)
        {

            builder.HasKey(x => x.DID);
            builder.Property(x => x.DNameAr).HasMaxLength(500);

            builder.HasMany(x => x.Students)
                  .WithOne(x => x.Department)
                  .HasForeignKey(x => x.DID)
                  .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(x => x.Instructor)
            .WithOne(x => x.departmentManager)
            .HasForeignKey<Department>(x => x.InsManager)
            .OnDelete(DeleteBehavior.Restrict);


        }
    }
}
