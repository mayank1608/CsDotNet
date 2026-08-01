using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentManagementDTO.Models
{
    public class Students
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int? StudentId { get; set; }
        public string FirstName { get; set; }
        public string Lastname { get; set; }
        public int StudentAge { get; set; }

        public int GradeId { get; set; }
        public Grades? Grade { get; set; }
    }

}

