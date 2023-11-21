using Cookiemonster.Models;
using System.Collections.Generic;

namespace Cookiemonster.Repositories
{
    public class UserRepository : Repository<User>
    {
        public UserRepository(AppDbContext context) : base(context) { }

    }
}