using System.ComponentModel.DataAnnotations;
namespace EcommWebAPI.Models
{
    public class UserAccount
    {
        [Key]
        public int UserAccountId { get; set; }
        public string FullName { get; set; }
        public string UserName { get; set; }
        public string UserEmail { get; set; }
        public string Password { get; set; }
    }
}
