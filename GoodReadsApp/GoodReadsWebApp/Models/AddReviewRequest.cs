using System.ComponentModel.DataAnnotations;

namespace GoodReadsWebApp.Models
{
    public class AddReviewRequest
    {
        [Required]
        public int Book_isbn { get; set; }

        [Required]
        [StringLength(200)]
        public string ReviewText { get; set; } = string.Empty;

        [Range(1, 5)]
        public int Rating { get; set; }
    }
}