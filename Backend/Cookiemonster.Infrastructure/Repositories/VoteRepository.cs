using Cookiemonster.Infrastructure.EFRepository.Context;
using Cookiemonster.Infrastructure.EFRepository.Models;

namespace Cookiemonster.Infrastructure.Repositories
{
    public class VoteRepository : Repository<Vote>
        //async!
    {
        public VoteRepository(AppDbContext context) : base(context) { }

    }
}