namespace SurveyBasket.Contracts.Requests;

public record PollRequest(
    string Title,
    string Description,
    bool IsPublished,
    DateOnly StartsAt,
    DateOnly EndsAt
);