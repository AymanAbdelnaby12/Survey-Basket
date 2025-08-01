﻿namespace Survey_Basket.Models
{
    public sealed class Answer
    {
        public int Id { get; set; }
        public string Content { get; set; } = string.Empty;
        public int QuestionId { get; set; }
        public bool IsActive { get; set; } = true;
        public Question Question { get; set; } = null!; 
        public ICollection<Answer> answers { get; set; } = new List<Answer>();
    }
}
