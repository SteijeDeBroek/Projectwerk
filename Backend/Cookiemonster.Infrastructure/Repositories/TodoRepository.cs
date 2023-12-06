using Cookiemonster.Models;
using System.Collections.Generic;

namespace Cookiemonster.Infrastructure.Repositories
{
    public class TodoRepository : Repository<Todo>
    {
        public TodoRepository(AppDbContext context) : base(context) { }
    }
}