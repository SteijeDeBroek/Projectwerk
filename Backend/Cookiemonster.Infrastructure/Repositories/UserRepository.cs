using Cookiemonster.Infrastructure.EFRepository.Context;
using Cookiemonster.Infrastructure.EFRepository.Models;
using BCrypt.Net;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks; // Voeg deze using-directive toe voor Task

namespace Cookiemonster.Infrastructure.Repositories
{
    public class UserRepository : Repository<User>
    {
        private readonly AppDbContext _context;

        public UserRepository(AppDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<User> CreateAsync(User user)
        {
            user.Password = BCrypt.Net.BCrypt.HashPassword(user.Password);
            return await base.CreateAsync(user);
        }

        public async Task<User> FindByUsernameAsync(string username)
        {
            return await _context.Users.FirstOrDefaultAsync(u => u.Username == username);
        }
    }
}
