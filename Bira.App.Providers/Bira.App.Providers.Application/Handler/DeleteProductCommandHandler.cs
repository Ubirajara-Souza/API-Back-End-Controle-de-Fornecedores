using Bira.App.Providers.Application.Command;
using Bira.App.Providers.Domain.DTOs.Response;
using Bira.App.Providers.Domain.Interfaces.Repositories;
using MediatR;

namespace Bira.App.Providers.Application.Handler
{
    public class DeleteProductCommandHandler : IRequestHandler<DeleteProductCommand, Response>
    {
        private readonly IProductRepository _productRepository;

        public DeleteProductCommandHandler(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }
        public async Task<Response> Handle(DeleteProductCommand request, CancellationToken cancellationToken)
        {
            try
            {
                await _productRepository.Delete(request.Id);
                var response = new Response();
                response.Success = true;

                return response;
            }
            catch (Exception ex)
            {
                var response = new Response();

                response.Success = false;
                response.AddError(ex.Message);

                return response;
            }
        }
    }
}
