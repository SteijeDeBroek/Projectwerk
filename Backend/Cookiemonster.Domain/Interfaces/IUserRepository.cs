using Cookiemonster.Infrastructure.EFRepository.Models;
using System.Threading.Tasks;

namespace Cookiemonster.Domain.Interfaces
{
    public interface IUserRepository : IRepository<User>
    {
        Task<User> FindByUsernameAsync(string username);
        Task<User> CreateUserAsync(User user);
    }
}
