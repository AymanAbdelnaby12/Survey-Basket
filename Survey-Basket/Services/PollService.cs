using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic;
using Survey_Basket.Contracts.Poll;
using Survey_Basket.Models;
using Survey_Basket.Persistance;
using SurveyBasket.Abstractions;
using SurveyBasket.Errors;

namespace SurveyBasket.Services;

public class PollService : IPollService
{
    private readonly AppDbContext _context;

    public PollService(AppDbContext context)
    {
        _context = context; 
    }

    public async Task<IEnumerable<Poll>> GetAllAsync() =>
        await _context.Polls.AsNoTracking().ToListAsync();

    public async Task<Result<PollResponse>> GetAsync(int id, CancellationToken cancellationToken = default)
    {
        var poll = await _context.Polls.FindAsync(id, cancellationToken);
        return poll is null
            ? Result.Failure<PollResponse>(PollErrors.PollNotFound)
            : Result.Success(poll.Adapt<PollResponse>());
    }

    public async Task<Result<PollResponse>> AddAsync(PollRequest pollRequest, CancellationToken cancellationToken = default)
    {
           var isExistingTitle = await _context.Polls.AnyAsync(p => p.Title == pollRequest.Title, cancellationToken);
        if (isExistingTitle)
         return   Result.Failure<PollResponse>(PollErrors.DuplicatedPollTitle);
        var poll = pollRequest.Adapt<Poll>();

        await _context.AddAsync(poll, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);

        return Result.Success(poll.Adapt<PollResponse>());
    }

    public async Task<Result> UpdateAsync(int id, PollRequest pollRequest, CancellationToken cancellationToken = default)
    {
        var isExistingTitle = await _context.Polls.AnyAsync(p => p.Title == pollRequest.Title, cancellationToken);
        if (isExistingTitle)
            return Result.Failure<PollResponse>(PollErrors.DuplicatedPollTitle);
        var currentPoll = await _context.Polls.FindAsync(id, cancellationToken);
        if (currentPoll is null)
            return Result.Failure(PollErrors.PollNotFound);
        currentPoll.Title = pollRequest.Title;
        currentPoll.Description= pollRequest.Description;
        currentPoll.StartsAt = pollRequest.StartsAt;
        currentPoll.EndsAt = pollRequest.EndsAt;

        await _context.SaveChangesAsync(cancellationToken);

        return Result.Success(); 
    }

    public async Task<Result> DeleteAsync(int id, CancellationToken cancellationToken = default)
    {
        var poll = await _context.Polls.FindAsync(id, cancellationToken);

        if (poll is null)
            return Result.Failure(PollErrors.PollNotFound);

        _context.Remove(poll);
        await _context.SaveChangesAsync(cancellationToken);
        return Result.Success();
    }

    public async Task<Result> TogglePublishStatusAsync(int id, CancellationToken cancellationToken = default)
    {
        var poll = await _context.Polls.FindAsync(id, cancellationToken);

        if (poll is null)
            return Result.Failure(PollErrors.PollNotFound);

        poll.IsPublished = !poll.IsPublished;
        await _context.SaveChangesAsync(cancellationToken);
        return Result.Success();
    }
}
