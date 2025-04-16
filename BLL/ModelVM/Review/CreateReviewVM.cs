using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.ModelVM.Review
{
    public class CreateReviewVM
    {
        public long Id { get; set; } // Optional, for editing purposes
        public string Comment { get; set; }
        public int Rate { get; set; } // 1 to 5 stars
        public string UserId { get; set; }
        public long ProductId { get; set; }
    }
}
