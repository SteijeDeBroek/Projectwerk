using Cookiemonster.Domain.Interfaces;
using Cookiemonster.Infrastructure.EFRepository.Context;
using Cookiemonster.Infrastructure.EFRepository.Models;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace Cookiemonster.Infrastructure.Repositories
{
    public class UserRepository : Repository<User>, IUserRepository
    {
        public UserRepository(AppDbContext context) : base(context) { }

        public async Task<User> CreateUserAsync(User user)
        {
            user.Password = BCrypt.Net.BCrypt.HashPassword(user.Password);
            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();
            return user;
        }

        public async Task<User> FindByUsernameAsync(string username)
        {
            return await _context.Users.FirstOrDefaultAsync(u => u.Username == username);
        }

        public async Task<IEnumerable<Todo>> GetTodosByUserIdAsync(int userId)
        {
            var user = await _context.Users
                                     .Include(u => u.Todos)
                                     .FirstOrDefaultAsync(u => u.UserId == userId && !u.IsDeleted);

            var todos = user?.Todos.ToList() ?? new List<Todo>();
            Randomize(todos);
            return todos;
        }

        private void Randomize<T>(IList<T> list)
        {
            Random rng = new Random();
            int n = list.Count;
            while (n > 1)
            {
                n--;
                int k = rng.Next(n + 1);
                T value = list[k];
                list[k] = list[n];
                list[n] = value;
            }
        }
    }
}
