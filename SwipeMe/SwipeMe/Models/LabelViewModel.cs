using System;
using System.Collections.Generic;
using System.Text;

namespace SwipeMe.Models
{
    public class LabelViewModel
    {
        public string Caption { get; set; }
        public string Description { get; set; }
        public DateTime CreatedDate { get; set; } = System.DateTime.UtcNow;
    }
}
