using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UniversityLibraryDTO.Models
{
    public class Student
    {
        [Key]
        public int? StudentId { get; set; }
        public string? FirstName { get; set; }
        public string? Lastname { get; set; }
        public int StudentAge { get; set; }
        public int DeptID { get; set; }
    }
}
