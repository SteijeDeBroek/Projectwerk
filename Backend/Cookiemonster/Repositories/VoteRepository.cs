using Cookiemonster.Models;
using System.Collections.Generic;

namespace Cookiemonster.Repositories
{
    public class VoteRepository : Repository<Vote>
    {
        public VoteRepository(AppDbContext context) : base(context) { }

    }
}