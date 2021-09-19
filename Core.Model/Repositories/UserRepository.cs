using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Core.Model { 
    public class UserRepository : IUserRepository
    {
        public AppDBContext _appDBContext;
        public UserRepository(AppDBContext appDBContext)
        {
            _appDBContext=appDBContext;
        }
        public User Add(User entity)
        {
           _appDBContext.Users.Add(entity);
            Commit();
            return entity;
        }

        public void Commit()
        {
            _appDBContext.SaveChanges();
        }

        public void Delete(User entity)
        {
            entity.IsDeleted = true;
            Update(entity);
            Commit();
        }

        public User Find(int id)
        {
            return _appDBContext.Users.Find(id);
        }

        public IQueryable<User> List()
        {
            return _appDBContext.Users.Where(u => u.IsDeleted != true);
        }

        public void Update(User entity)
        {
            _appDBContext.Users.Update(entity);
            Commit();
        }

        
    }
    public interface IUserRepository : IRepository<User>
    {
       
    }
}
