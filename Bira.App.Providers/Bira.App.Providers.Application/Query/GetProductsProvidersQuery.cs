using Bira.App.Providers.Domain.DTOs.Response;
using MediatR;

namespace Bira.App.Providers.Application.Query
{
    public class GetProductsProvidersQuery : IRequest<Response>
    {
        public GetProductsProvidersQuery() { }

    }
}
