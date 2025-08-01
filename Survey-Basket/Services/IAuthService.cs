﻿using Survey_Basket.Contracts.Authentication;
using SurveyBasket.Abstractions;

namespace Survey_Basket.Services
{
    public interface IAuthService
    {
        Task<Result<AuthResponse>> GetTokenAsync(string email, string password, CancellationToken cancellationToken = default); 
        Task<Result<AuthResponse>> GetRefreshTokenAsync(string token,string refreshToken, CancellationToken cancellationToken = default);
        Task<Result> RevokeRefreshTokenAsync(string token, string refreshToken, CancellationToken cancellationToken = default);
    }
}
