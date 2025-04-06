using DragonBall.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DragonBall.Infra.Persistence.Configuration
{
    class CharacterConfiguration : IEntityTypeConfiguration<Character>
    {
        public void Configure(EntityTypeBuilder<Character> builder)
        {
            builder.ToTable(nameof(Character));

            builder.HasMany(c => c.Transformation)
                .WithOne()
                .HasForeignKey(t => t.CharacterId)
                .OnDelete(DeleteBehavior.NoAction);

            builder.Property(c => c.Name)
                .HasColumnType("varchar(50)")
                .HasMaxLength(50)
                .IsRequired();
            builder.Property(c => c.Ki)
                .HasColumnType("varchar(100)")
                .HasMaxLength(100)
                .IsRequired();
            builder.Property(c => c.Race)
                .HasColumnType("varchar(50)")
                .HasMaxLength(50)
                .IsRequired();
            builder.Property(c => c.Gender)
                .HasColumnType("varchar(10)")
                .HasMaxLength(10)
                .IsRequired();
            builder.Property(c => c.Description)
                .HasColumnType("nvarchar(Max)")
                .IsRequired();
            builder.Property(c => c.Affiliation)
                .HasColumnType("varchar(50)")
                .HasMaxLength(50)
                .IsRequired();
        }
    }
}
