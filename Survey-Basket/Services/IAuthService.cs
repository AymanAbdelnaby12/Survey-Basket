using Survey_Basket.Contracts.Authentication;

namespace Survey_Basket.Services
{
    public interface IAuthService
    {
        Task<AuthResponse>GetTokenAsync(string email, string password, CancellationToken cancellationToken = default);
    }
}
