using System.ComponentModel.DataAnnotations;

namespace GoodReadsWebApp.Models
{
    public class UpdateReviewRequest
    {
        [Required]
        [StringLength(200)]
        public string Review { get; set; } = string.Empty;

        [Range(1, 5)]
        public int Rating { get; set; }
    }
}