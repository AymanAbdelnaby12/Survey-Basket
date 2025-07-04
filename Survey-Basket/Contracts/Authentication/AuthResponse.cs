namespace Survey_Basket.Contracts.Authentication
{
    public class AuthResponse(
        string Id,
        string Email,
        string FirstName,
        string LastName,
        string Token,
        int Expiration ,
        string RefreshToken,
        DateTime RefreshTokenExpiration
    );
    
}
