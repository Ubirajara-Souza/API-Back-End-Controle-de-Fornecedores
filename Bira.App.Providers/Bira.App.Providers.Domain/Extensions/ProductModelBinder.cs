using Bira.App.Providers.Domain.DTOs.Request;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.Text.Json;

namespace Bira.App.Providers.Service.Extensions
{
    public class ProductModelBinder : IModelBinder
    {
        public Task BindModelAsync(ModelBindingContext bindingContext)
        {
            if (bindingContext == null)
            {
                throw new ArgumentNullException(nameof(bindingContext));
            }

            var serializeOptions = new JsonSerializerOptions
            {
                WriteIndented = true,
                Encoder = System.Text.Encodings.Web.JavaScriptEncoder.UnsafeRelaxedJsonEscaping,
                PropertyNameCaseInsensitive = true
            };

            var productDto = JsonSerializer.Deserialize<ProductDto>(bindingContext.ValueProvider.GetValue("product").FirstOrDefault(), serializeOptions);
            productDto.ImageUpload = bindingContext.ActionContext.HttpContext.Request.Form.Files.FirstOrDefault();

            bindingContext.Result = ModelBindingResult.Success(productDto);
            return Task.CompletedTask;
        }
    }
}
