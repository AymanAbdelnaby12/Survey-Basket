namespace Survey_Basket.Contracts.Poll;

public record PollRequest(
    string Title,
    string Description,
    bool IsPublished,
    DateOnly StartsAt,
    DateOnly EndsAt
);