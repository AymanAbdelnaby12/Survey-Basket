using SurveyBasket.Abstractions;

namespace SurveyBasket.Errors;

public static class QuestionErrors
{
    public static readonly Error QuestionNotFound =
        new("Poll.NotFound", "No Question was found with the given ID");

    public static readonly Error DuplicatedQuestionContent =
        new("Question.DuplicatedContent", "Another Question with the same content is already exists");
}