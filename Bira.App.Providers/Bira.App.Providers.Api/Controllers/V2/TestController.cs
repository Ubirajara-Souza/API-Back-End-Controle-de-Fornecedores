using Bira.App.Providers.Domain.Interfaces;
using Bira.App.Providers.Service.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Bira.App.Providers.Api.Controllers.V2
{
    [ApiVersion("2.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class TestController : BaseController
    {
        private readonly ILogger _logger;
        public TestController(INotifier notifier, IUser user, ILogger<TestController> logger) : base(notifier, user)
        {
            _logger = logger;
        }

        [HttpGet]
        public string Value()
        {
            _logger.LogTrace("Log de Trace");
            _logger.LogDebug("Log de Debug");
            _logger.LogInformation("Log de Informação");
            _logger.LogWarning("Log de Aviso");
            _logger.LogError("Log de Erro");
            _logger.LogCritical("Log de Problema Critico");

            return "Sou a V2";
        }
    }
}
