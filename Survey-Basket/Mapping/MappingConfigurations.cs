using Survey_Basket.Contracts.Poll;
using Survey_Basket.Contracts.Questions;

namespace SurveyBasket.Mapping;

public class MappingConfigurations : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        TypeAdapterConfig<PollRequest, Poll>
                .NewConfig()
                .Ignore(dest => dest.Id);
        config.NewConfig<QuestionRequest, Question>()
            .Map(dest => dest.Answers, src => src.Answers.Select(a => new Answer { Content = a.Content }));
    }
}