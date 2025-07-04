using Microsoft.AspNetCore.Http; 
using Microsoft.AspNetCore.Mvc;
using Survey_Basket.Contracts.Authentication;
using Survey_Basket.Services;
using SurveyBasket.Contracts.Authentication;

namespace Survey_Basket.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        } 
        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginRequest request, CancellationToken cancellationToken)
        {
            var result = await _authService.GetTokenAsync(request.Email, request.Password, cancellationToken);

            if (result == null)
            {
                return Unauthorized("Invalid email or password");
            }

            return Ok(result);
        }
        [HttpPost("refresh")]
        public async Task<IActionResult> RefreshAsync([FromBody] RefreshTokenRequest request, CancellationToken cancellationToken)
        {
            var authResult = await _authService.GetRefreshTokenAsync(request.Token, request.RefreshToken, cancellationToken);

            return authResult is null ? BadRequest("Invalid token") : Ok(authResult);
        }

        [HttpPost("revoke-refresh-token")]
        public async Task<IActionResult> RevokeRefreshTokenAsync([FromBody] RefreshTokenRequest request, CancellationToken cancellationToken)
        {
            var isRevoked = await _authService.RevokeRefreshTokenAsync(request.Token, request.RefreshToken, cancellationToken);

            return isRevoked ? Ok() : BadRequest("Operation failed");
        }

    }
}
