using Bira.App.Providers.Domain.DTOs.Request;
using Bira.App.Providers.Domain.Interfaces;
using Bira.App.Providers.Service.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Bira.App.Providers.Api.Controllers.V1
{
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class AuthController : BaseController
    {
        private readonly IAuthService _authService;
        private readonly ILogger _logger;
        public AuthController(IAuthService authService, ILogger<AuthController> logger,
            INotifier notifier, IUser user) : base(notifier, user)
        {
            _authService = authService;
            _logger = logger;
        }

        [HttpPost("new-account")]
        public async Task<ActionResult> Register(RegisterUserDto registerUser)
        {
            if (!ModelState.IsValid) return CustomResponse(ModelState);

            var result = await _authService.Register(registerUser);
            if (result.Succeeded)
            {
                return CustomResponse(await _authService.GenerateJwt(registerUser.Email));
            }
            foreach (var error in result.Errors)
            {
                NotifyError(error.Description);
            }

            return CustomResponse(registerUser);
        }

        [HttpPost("login")]
        public async Task<ActionResult> Login(LoginUserDto loginUser)
        {
            if (!ModelState.IsValid) return CustomResponse(ModelState);

            var result = await _authService.Login(loginUser);

            if (result.Succeeded)
            {
                _logger.LogInformation("Usuario " + loginUser.Email + " logado com sucesso");
                return CustomResponse(await _authService.GenerateJwt(loginUser.Email));
            }
            if (result.IsLockedOut)
            {
                NotifyError("Usuário temporariamente bloqueado por tentativas inválidas");
                return CustomResponse(loginUser);
            }

            NotifyError("Usuário ou Senha incorretos");
            return CustomResponse(loginUser);
        }
    }
}
