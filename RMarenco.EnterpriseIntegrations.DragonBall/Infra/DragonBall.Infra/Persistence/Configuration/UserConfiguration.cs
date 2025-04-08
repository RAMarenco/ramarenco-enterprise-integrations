using DragonBall.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DragonBall.Infra.Persistence.Configuration
{
    class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.ToTable(nameof(User));

            builder.Property(u => u.Email)
                .HasColumnType("varchar(256)")
                .HasMaxLength(256)
                .IsRequired();
            builder.HasIndex(u => u.Email)
                .IsUnique();
            builder.Property(u => u.Password)
                .HasColumnType("varchar(256)")
                .HasMaxLength(256)
                .IsRequired();
            builder.Property(u => u.Salt)
                .HasColumnType("varchar(256)")
                .HasMaxLength(64)
                .IsRequired();
            builder.Property(u => u.AccessToken)
                .HasColumnType("varchar(2048)")
                .HasMaxLength(2048);
        }
    }
}
