using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Core.Api.ViewModels
{
    public class RequestVM
    {
        [Required(ErrorMessage ="لابد من ادخال رقم البطاقة")]
        public string UserCardNumber { get; set; }
        [Required(ErrorMessage = "لابد من ادخال رقم CVV")]
        public string UserCardCVV { get; set; }
        [Required(ErrorMessage = "لابد من اختيار الباقة المناسبة")]
        public int PackageCode { get; set; }
    }
}
