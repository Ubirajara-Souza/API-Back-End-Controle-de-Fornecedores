using AutoMapper;
using Bira.App.Providers.Application.Command;
using Bira.App.Providers.Domain.DTOs.Request;
using Bira.App.Providers.Domain.Entities;
using Bira.App.Providers.Domain.Interfaces.Repositories;
using Bira.App.Providers.Service.Extensions;
using Bira.App.Providers.Service.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Bira.App.Providers.Api.Controllers.V1
{
    [Route("api/V1/[controller]")]
    public class ProductController : BaseController
    {
        private readonly IProductRepository _productRepository;
        private readonly IProductService _productService;
        private readonly IImageService _imageService;
        private readonly IMapper _mapper;
        private readonly IMediator _mediator;

        public ProductController(IProductRepository productRepository, IProductService productService,
            IImageService imageService, IMapper mapper, IMediator mediator, INotifier notifier) : base(notifier)
        {
            _productRepository = productRepository;
            _productService = productService;
            _imageService = imageService;
            _mapper = mapper;
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IEnumerable<ProductDto>> GetAllProduct()
        {
            var product = await _productRepository.GetProductsProviders();
            var productDto = _mapper.Map<IEnumerable<ProductDto>>(product);
            return productDto;
        }

        [HttpGet("{id:guid}")]
        public async Task<ActionResult<ProductDto>> GetProductById(Guid id)
        {
            var productDto = await GetProduct(id);

            if (productDto == null) return NotFound();

            return productDto;
        }

        [HttpPost]
        public async Task<ActionResult<ProductDto>> AddProduct([ModelBinder(BinderType = typeof(ProductModelBinder))]
        ProductDto productDto)
        {
            if (!ModelState.IsValid) return CustomResponse(ModelState);

            var imagePrefix = Guid.NewGuid() + "_";
            if (!await _imageService.UploadFile(productDto.ImageUpload, imagePrefix))
            {
                return CustomResponse(ModelState);
            }

            productDto.Image = imagePrefix + productDto.ImageUpload.FileName;

            var result = await _mediator.Send(new CreateProductCommand(productDto));

            if (result.Errors.Any())
                return BadRequest(result);

            return CustomResponse(productDto);
        }

        [HttpPut("{id:guid}")]
        public async Task<IActionResult> UpdateProduct(Guid id, [ModelBinder(BinderType = typeof(ProductModelBinder))]
        ProductDto productDto)
        {
            if (id != productDto.Id)
            {
                NotifyError("Os ids informados não são iguais!");
                return CustomResponse();
            }

            var updateProduct = await GetProduct(id);

            if (string.IsNullOrEmpty(productDto.Image))
                productDto.Image = updateProduct.Image;

            if (!ModelState.IsValid) return CustomResponse(ModelState);

            if (productDto.ImageUpload != null)
            {
                var imagePrefix = Guid.NewGuid() + "_";
                if (!await _imageService.UploadFile(productDto.ImageUpload, imagePrefix))
                {
                    return CustomResponse(ModelState);
                }

                updateProduct.Image = imagePrefix + productDto.ImageUpload.FileName;
            }

            updateProduct.ProviderId = productDto.ProviderId;
            updateProduct.Name = productDto.Name;
            updateProduct.Description = productDto.Description;
            updateProduct.Value = productDto.Value;
            updateProduct.Active = productDto.Active;

            var product = _mapper.Map<Product>(updateProduct);
            await _productService.Update(product);

            return CustomResponse(productDto);
        }

        [HttpDelete("{id:guid}")]
        public async Task<ActionResult<ProductDto>> DeleteProduct(Guid id)
        {
            var product = await GetProduct(id);

            if (product == null) return NotFound();

            await _productService.Delete(id);

            return CustomResponse(product);
        }

        private async Task<ProductDto> GetProduct(Guid id)
        {
            var product = await _productRepository.GetProductProviderById(id);
            var productDto = _mapper.Map<ProductDto>(product);
            return productDto;
        }
    }
}

