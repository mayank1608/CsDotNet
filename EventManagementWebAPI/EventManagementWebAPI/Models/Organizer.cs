using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EventManagementApp.Models
{
    [Table("tbl_Organizer")]
    public class Organizer
    {
        [Key]
        public int OrganizerID { get; set; }

        [Required]
        [StringLength(100)]
        public string UserName { get; set; } = string.Empty;

        [Required]
        [StringLength(100)]
        public string OrganizerPwd { get; set; } = string.Empty;

        // Navigation Property
        public ICollection<OrganizeEvent> OrganizeEvents { get; set; } = new List<OrganizeEvent>();
    }
}