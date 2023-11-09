using Cookiemonster.Interfaces;
using Cookiemonster.Models;


namespace Cookiemonster.Services
{
    public class UserService : IDeletable
    {
        private readonly Repository<User> _userRepository;

        public UserService(Repository<User> userRepository)
        {
            _userRepository = userRepository;
        }

        public User CreateUser(User user)
        {
            return _userRepository.Create(user);
        }

        public User GetUser(int id)
        {
            return _userRepository.Get(id);
        }

        public List<User> GetAllUsers()
        {
            return _userRepository.GetAll();
        }

        public User UpdateUser(User user)
        {
            return _userRepository.Update(user);
        }

        public bool DeleteUser(int id)
        {
            var entity = _userRepository.Get(id);
            if (entity == null)
                return false;

            entity.isDeleted = true;
            return true;
        }
    }
}
