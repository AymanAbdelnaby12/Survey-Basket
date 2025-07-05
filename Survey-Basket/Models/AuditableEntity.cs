namespace Survey_Basket.Models
{
    public class AuditableEntity
    {

        public string CreatedById { get; set; } = string.Empty;
        public DateTime CreatedOn { get; set; } = DateTime.UtcNow;
        public string? UpdatedById { get; set; } = null;
        public DateTime? UpdatedOn { get; set; } = null;

        public ApplicationUser CreatedBy { get; set; }
        public ApplicationUser? UpdatedBy { get; set; }
    }
}
