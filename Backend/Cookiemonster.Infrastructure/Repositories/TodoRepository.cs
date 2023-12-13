 using Cookiemonster.Infrastructure.EFRepository.Context;
using Cookiemonster.Infrastructure.EFRepository.Models;

namespace Cookiemonster.Infrastructure.Repositories
{
    public class TodoRepository : Repository<Todo>
    {
        public TodoRepository(AppDbContext context) : base(context) { }
    }
}