using Bira.App.Providers.Domain.DTOs.Request;
using Bira.App.Providers.Domain.DTOs.Response;
using Microsoft.AspNetCore.Identity;

namespace Bira.App.Providers.Service.Interfaces
{
    public interface IAuthService
    {
        Task<LoginResponse> GenerateJwt(string email);
        Task<SignInResult> Login(LoginUserDto loginUser);
        Task<IdentityResult> Register(RegisterUserDto registerUser);
    }
}

