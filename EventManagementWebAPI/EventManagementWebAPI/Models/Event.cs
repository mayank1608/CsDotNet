using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EventManagementApp.Models
{
    [Table("tbl_Events")]
    public class Event
    {
        [Key]
        public int EventID { get; set; }

        [Required]
        [StringLength(100)]
        public string EventType { get; set; } = string.Empty;

        // Navigation Properties
        public ICollection<OrganizeEvent> OrganizeEvents { get; set; } = new List<OrganizeEvent>();

        public ICollection<Member> Members { get; set; } = new List<Member>();
    }
}