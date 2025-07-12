using Survey_Basket.Contracts.Poll;

namespace SurveyBasket.Mapping;

public class MappingConfigurations : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        TypeAdapterConfig<PollRequest, Poll>
                .NewConfig()
                .Ignore(dest => dest.Id);
    }
}