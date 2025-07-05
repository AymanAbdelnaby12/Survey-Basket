using System.ComponentModel.DataAnnotations;

namespace Survey_Basket.Models
{
    public class Poll: AuditableEntity
    {
        public int Id { get; set; } 
        public string Title { get; set; }
        public string Description { get; set; }
        public DateOnly StartsAt { get; set; }
        public DateOnly EndsAt { get; set; }
        public bool IsPublished { get; set; }   


    }
}
