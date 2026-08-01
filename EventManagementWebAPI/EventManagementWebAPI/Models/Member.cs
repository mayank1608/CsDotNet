using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EventManagementApp.Models
{
    [Table("tbl_Members")]
    public class Member
    {
        [Key]
        public int MemberID { get; set; }

        [Required]
        [StringLength(100)]
        public string MemberName { get; set; } = string.Empty;

        [Required]
        public DateTime BirthDate { get; set; }

        [Required]
        [StringLength(150)]
        public string Email { get; set; } = string.Empty;

        [Required]
        public int EventID { get; set; }

        // Navigation Property
        public Event? Event { get; set; }
    }
}