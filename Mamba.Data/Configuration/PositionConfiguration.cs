using Mamba2.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Mamba2.Configuration
{
    public class PositionConfiguration:IEntityTypeConfiguration<Position>
    {
        public void Configure(EntityTypeBuilder<Position> builder)
        {
            builder.Property(position => position.Name)
                .IsRequired()
                .HasMaxLength(50);
        }
    }
}
