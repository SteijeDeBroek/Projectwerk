using Cookiemonster.Infrastructure.EFRepository.Context;
using Cookiemonster.Infrastructure.EFRepository.Models;
using BCrypt.Net;
using Microsoft.EntityFrameworkCore;

namespace Cookiemonster.Infrastructure.Repositories
{
    public class UserRepository : Repository<User>
        //async!
    {
        private readonly AppDbContext _context;

        public UserRepository(AppDbContext context) : base(context) { 
            _context = context;
        }

        public User Create(User user)
        {
            user.Password = BCrypt.Net.BCrypt.HashPassword(user.Password);
            return base.Create(user);
        }

        public async Task<User> FindByUsernameAsync(string username)
        {
            return await _context.Users.FirstOrDefaultAsync(u => u.Username == username);
        }
    }
}