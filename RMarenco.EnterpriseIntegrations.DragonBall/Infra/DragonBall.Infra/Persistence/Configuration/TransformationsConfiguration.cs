using DragonBall.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DragonBall.Infra.Persistance.Configuration
{
    class TransformationsConfiguration : IEntityTypeConfiguration<Transformation>
    {
        public void Configure(EntityTypeBuilder<Transformation> builder)
        {
            builder.ToTable(nameof(Transformation));

            builder.Property(t => t.Name)
                .HasColumnType("varchar(50)")
                .HasMaxLength(100)
                .IsRequired();
            builder.Property(t => t.Ki)
                .HasColumnType("varchar(100)")
                .HasMaxLength(100)
                .IsRequired();
        }
    }
}
