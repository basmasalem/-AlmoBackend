using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Core.Model
{
   public class Help
    {
        public int HelpId { get; set; }
        public string Email { get; set; }
        public string Name { get; set; }
        public string Text { get; set; }
        [ForeignKey("UserCreated")]
        public int UserId { get; set; }
        public bool? IsDeleted { get; set; }
        public DateTime? DateCreated { get; set; }
        public virtual User UserCreated { get; set; }
    }
}
