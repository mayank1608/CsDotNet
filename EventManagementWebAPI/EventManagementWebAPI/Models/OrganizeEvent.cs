using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EventManagementApp.Models
{
    [Table("tbl_OrganizeEvent")]
    public class OrganizeEvent
    {
        [Key]
        public int OrganizeEventID { get; set; }

        [Required]
        public int EventID { get; set; }

        [Required]
        public int OrganizerID { get; set; }

        [Required]
        [StringLength(200)]
        public string EventLocation { get; set; } = string.Empty;

        [Required]
        [StringLength(100)]
        public string City { get; set; } = string.Empty;

        [Required]
        public DateTime StartDate { get; set; }

        [Required]
        public DateTime EndDate { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal Price { get; set; }

        [Required]
        [StringLength(500)]
        public string EventDesc { get; set; } = string.Empty;

        // Navigation Properties
        public Event? Event { get; set; }

        public Organizer? Organizer { get; set; }
    }
}