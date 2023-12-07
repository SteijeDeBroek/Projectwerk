using Cookiemonster.Infrastructure.EFRepository.Context;
using Cookiemonster.Infrastructure.EFRepository.Models;


namespace Cookiemonster.Infrastructure.Repositories
{
    public class ImageRepository : Repository<Image>
    {
        public ImageRepository(AppDbContext context) : base(context) { }
    }
}