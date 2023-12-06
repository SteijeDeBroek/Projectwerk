using Cookiemonster.Models;
using Microsoft.EntityFrameworkCore;


namespace Cookiemonster.Infrastructure.Repositories
{
    public class ImageRepository : Repository<Image>
    {
        public ImageRepository(AppDbContext context) : base(context) { }
    }
}