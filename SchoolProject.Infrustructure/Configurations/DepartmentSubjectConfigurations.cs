using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SchoolProject.Data.Entities;

namespace SchoolProject.Infrustructure.Configurations
{
    public class DepartmentSubjectConfigurations : IEntityTypeConfiguration<DepartmetSubject>
    {
        public void Configure(EntityTypeBuilder<DepartmetSubject> builder)
        {
            builder.HasKey(x => new { x.SubID, x.DID });

            builder.HasOne(ds => ds.Department)
                 .WithMany(d => d.DepartmentSubjects)
                 .HasForeignKey(ds => ds.DID);

            builder.HasOne(ds => ds.Subject)
                 .WithMany(d => d.DepartmetsSubjects)
                 .HasForeignKey(ds => ds.SubID);


        }
    }
}
