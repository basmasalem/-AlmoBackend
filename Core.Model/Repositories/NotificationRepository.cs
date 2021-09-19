using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Core.Model { 
    public class NotificationRepository : INotificationRepository
    {
        public AppDBContext _appDBContext;
        public NotificationRepository(AppDBContext appDBContext)
        {
            _appDBContext=appDBContext;
        }
        public Notification Add(Notification entity)
        {
           _appDBContext.Notifications.Add(entity);
            Commit();
            return entity;
        }

        public void Commit()
        {
            _appDBContext.SaveChanges();
        }

        public void Delete(Notification entity)
        {
           
            Update(entity);
            Commit();
        }

        public Notification Find(int id)
        {
            return _appDBContext.Notifications.Find(id);
        }

        public IQueryable<Notification> List()
        {
            return _appDBContext.Notifications.OrderByDescending(x=>x.DateCreated);
        }

        public void Update(Notification entity)
        {
            _appDBContext.Notifications.Update(entity);
            Commit();
        }

        
    }
    public interface INotificationRepository : IRepository<Notification>
    {
       
    }
}
