namespace Survey_Basket.Contracts.Questions
{
    public class QuestionRequestValidator:AbstractValidator<QuestionRequest>
    {
        public QuestionRequestValidator()
        {
            RuleFor(q => q.Content).NotEmpty().WithMessage("Content is required.").Length(3,100);
            RuleFor(x=>x.Answers).NotNull().WithMessage("Answers are required.");
            RuleFor(q => q.Answers).Must(answers => answers != null && answers.Count > 1)
                .WithMessage("At least two answer is required.")
                .When(x=>x.Answers != null);
            RuleFor(x => x.Answers)
                .Must(x => x.Distinct().Count() == 0).WithMessage("you cannot add duplicated answer for the same question")
                .When(x => x.Answers != null);
        }
    } 
}
