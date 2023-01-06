using Bira.App.Providers.Domain.DTOs.Request;
using Bira.App.Providers.Domain.DTOs.Response;
using MediatR;

namespace Bira.App.Providers.Application.Command
{
    public class UpdateProductCommand : IRequest<Response>
    {
        public UpdateProductCommand(ProductDto productDto)
        {
            ProductDto = productDto;
        }

        public ProductDto ProductDto { get; set; }
    }
}


