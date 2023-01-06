using Bira.App.Providers.Domain.DTOs.Response;
using Bira.App.Providers.Domain.Interfaces.Repositories;
using MediatR;

namespace Bira.App.Providers.Application.Query.QueryHandler
{
    public class GetProductsProvidersQueryHandler : IRequestHandler<GetProductsProvidersQuery, Response>
    {
        private readonly IProductRepository _productRepository;

        public GetProductsProvidersQueryHandler(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public async Task<Response> Handle(GetProductsProvidersQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var response = new Response();
                var product = await _productRepository.GetProductsProviders();

                if (product == null)
                {
                    response.Success = false;
                    response.AddError("Não existe produto cadastrado.");
                }

                response.Success = true;
                response.Result = product;

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
