using System;
using System.Collections.Generic;

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
        public virtual ICollection<SubscribeRequest> SubscribeRequests { get; set; }
    }
}
