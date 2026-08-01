using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GoodReadsWebApp.Models
{
    [Table("tbl_Authors")]
    public class Author
    {
        [Key]
        public int Author_id { get; set; }

        [Required]
        [StringLength(50)]
        public string Author_name { get; set; } = string.Empty;

        // Navigation Property
        public ICollection<Book> Books { get; set; } = new List<Book>();
    }
}