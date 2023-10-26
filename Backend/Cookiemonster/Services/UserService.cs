﻿using Cookiemonster.Models;


namespace Cookiemonster.Services
{
    public class UserService
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
            return _userRepository.Delete(id);
        }
    }
}
