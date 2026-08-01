using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace BookManagementDTO.Models
{
    public class Reviews
    {
        [Key]
        public int ReviewID { get; set; }
        public int BookID { get; set; } 
        public Books? Book { get; set; }
        [Range(1, 5, ErrorMessage = "Rating must be between {1} and {5}.")]
        public int Rating { get; set; }
        public string? Feedback_Review {  get; set; }
    }
}
