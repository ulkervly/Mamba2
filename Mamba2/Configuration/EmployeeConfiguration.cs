using Mamba2.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Mamba2.Configuration
{
    public class EmployeeConfiguration:IEntityTypeConfiguration<Employee>
    {
        public void Configure(EntityTypeBuilder<Employee> builder)
        {
            builder.Property(x => x.FullName)
                    .IsRequired()
                    .HasMaxLength(90);
            builder.Property(x => x.Position)
                 .IsRequired()
                 .HasMaxLength(70);
            builder.Property(x => x.Desc)
                .IsRequired()
                .HasMaxLength(200);
            builder.Property(x=>x.MediaLink)
                .IsRequired()
                .HasMaxLength (400);
            builder.Property(x => x.EmployeeImageUrl).IsRequired();
           
        }
    }
}
