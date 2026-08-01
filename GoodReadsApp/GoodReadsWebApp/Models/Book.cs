using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GoodReadsWebApp.Models
{
    [Table("tbl_Books")]
    public class Book
    {
        [Key]
        public int Book_isbn { get; set; }

        [Required]
        [StringLength(100)]
        public string Title { get; set; } = string.Empty;

        [Required]
        public int Book_edition { get; set; }

        [Required]
        public int Author_id { get; set; }

        // public Author? Author { get; set; }
        // Navigation Property
        [ForeignKey(nameof(Author_id))]
        public Author Author { get; set; } = null!;
        public ICollection<Review> Reviews { get; set; } = new List<Review>();
    }
}