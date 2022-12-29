using Microsoft.AspNetCore.Http;

namespace Bira.App.Providers.Service.Interfaces
{
    public interface IImageService
    {
        Task<bool> UploadFile(IFormFile file, string imagePrefix);
    }
}

