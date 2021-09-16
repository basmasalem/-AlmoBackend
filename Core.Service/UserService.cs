using Core.Model;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Core.Service
{
    public interface IUserService
    {
        public List<User> GetAllUsers();
        public List<User> SearchInUsers(string Name,string Email, int TypeId);
        public User GetUserData(int Id);
        public void DeleteUser(User Model);
        public void UpdateUser(User Model);
        public User AddUser(User Model);

        public User ValidateUser(string Email, string Password, int TypeId);


    }
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }
        public User AddUser(User Model)
        {
            return _userRepository.Add(Model);
        }

        public void DeleteUser(User user)
        {
            _userRepository.Delete(user);
        }

        public List<User> GetAllUsers()
        {
           return _userRepository.List().ToList();
        }

        public User GetUserData(int Id)
        {
            return _userRepository.Find(Id);
        }

        public List<User> SearchInUsers(string Name, string Email, int TypeId)
        {
            return _userRepository.List().Where(u=>((string.IsNullOrEmpty(Name)|| u.Name.Contains(Name))&&(string.IsNullOrEmpty(Email) || u.Email==Email))&& u.IsDeleted!=true && u.UserTypeId == TypeId).ToList();
        }

        public void UpdateUser( User Model)
        {
             _userRepository.Update(Model);
        }

        public User ValidateUser(string Email, string Password, int TypeId)
        {
            return _userRepository.List().FirstOrDefault(x => x.Email == Email && x.Password == Password && x.IsDeleted!=true && x.UserTypeId==TypeId);
        }
    }
}
