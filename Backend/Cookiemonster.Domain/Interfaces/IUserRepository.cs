using Cookiemonster.Infrastructure.EFRepository.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cookiemonster.Domain.Interfaces
{
    public interface IUserRepository : IRepository<User>
    {
        public User FindByUsernameAsync(string username);
    }

}
