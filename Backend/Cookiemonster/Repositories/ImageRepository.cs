using Cookiemonster.Models;
using Microsoft.EntityFrameworkCore;


namespace Cookiemonster.Repositories
{
    public class ImageRepository : Repository<Image>
    {
        public ImageRepository(AppDbContext context) : base(context) { }
    }
}