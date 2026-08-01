using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GoodReadsWebApp.Models
{
    [Table("tbl_Reviews")]
    public class Review
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Review_id { get; set; }
        [Required]
        public int Book_isbn { get; set; }
        [Required]
        public int Rating { get; set; }
        [Required]
        [StringLength(200)]
        public string ReviewText { get; set; } = string.Empty;
        // Navigation Property
        [ForeignKey(nameof(Book_isbn))]
        public Book Book { get; set; } = null!;
    }
}