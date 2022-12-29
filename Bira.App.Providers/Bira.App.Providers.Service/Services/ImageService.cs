using Bira.App.Providers.Service.Interfaces;
using Bira.App.Providers.Service.Notifications;
using Microsoft.AspNetCore.Http;

namespace Bira.App.Providers.Service.Services
{
    public class ImageService : IImageService
    {
        private readonly INotifier _notifier;

        public ImageService(INotifier notifier)
        {
            _notifier = notifier;
        }

        public async Task<bool> UploadFile(IFormFile file, string imagePrefix)
        {
            if (file == null || file.Length == 0)
            {
                _notifier.Handle(new Notification("Forneça uma imagem para este produto!"));
                return false;
            }

            var path = Path.Combine(Directory.GetCurrentDirectory(), "C:/Users/Birinha/Desktop/Demo-imagens-App/Imagens", imagePrefix + file.FileName);

            if (File.Exists(path))
            {
                _notifier.Handle(new Notification("Já existe um arquivo com este nome!"));
                return false;
            }

            using (var stream = new FileStream(path, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }

            return true;
        }
    }
}
