using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Core.Model
{
  public  class Notification
    {
        public int NotificationId { get; set; }
        [ForeignKey("ToUser")]
        public int? ToUserId { get; set; }
        public bool? IsRead { get; set; }
        public DateTime DateCreated { get; set; }
        public string Title { get; set; }
        public string Body { get; set; }  
        public virtual User ToUser { get; set; }
    }
}
