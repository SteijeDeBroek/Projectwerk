using Cookiemonster.Infrastructure.EFRepository.Context;
using Cookiemonster.Infrastructure.EFRepository.Models;

namespace Cookiemonster.Infrastructure.Repositories
{
    public class UserRepository : Repository<User>
        //async!
    {
        public UserRepository(AppDbContext context) : base(context) { }

    }
}