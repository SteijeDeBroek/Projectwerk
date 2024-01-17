using Cookiemonster.Infrastructure.EFRepository.Context;
using Cookiemonster.Infrastructure.EFRepository.Models;

namespace Cookiemonster.Infrastructure.Repositories
{
    public class VoteRepository : Repository<Vote>
    {
        public VoteRepository(AppDbContext context) : base(context) { }

    }
}