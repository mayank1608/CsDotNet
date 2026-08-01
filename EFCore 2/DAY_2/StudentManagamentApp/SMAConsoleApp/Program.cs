// See https://aka.ms/new-console-template for more information
using StudentManagementDAL;
using StudentManagementDTO.Models;

Console.WriteLine("Hello, World!");
StudentDAL objStudentDAL = new StudentDAL();
Students students = new Students()
{
    StudentId = 1,
    FirstName = "Yolo",
    Lastname = "LOL",
    StudentAge = 30,
    GradeId = 1
};
objStudentDAL.AddStudentDetails(students);