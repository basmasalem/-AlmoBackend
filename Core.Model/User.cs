using System;

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
    }
}
