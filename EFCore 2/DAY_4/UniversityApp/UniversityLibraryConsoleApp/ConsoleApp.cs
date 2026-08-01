using UniversityLibraryDTO.Models;
using UniversityLibraryDAL;

namespace UniversityLibraryConsoleApp
{
    public class ConsoleApp
    {

        private static List<Student> GetStudentData()
        {
            List<Student> studentList = new List<Student>{
                                new Student {  FirstName = "Renee", Lastname = "Becker", StudentAge = 20, DeptID = 1 },
                                 new Student {  FirstName = "Scott", Lastname = "Becker", StudentAge = 21, DeptID = 1 },
                                 new Student {  FirstName = "Steven", Lastname = "King", StudentAge = 20, DeptID = 2 },
                                 new Student {  FirstName = "David", Lastname = "Austin", StudentAge = 20, DeptID = 3 },
                                 new Student {  FirstName = "James", Lastname = "Marlow", StudentAge = 21, DeptID = 2 },
                                 new Student {  FirstName = "John", Lastname = "Seo", StudentAge = 20, DeptID = 4 },
                                 new Student {  FirstName = "Joshua", Lastname = "Patel", StudentAge = 21, DeptID = 4 },
                                 new Student {  FirstName = "James", Lastname = "Landry", StudentAge = 20, DeptID = 2 }

                             };
            return studentList;
            


        }
        static void Main(string[] args)
        
        {
            try
            {
                Console.WriteLine("University Management App");

                Console.WriteLine("Enter your choice" +
                    "\n1. Add Student data." +
                    "\n2. Add all student data" +
                    "\n3. Display the student details whose age is greater than 20" +
                    "\n4. Display Student details with Department details");
                    
                Console.WriteLine("Enter Your Choice :");

                int key = Convert.ToInt32(Console.ReadLine());
               
                UniversityDAL DALObj = new UniversityDAL();

                switch (key)
                {
                    case 1:
                        Console.WriteLine("Enter the Student Details\n");
                        Console.WriteLine("Enter First Name: ");
                        string fname = Console.ReadLine();
                        Console.WriteLine("Enter Last Name: ");
                        string lname = Console.ReadLine();
                        Console.WriteLine("Enter Age: ");
                        int age = Convert.ToInt32(Console.ReadLine());

                        //Create an obj of the class Student
                        Student Student1 = new Student();
                        Student1.FirstName = fname;
                        Student1.Lastname = lname;
                        Student1.StudentAge = age;

                        
                        bool result = DALObj.AddStudentData(Student1);
                        if (result)
                        {
                            Console.WriteLine("Student data Added successfully");
                        }
                        else
                        {
                            Console.WriteLine("Error occured");
                        }
                        break;
                    case 2:

                        { //Add the student obj to list
                          //invoke the add all students function

                            List<Student> studentList = GetStudentData();
                            
                            result = DALObj.AddAllStudentsData(studentList);
                            if (result)
                            {
                                Console.WriteLine("Student data Added successfully");
                            }
                            else
                            {
                                Console.WriteLine("Error occured");
                            }

                            break;

                        }
                    case 3:
                        {
                            Console.WriteLine("Display the student details whose age is greater than 20 :");
                            
                            int age1 = 20;

                            List<Student> details = DALObj.StudentDetails(age1);

                            if (details.Count != 0)
                            {
                                foreach (Student student in details)
                                {
                                    Console.WriteLine("Student Details : ");
                                    Console.WriteLine($" STUDENT ID :{student.StudentId} \n STUDENT NAME :{student.FirstName} {student.Lastname} \n AGE :{student.StudentAge} \n DEPARTMENT :{student.DeptID}");
                                    Console.WriteLine("-------------------------");
                                }
                                Console.WriteLine("Successful query");
                            }
                            else
                            {
                                Console.WriteLine("Error occured");
                            }
                            
                            break;
                        }

                    case 4:
                        {
                            Console.WriteLine("Student and their Department Details : ");
                            List<StudentDepartmentMap> stuDept = DALObj.StuDeptDetails();
                            
                            if (stuDept.Count != 0)
                            {
                                foreach (StudentDepartmentMap studentDepartmentMap in stuDept)
                                {
                                    
                                    
                                    Console.WriteLine($" STUDENT ID :{studentDepartmentMap.studentId} \n STUDENT NAME :{studentDepartmentMap.Name} \n DEPARTMENT ID :{studentDepartmentMap.DepartmentId} \n DEPARTMENT NAME :{studentDepartmentMap.DepartmentName}");
                                    Console.WriteLine("-------------------------");
                                }
                                Console.WriteLine("Successful query");

                            }
                            else
                            {
                                Console.WriteLine("Error occured");
                            }
                            break;
                        }
                    default:
                        break;

                }



            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }


    }
    

  
}
