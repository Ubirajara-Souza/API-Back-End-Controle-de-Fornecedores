using MediatR;
using Bira.App.Providers.Domain.DTOs.Request;
using Bira.App.Providers.Domain.DTOs.Response;

namespace Bira.App.Providers.Application.Command
{
    public class CreateProductCommand : IRequest<Response>
    {
        public CreateProductCommand(ProductDto productDto)
        {
            ProductDto = productDto;
        }

        public ProductDto ProductDto { get; set; }
    }
}
