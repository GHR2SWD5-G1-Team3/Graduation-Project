using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.ModelVM.Review
{
    public class DisplayReviewVM
    {
        public long Id { get; set; }
        public string Comment { get; set; }
        public int Rate { get; set; }
        public DateTime CreatedAt { get; set; }
        public string UserName { get; set; } // For display purposes, map User info
    }
}
