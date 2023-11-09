using Cookiemonster.Interfaces;
using Cookiemonster.Models;
using Microsoft.EntityFrameworkCore;

namespace Cookiemonster.Services
{
    public class ImageService : IDeletable
    {
        private readonly Repository<Image> _imageRepository;

        public ImageService(Repository<Image> imageRepository)
        {
            _imageRepository = imageRepository;
        }

        public Image CreateImage(Image image)
        {
            return _imageRepository.Create(image);
        }

        public Image GetImage(int id)
        {
            return _imageRepository.Get(id);
        }

        public List<Image> GetAllImages()
        {
            return _imageRepository.GetAll();
        }

        public Image UpdateImage(Image image)
        {
            return _imageRepository.Update(image);
        }

        public bool DeleteImage(int id)
        {
            var entity = _imageRepository.Get(id);
            if (entity == null)
                return false;

            entity.isDeleted = true;
            return true;
        }
    }
}
