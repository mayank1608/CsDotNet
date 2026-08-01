using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace BookManagementDTO.Models
{
    public class Authors
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public string AuthorID {  get; set; }
        public string AuthorName { get; set; }
        public ICollection<Books>? Book { get; set; }
    }
}
