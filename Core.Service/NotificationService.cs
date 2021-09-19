using Core.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Core.Service
{

    public interface INotificationService
    {

        public Notification GetNotificationData(int Id);
     
        public void DeleteNotification(Notification Model);
        public void UpdateNotification(Notification Model);
        public Notification AddNotification(Notification Model);
        public List<Notification> GetAllNotifications(int UserId);


    }
    public class NotificationService : INotificationService
    {
        private readonly INotificationRepository _NotificationRepository;
        public NotificationService(INotificationRepository NotificationRepository)
        {
            _NotificationRepository = NotificationRepository;
        }
        public Notification AddNotification(Notification Model)
        {
            return _NotificationRepository.Add(Model);
        }

        public void DeleteNotification(Notification Notification)
        {
            _NotificationRepository.Delete(Notification);
        }

   

        public Notification GetNotificationData(int Id)
        {
            return _NotificationRepository.List().FirstOrDefault(h=>h.NotificationId==Id);
        }

        public List<Notification> GetAllNotifications(int UserId)
        {
            return _NotificationRepository.List().Where(s=>s.ToUserId==UserId).ToList();
        }



        public void UpdateNotification(Notification Model)
        {
            _NotificationRepository.Update(Model);
        }


    }
}
