using Microsoft.EntityFrameworkCore;
using Survey_Basket.Models;
using Survey_Basket.Persistance; 

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

    public async Task<Poll?> GetAsync(int id, CancellationToken cancellationToken = default) => 
        await _context.Polls.FindAsync(id, cancellationToken);

    public async Task<Poll> AddAsync(Poll poll, CancellationToken cancellationToken = default)
    {
        await _context.AddAsync(poll, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);

        return poll;
    }

    public async Task<bool> UpdateAsync(int id, Poll poll, CancellationToken cancellationToken = default)
    {
        var currentPoll = await GetAsync(id, cancellationToken);

        if (currentPoll is null)
            return false;

        currentPoll.Title = poll.Title;
        currentPoll.Description = poll.Description;
        currentPoll.StartsAt = poll.StartsAt;
        currentPoll.EndsAt = poll.EndsAt;

        await _context.SaveChangesAsync(cancellationToken);

        return true;
    }

    public async Task<bool> DeleteAsync(int id, CancellationToken cancellationToken = default)
    {
        var poll = await GetAsync(id, cancellationToken);

        if (poll is null)
            return false;

        _context.Remove(poll);

        await _context.SaveChangesAsync(cancellationToken);

        return true;
    }

    public async Task<bool> TogglePublishStatusAsync(int id, CancellationToken cancellationToken = default)
    {
        var poll = await GetAsync(id, cancellationToken);

        if (poll is null)
            return false;

        poll.IsPublished = !poll.IsPublished;

        await _context.SaveChangesAsync(cancellationToken);

        return true;
    }
}