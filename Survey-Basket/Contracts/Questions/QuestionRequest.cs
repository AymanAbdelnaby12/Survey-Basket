namespace Survey_Basket.Contracts.Questions
{
    public class QuestionRequest
    {
        public string Content { get; set; } = string.Empty;
        public ICollection<Answer> Answers { get; set; } = new List<Answer>();
    }
}
