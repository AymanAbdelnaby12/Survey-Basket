namespace Survey_Basket.Contracts.Answers
{
    public class AnswersResponse
    {
        public int Id { get; set; }
        public string Content { get; set; }
        public IEnumerable<AnswersResponse> Answers { get; set; }
    }
}
