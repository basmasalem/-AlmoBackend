using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Core.Model
{
   public class Problem
    {
        public int ProblemId { get; set; }
        public string Image { get; set; }
        public string Title { get; set; }
        public string Text { get; set; }
        [ForeignKey("UserCreated")]
        public int UserId { get; set; }
        public bool? IsDeleted { get; set; }
        public DateTime? DateCreated { get; set; }
        public virtual User UserCreated { get; set; }
    }
}
