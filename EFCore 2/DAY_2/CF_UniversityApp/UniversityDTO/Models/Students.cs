using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UniversityDTO.Models
{
    public class Students
    {
        [Key]
        public int StudentId { get; set; }
        [Required][MaxLength(50)]
        public string FirstName { get; set; }
        public string Lastname { get; set; }
        public int StudentAge { get; set; }
        [ForeignKey("Departments")]
        public int DeptID { get; set; }
        public virtual Departments Departments { get; set; }
    }
}
