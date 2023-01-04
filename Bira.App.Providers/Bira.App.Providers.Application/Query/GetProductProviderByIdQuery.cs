using Bira.App.Providers.Domain.DTOs.Response;
using MediatR;

namespace Bira.App.Providers.Application.Query
{
    public class GetProductProviderByIdQuery : IRequest<Response>
    {
        public GetProductProviderByIdQuery(Guid id)
        {
            Id = id;
        }

        public Guid Id { get; set; }
    }
}
