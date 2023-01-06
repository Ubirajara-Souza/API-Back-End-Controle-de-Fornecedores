using Bira.App.Providers.Domain.DTOs.Response;
using MediatR;

namespace Bira.App.Providers.Application.Command
{
    public class DeleteProductCommand : IRequest<Response>
    {
        public DeleteProductCommand(Guid id)
        {
            Id = id;
        }

        public Guid Id { get; set; }
    }
}

