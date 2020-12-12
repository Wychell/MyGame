using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MyGame.Domain.Entities;

namespace MyGame.Infra.Data.Configurations
{
    public class LoaConfiguration : IEntityTypeConfiguration<Loan>
    {
        public void Configure(EntityTypeBuilder<Loan> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Ignore(x => x.Notifications);
            builder.Property(x => x.EndDate);

            builder.HasOne(l => l.Friend)
                   .WithMany(f => f.Loans)
                   .HasForeignKey(l => l.FriendId);

            builder.HasOne(l => l.Game)
                .WithMany(g => g.Loans)
                .HasForeignKey(l => l.GameId);
        }
    }
    
}
