using Cookiemonster.Models;
using System.Collections.Generic;

namespace Cookiemonster.Repositories
{
    public class TodoRepository : Repository<Todo>
    {
        public TodoRepository(AppDbContext context) : base(context) { }
    }
}