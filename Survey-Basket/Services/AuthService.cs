using Microsoft.AspNetCore.Identity;
using Survey_Basket.Contracts.Authentication;
using Survey_Basket.Models;
using SurveyBasket.Authentication;
using System.Security.Cryptography;

namespace Survey_Basket.Services
{
    public class AuthService : IAuthService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IJwtProvider _jwtProvider;
        private readonly int _refreshTokenExpiryDays = 14; // Set the refresh token expiry days

        public AuthService(UserManager<ApplicationUser> userManager, IJwtProvider jwtProvider)
        {
            _userManager = userManager;
            _jwtProvider = jwtProvider;
        }

        public async Task<AuthResponse> GetTokenAsync(string email, string password, CancellationToken cancellationToken = default)
        {
            // check user
            var user = await _userManager.FindByEmailAsync(email);
            if (user is null)
            {
                return null;
            }
            //check password
            var isPasswordValid = await _userManager.CheckPasswordAsync(user, password);
            if (!isPasswordValid)
            {
                return null;
            }


            // generate token
            var (token, expiresIn) = _jwtProvider.GenerateToken(user);
            // generate refresh token
            var refreshToken = GenerateRefreshToken();
            var refreshTokenExpiration = DateTime.UtcNow.AddDays(_refreshTokenExpiryDays);

            user.RefreshTokens.Add(new RefreshToken
            {
                Token = refreshToken,
                ExpiresOn = refreshTokenExpiration
            });
            await _userManager.UpdateAsync(user);
            // return AuthResponse
            return new AuthResponse(user.Id, user.Email, user.FirstName, user.LastName,token,expiresIn*60, refreshToken, refreshTokenExpiration);
        }
        private static string GenerateRefreshToken()
        {
            return Convert.ToBase64String(RandomNumberGenerator.GetBytes(64)); // Generate a secure random refresh token
        }
        public async Task<AuthResponse?> GetRefreshTokenAsync(string token, string refreshToken, CancellationToken cancellationToken = default)
        {
            var userId = _jwtProvider.ValidationToken(token);
            if (userId is null)
                return null;

            var user = await _userManager.Users 
                .FirstOrDefaultAsync(u => u.Id == userId, cancellationToken);

            if (user is null)
                return null;

            var userRefreshToken = user.RefreshTokens.FirstOrDefault(rt => rt.Token == refreshToken && rt.IsActive);
            if (userRefreshToken is null)
                return null;

            // Revoke old refresh token
            userRefreshToken.RevokedOn = DateTime.UtcNow;

            // Generate new JWT token
            var (newToken, expiresIn) = _jwtProvider.GenerateToken(user);

            // Generate new refresh token
            var newRefreshToken = GenerateRefreshToken();
            var refreshTokenExpiration = DateTime.UtcNow.AddDays(_refreshTokenExpiryDays);

            user.RefreshTokens.Add(new RefreshToken
            {
                Token = newRefreshToken,
                ExpiresOn = refreshTokenExpiration
            });

            await _userManager.UpdateAsync(user);

            return new AuthResponse(user.Id, user.Email, user.FirstName, user.LastName, newToken, expiresIn * 60, newRefreshToken, refreshTokenExpiration);
        }

        public async Task<bool> RevokeRefreshTokenAsync(string token, string refreshToken, CancellationToken cancellationToken = default)
        {
            var userId = _jwtProvider.ValidationToken(token);

            if (userId is null)
                return false;

            var user = await _userManager.FindByIdAsync(userId);

            if (user is null)
                return false;

            var userRefreshToken = user.RefreshTokens.SingleOrDefault(x => x.Token == refreshToken && x.IsActive);

            if (userRefreshToken is null)
                return false;

            userRefreshToken.RevokedOn = DateTime.UtcNow;

            await _userManager.UpdateAsync(user);

            return true;
        }

    }
}