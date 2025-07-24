using Survey_Basket.Contracts.Poll;
using Survey_Basket.Models;
using SurveyBasket.Abstractions;

namespace SurveyBasket.Services;

public interface IPollService
{
    Task<IEnumerable<Poll>> GetAllAsync();
    Task<Result<PollResponse>> GetAsync(int id, CancellationToken cancellationToken = default);
    Task<Result<PollResponse>> AddAsync(PollRequest poll, CancellationToken cancellationToken = default);
    Task<Result> UpdateAsync(int id, PollRequest pollRequest, CancellationToken cancellationToken = default);
    Task<Result> DeleteAsync(int id, CancellationToken cancellationToken = default);
    Task<Result> TogglePublishStatusAsync(int id, CancellationToken cancellationToken = default);
}
