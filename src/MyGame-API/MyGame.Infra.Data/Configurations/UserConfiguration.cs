using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MyGame.Domain.Entities;

namespace MyGame.Infra.Data.Configurations
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Ignore(x => x.Notifications);

            builder.Property(x => x.Name).HasMaxLength(100).IsRequired();
            builder.Property(x => x.Email).HasMaxLength(50).IsRequired();

            builder
                .HasMany(x => x.Friends)
                .WithOne(x => x.User)
                .HasForeignKey(x => x.UserId);
        }
    }
}
