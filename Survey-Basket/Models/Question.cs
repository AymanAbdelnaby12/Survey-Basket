﻿using System.Security.Cryptography.X509Certificates;

namespace Survey_Basket.Models
{
    public sealed class Question:AuditableEntity
    {
        public int Id { get; set; }
        public string Content { get; set; } = string.Empty;
        public bool IsActive { get; set; } = true;

        public int PollId { get; set; }
        public Poll Poll { get; set; } 

        public ICollection<Answer> Answers { get; set; } = new List<Answer>();
    }
}
