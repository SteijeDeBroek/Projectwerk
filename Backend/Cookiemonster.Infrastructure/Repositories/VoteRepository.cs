using Cookiemonster.Models;
using System.Collections.Generic;

namespace Cookiemonster.Infrastructure.Repositories
{
    public class VoteRepository : Repository<Vote>
    {
        public VoteRepository(AppDbContext context) : base(context) { }

    }
}