using AutoMapper;
using Bira.App.Providers.Application.Command;
using Bira.App.Providers.Domain.DTOs.Response;
using Bira.App.Providers.Domain.Entities;
using Bira.App.Providers.Domain.Interfaces.Repositories;
using MediatR;

namespace Bira.App.Providers.Application.Handler
{
    public class CreateProductCommandHandler : IRequestHandler<CreateProductCommand, Response>
    {
        private readonly IMapper _mapper;
        private readonly IProductRepository _productRepository;

        public CreateProductCommandHandler(IMapper mapper, IProductRepository productRepository)
        {
            _mapper = mapper;
            _productRepository = productRepository;
        }

        public async Task<Response> Handle(CreateProductCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var product = _mapper.Map<Product>(request.ProductDto);
                if (!RunValidation(new ProductValidators(), product)) return;
                await _productRepository.Add(product);

                var response = new Response(product, true);

                return response;
            }
            catch (Exception ex)
            {
                var response = new Response();

                response.AddError(ex.Message);
                response.Success = false;

                return response;
            }
        }
    }
}
