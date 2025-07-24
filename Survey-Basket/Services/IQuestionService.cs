using Survey_Basket.Contracts.Questions;
using SurveyBasket.Abstractions;

namespace Survey_Basket.Services
{
    public interface IQuestionService
    {
        Task <Result<IEnumerable<QuestionReponse>>> GetAllAsync(int pollId, CancellationToken cancellationToken = default);
        Task <Result<QuestionReponse>> GetAsync(int pollId, int id, CancellationToken cancellationToken = default);
        Task<Result<QuestionReponse>> AddAsync(int pollId, QuestionRequest request, CancellationToken cancellationToken = default);
        Task<Result> UpdateAsync(int pollId, int id, QuestionRequest request, CancellationToken cancellationToken = default);
        Task<Result> ToggleStatusAsync(int pollId, int id, CancellationToken cancellationToken = default);

    }
}
