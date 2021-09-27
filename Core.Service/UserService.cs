using Core.Model;
using Core.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Core.Service
{
    public interface IUserService
    {
        public List<User> GetAllUsers();
        public List<User> GetAllSubscribedUsers();
        public List<User> SearchInUsers(string Name,string Email, int TypeId);
        public List<User> SearchInSubscribedUsers(string Name,string Email, int TypeId);
        public User GetUserData(int Id);
        public void DeleteUser(User Model);
        public void UpdateUser(User Model);
        public User AddUser(User Model);

        public ValidationResultVM ValidateRegisterdUser(string Email, string Password, int TypeId);
        public ValidationResultVM ValidateLogedUser(string Email, string Password, int TypeId, string NotificationToken);

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
           return _userRepository.List().Where(u=>u.IsDeleted!=true).ToList();
        }
        public List<User> GetAllSubscribedUsers()
        {
            return _userRepository.List().Where(u=>u.SubscribeRequests.Count()>0 && u.IsDeleted != true).ToList();
        }
        public User GetUserData(int Id)
        {
            return _userRepository.Find(Id);
        }

        public List<User> SearchInUsers(string Name, string Email, int TypeId)
        {
            return _userRepository.List().Where(u=>((string.IsNullOrEmpty(Name)|| u.Name.Contains(Name))&&(string.IsNullOrEmpty(Email) || u.Email==Email))&& u.IsDeleted!=true && u.UserTypeId == TypeId).ToList();
        }
        public List<User> SearchInSubscribedUsers(string Name, string Email, int TypeId)
        {
            return _userRepository.List().Where(u => ((string.IsNullOrEmpty(Name) || u.Name.Contains(Name)) && (string.IsNullOrEmpty(Email) || u.Email == Email)) && u.IsDeleted != true && u.UserTypeId == TypeId && u.SubscribeRequests .Count()>0).ToList();
        }

        public void UpdateUser( User Model)
        {
             _userRepository.Update(Model);
        }

        public ValidationResultVM ValidateRegisterdUser(string Email, string Password, int TypeId)
        {
           User user=_userRepository.List().FirstOrDefault(x => x.Email == Email && x.Password == Password && x.IsDeleted!=true && x.UserTypeId==TypeId);
            if (user != null && user.IsEmailVerified != true)
                return new ValidationResultVM {UserData=user,  Message = " هذا المستخدم مسجل من قبل ولكن غير مفعل  عن طريق البريد الالكترونى" };
            if (user != null && user.IsActive != true)
                return new ValidationResultVM {UserData=user, Message = "هذا المستخدم مسجل من قبل ولكن غير مفعل" };
            else if (user != null)
                return new ValidationResultVM {UserData=user, Message = "هذا المستخدم مسجل من قبل" };
            else return new ValidationResultVM { };
        }
        public ValidationResultVM ValidateLogedUser(string Email, string Password, int TypeId, string NotificationToken)
        {
            User user = _userRepository.List().FirstOrDefault(x => x.Email == Email && x.Password == Password && x.IsDeleted != true && x.UserTypeId == TypeId);
            if (user != null && user.IsEmailVerified != true)
                return new ValidationResultVM {  Message = " هذا المستخدم مسجل من قبل ولكن غير مفعل  عن طريق البريد الالكترونى" };
            if (user != null && user.IsActive != true)
            {               
                return new ValidationResultVM { Message = "هذا المستخدم مسجل من قبل ولكن غير مفعل" };
            }
            else if (user != null)
            {
                user.UserToken = NotificationToken;
                UpdateUser(user);
                return new ValidationResultVM { UserData = user, Message = "تم تسجيل الدخول بنجاح" };

            }
            else return new ValidationResultVM { };
           
        }
    }
}
