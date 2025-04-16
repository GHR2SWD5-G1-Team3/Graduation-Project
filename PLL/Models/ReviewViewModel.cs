using System.ComponentModel.DataAnnotations;
namespace PLL.Models
{
    public class ReviewViewModel
    {
        public long Id { get; set; } // needed for Edit/Delete

        [Required]
        public string Comment { get; set; }

        [Range(1, 5)]
        public int Rate { get; set; }

        public long ProductId { get; set; }

        // This will not be filled by the form. We’ll populate it in the controller.
        public string? UserId { get; set; }

        public DateTime CreatedAt { get; set; } // for display purposes
    }
}
