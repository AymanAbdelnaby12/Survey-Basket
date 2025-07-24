namespace Survey_Basket.Contracts.Questions
{
    public class QuestionReponse
    {
        public int Id { get; set; }
        public string Content { get; set; } 
        IEnumerable<Answer> Answers { get; set; }
    }
}
