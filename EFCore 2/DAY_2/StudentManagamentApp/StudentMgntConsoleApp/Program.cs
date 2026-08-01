// See https://aka.ms/new-console-template for more information
using StudentManagementDAL;
using StudentManagementDTO.Models;

Console.WriteLine("Hello, World!");
try
{
	Students students = new Students()
	{
		StudentId = 1,
		StudentAge = 18,
		FirstName = "Rajat",
		Lastname = "Singh",
		GradeId = 1
		//Grade = new Grades { GradeId = 1 }
	};
	StudentDAL objStudentDAL = new StudentDAL();
	objStudentDAL.AddStudentDetails(students);
}
catch (Exception ex)
{

	Console.WriteLine(ex.Message);
}