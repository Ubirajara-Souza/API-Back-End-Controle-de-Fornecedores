using AutoMapper;
using Bira.App.Providers.Domain.DTOs.Response;
using Bira.App.Providers.Domain.Interfaces.Repositories;
using MediatR;

namespace Bira.App.Providers.Application.Query.QueryHandler
{
    public class GetProductProviderByIdQueryHandler : IRequestHandler<GetProductProviderByIdQuery, Response>
    {
        private readonly IProductRepository _productRepository;

        public GetProductProviderByIdQueryHandler(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }
        public async Task<Response> Handle(GetProductProviderByIdQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var product = await _productRepository.GetProductProviderById(request.Id);

                var response = new Response(product, true);

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
