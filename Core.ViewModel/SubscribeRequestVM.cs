using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Core.Api.ViewModels
{
    public class SubscribeRequestVM
    {
        public int SubscribeRequestId { get; set; }
        public DateTime? FromDate { get; set; }
        public DateTime? ToDate { get; set; }
        public int Period { get; set; }
        public decimal? Cost { get; set; }
        public DateTime? DateCreated { get; set; }
        public bool? IsActive { get; set; }

    }
}
