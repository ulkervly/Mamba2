using Mamba2.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Mamba2.Configuration
{
    public class EmployeePositionConfiguration:IEntityTypeConfiguration<EmployeePosition>
    {
        public void Configure(EntityTypeBuilder<EmployeePosition> builder)
        {
            builder.HasOne(ep=>ep.Employee).WithMany(ep=>ep.EmployeePositions).HasForeignKey(ep=>ep.EmployeeId).OnDelete(DeleteBehavior.NoAction);
            builder.HasOne(ep => ep.Position).WithMany(ep => ep.EmployeePositions).HasForeignKey(ep => ep.PositionId).OnDelete(DeleteBehavior.NoAction);
        }
    }
}
