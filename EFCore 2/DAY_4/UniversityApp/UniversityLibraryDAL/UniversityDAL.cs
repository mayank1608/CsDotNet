using Azure;
using Microsoft.EntityFrameworkCore.Update;
using System.Collections;
using System.Collections.Generic;
using UniversityLibraryDTO.Models;

namespace UniversityLibraryDAL
{
    public class UniversityDAL
    {
        private UniversityDbContext context;
        Student students = new Student() { };
        
        public UniversityDAL()
        {
            context = new UniversityDbContext();
        }
        public bool AddStudentData(Student student)
        {
            try
            {
                //Use DBContext to insert into database
                context.Students.Add(student);
                context.SaveChanges(); //takes insert statement
                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return false;
            }
        }
        public bool AddAllStudentsData(List<Student> studentList)
        {
            try
            {
                //add to context.student
                //save changes
                foreach (Student std in studentList)
                {
                    context.Students.Add(std);
                }
                context.SaveChanges();
                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return false;
            }
        }

        public List<Student> StudentDetails (int age) 
        {
            List<Student> StudentDetails = new List<Student>();
            try
            {
                //IEnumerable Students = from student in context.Students
                                       //where student.StudentAge >= age
                                       //orderby student.FirstName
                                       //select student;
                //Console.WriteLine(Students);

                var Students = context.Students.Where(x => x.StudentAge > age).OrderBy(x => x.FirstName);

                foreach (Student student in Students)
                {
                    StudentDetails.Add(student);
                }
                return StudentDetails;
            }
            catch (Exception ex)
            {

                Console.WriteLine(ex.ToString);
                return StudentDetails;
            }
        }

        public List<StudentDepartmentMap> StuDeptDetails()
        {
            List<StudentDepartmentMap> result = new List<StudentDepartmentMap>();
            try
            {
                var joinquery = from student in context.Students
                                join dept in context.Departments on student.DeptID equals dept.DepartmentId
                                orderby student.FirstName
                                select new
                                {
                                    Fullname = student.FirstName + student.Lastname,
                                    DepartmentId = dept.DepartmentId,
                                    DepartmentName = dept.DepartmentName
                                };

                /*var joinquery = context.Students.Join(context.Departments, stu => stu.DeptID, dept => dept.DepartmentId,
                    (stu, dept) => new
                    {
                        Fullname = stu.FirstName,
                        DepartmentId = dept.DepartmentId,
                        DepartmentName = dept.DepartmentName
                    });*/

                foreach (var mapObj in joinquery)
                {
                    StudentDepartmentMap obj = new StudentDepartmentMap();
                    obj.Name = mapObj.Fullname;
                    obj.DepartmentId = mapObj.DepartmentId;
                    obj.DepartmentName = mapObj.DepartmentName;
                    result.Add(obj);
                }
                return result;
            }
            catch (Exception ex)
            {

                Console.WriteLine(ex.ToString);
                return result;
            }
        }

        
    }
}
