using SurveyBasket.Abstractions;

namespace SurveyBasket.Errors;

public static class UserErrors
{
    public static readonly Error InvalidCredentials = 
        new("User.InvalidCredentials", "Invalid email/password");

    public static readonly Error InvalidJwtToken =
        new("User.InvalidJwtToken", "Invalid Jwt token");

    public static readonly Error InvalidRefreshToken =
        new("User.InvalidRefreshToken", "Invalid refresh token"); 

    public static readonly Error DuplicateEmail =
        new("User.DuplicateEmail", "Email already exists"); 

    public static Error UnableToRegister(string message) =>
        new("User.UnableToRegister", $"Unable to register user: {message}");
}