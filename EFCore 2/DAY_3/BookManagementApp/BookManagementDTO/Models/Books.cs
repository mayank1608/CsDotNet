using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Security.Principal;
using System.Text;

namespace BookManagementDTO.Models
{
    public class Books
    {
        [Key]
        public int BookID { get; set; }
        public string Title { get; set; }   
        public int Book_Edition {  get; set; }
        [ForeignKey("Authors")]
        public string AuthorID {  get; set; }
        public Authors? Author { get; set; }
        public ICollection<Reviews> Review { get; set; }

    }
}
