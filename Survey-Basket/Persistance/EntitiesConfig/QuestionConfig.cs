using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Survey_Basket.Persistance.EntitiesConfig
{
    public class QuestionConfig : IEntityTypeConfiguration<Question>
    {
        public void Configure(EntityTypeBuilder<Question> builder)
        {
            builder.HasIndex(q => new { q.PollId, q.Content }).IsUnique();
            builder.Property(q => q.Content)
                   .IsRequired()
                   .HasMaxLength(500);
        }
    }
}
