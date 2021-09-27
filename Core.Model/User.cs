using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Core.Model
{
    public class User
    {
        public int UserId { get; set; }
        public string Name { get; set; }

        public string Password { get; set; }
        public string Email { get; set; }
        public int  UserTypeId { get; set; }
        public bool? IsDeleted { get; set; }
        public bool? IsActive { get; set; }
        public bool? IsEmailVerified { get; set; }
        public string UserToken { get; set; }
        [ForeignKey("Course")]
        public int? CourseId { get; set; }
        public int ClassNumber { get; set; }
        public string Image { get; set; }
        public DateTime? ReservationDate { get; set; }
        public virtual Course Course { get; set; }
        public virtual ICollection<Notification> Notifications { get; set; }
        public virtual ICollection<SubscribeRequest> SubscribeRequests { get; set; }
    }
}
