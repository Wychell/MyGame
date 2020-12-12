using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MyGame.Domain.Entities;

namespace MyGame.Infra.Data.Configurations
{
    public class GameConfiguration : IEntityTypeConfiguration<Game>
    {
        public void Configure(EntityTypeBuilder<Game> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Ignore(x => x.Notifications);


            builder.Property(x => x.Name).HasMaxLength(100).IsRequired();
            builder.Property(x => x.Gender).HasMaxLength(50).IsRequired();
        }
    }
}
