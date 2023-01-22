using AutoMapper;
using Bira.App.Providers.Application.Command;
using Bira.App.Providers.Application.Query;
using Bira.App.Providers.Domain.DTOs.Request;
using Bira.App.Providers.Domain.Extensions;
using Bira.App.Providers.Domain.Interfaces;
using Bira.App.Providers.Service.Extensions;
using Bira.App.Providers.Service.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Bira.App.Providers.Api.Controllers.V1
{
    [Authorize]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class ProductController : BaseController
    {
        private readonly IImageService _imageService;
        private readonly IMapper _mapper;
        private readonly IMediator _mediator;

        public ProductController(IImageService imageService, IMapper mapper,
            IMediator mediator, INotifier notifier, IUser user) : base(notifier, user)
        {
            _imageService = imageService;
            _mapper = mapper;
            _mediator = mediator;
        }

        [AllowAnonymous]
        [HttpGet]
        public async Task<IEnumerable<ProductDto>> GetAllProduct()
        {
            var product = await _mediator.Send(new GetProductsProvidersQuery());
            var productDto = _mapper.Map<IEnumerable<ProductDto>>(product.Result);
            return productDto;
        }

        [ClaimsAuthorize("Product", "GetByID")]
        [HttpGet("{id:guid}")]
        public async Task<ActionResult<ProductDto>> GetProductById(Guid id)
        {
            var productDto = await GetProduct(id);

            if (productDto == null) return NotFound();

            return productDto;
        }

        [ClaimsAuthorize("Product", "Add")]
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

            if (result.Errors.Any()) return BadRequest(result);

            return CustomResponse(productDto);
        }

        [ClaimsAuthorize("Product", "Update")]
        [HttpPut("{id:guid}")]
        public async Task<IActionResult> UpdateProduct(Guid id, [ModelBinder(BinderType = typeof(ProductModelBinder))]
        ProductDto productDto)
        {
            if (id != productDto.Id)
            {
                NotifyError("Os ids informados não são iguais!");
                return CustomResponse();
            }

            if (!ModelState.IsValid) return CustomResponse(ModelState);

            var updateProduct = await GetProduct(id);

            if (string.IsNullOrEmpty(productDto.Image))
                productDto.Image = updateProduct.Image;

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

            var result = await _mediator.Send(new UpdateProductCommand(updateProduct));

            if (result.Errors.Any()) return BadRequest(result);

            return CustomResponse(productDto);
        }

        [ClaimsAuthorize("Product", "Delete")]
        [HttpDelete("{id:guid}")]
        public async Task<ActionResult<ProductDto>> DeleteProduct(Guid id)
        {
            var product = await GetProduct(id);

            if (product == null) return NotFound();

            await _mediator.Send(new DeleteProductCommand(id));

            return CustomResponse(product);
        }

        private async Task<ProductDto> GetProduct(Guid id)
        {
            var product = await _mediator.Send(new GetProductProviderByIdQuery(id));
            var productDto = _mapper.Map<ProductDto>(product.Result);
            return productDto;
        }
    }
}



