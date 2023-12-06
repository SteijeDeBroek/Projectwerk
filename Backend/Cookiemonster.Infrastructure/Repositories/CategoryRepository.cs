using Cookiemonster.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace Cookiemonster.Infrastructure.Repositories
{
    public class CategoryRepository : Repository<Category>
    {
        public CategoryRepository(AppDbContext context) : base(context) { }

    }
}
