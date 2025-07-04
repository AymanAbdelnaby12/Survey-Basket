namespace Survey_Basket.Contracts.Poll;

public record PollRequest(
    string Title,
    string Description, 
    DateOnly StartsAt,
    DateOnly EndsAt
);