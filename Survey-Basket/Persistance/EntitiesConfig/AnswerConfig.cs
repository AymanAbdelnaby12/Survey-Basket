using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Survey_Basket.Persistance.EntitiesConfig
{
    public class AnswerConfig : IEntityTypeConfiguration<Answer>
    {
        public void Configure(EntityTypeBuilder<Answer> builder)
        {
            builder.HasIndex(x => new { x.QuestionId, x.Content }).IsUnique();
            builder.Property(x => x.Content)
                   .IsRequired()
                   .HasMaxLength(500);

        }
    }
}
