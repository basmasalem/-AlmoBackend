using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Core.Model
{
  public  class SubscribeRequest
    {
        public int SubscribeRequestId { get; set; }
        [ForeignKey("UserCreated")]
        public int UserId { get; set; }    
        public DateTime? FromDate { get; set; }
        public DateTime? ToDate { get; set; }
        public int Period { get; set; }
        public decimal? Cost { get; set; }
        public DateTime? DateCreated { get; set; }
        public virtual User UserCreated { get; set; }
        public string UserCardNumber { get; set; }
        public string UserCardCVV { get; set; }
        public bool? IsDeleted { get; set; }
        public bool? IsActive { get; set; }

    }
}
