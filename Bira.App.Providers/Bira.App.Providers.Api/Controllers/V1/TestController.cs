using Bira.App.Providers.Domain.Interfaces;
using Bira.App.Providers.Service.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Bira.App.Providers.Api.Controllers.V1
{
    [ApiVersion("1.0", Deprecated = true)]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class TestController : BaseController
    {
        public TestController(INotifier notifier, IUser user) : base(notifier, user)
        { }

        [HttpGet]
        public string Value()
        {
            return "Sou a V1";
        }
    }
}
