using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MyGame.Domain.Entities;

namespace MyGame.Infra.Data.Configurations
{
    public class FriendConfiguration : IEntityTypeConfiguration<Friend>
    {
        public void Configure(EntityTypeBuilder<Friend> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Ignore(x => x.Notifications);

            builder.Property(c => c.Name).HasMaxLength(100)
                .IsRequired();

            builder.Property(c => c.Email).HasMaxLength(100)
                .IsRequired();

            builder.Property(c => c.Phone).HasMaxLength(30)
                .IsRequired();

            builder.HasMany(f => f.Loans)
                .WithOne(u => u.Friend)
                .HasForeignKey(x => x.FriendId);

        }
    }
}
