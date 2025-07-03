using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Survey_Basket.Models;

namespace Survey_Basket.Persistance.EntitiesConfig
{
    public class PollConfig : IEntityTypeConfiguration<Models.Poll>
    {
        public void Configure(EntityTypeBuilder<Poll> builder)
        {
            builder.HasIndex(p => p.Title)
                   .IsUnique();
            builder.Property(p => p.Title)
                   .IsRequired()
                   .HasMaxLength(100);
            builder.Property(p => p.Description)
                    .HasMaxLength(500);  
        }
    }
}
