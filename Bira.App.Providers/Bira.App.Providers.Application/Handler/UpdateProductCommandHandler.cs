using AutoMapper;
using Bira.App.Providers.Application.Command;
using Bira.App.Providers.Domain.DTOs.Response;
using Bira.App.Providers.Domain.Entities;
using Bira.App.Providers.Domain.Interfaces.Repositories;
using MediatR;

namespace Bira.App.Providers.Application.Handler
{
    public class UpdateProductCommandHandler : IRequestHandler<UpdateProductCommand, Response>
    {
        private readonly IMapper _mapper;
        private readonly IProductRepository _productRepository;

        public UpdateProductCommandHandler(IMapper mapper, IProductRepository productRepository)
        {
            _mapper = mapper;
            _productRepository = productRepository;
        }

        public async Task<Response> Handle(UpdateProductCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var product = _mapper.Map<Product>(request.ProductDto);
                await _productRepository.Update(product);

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
