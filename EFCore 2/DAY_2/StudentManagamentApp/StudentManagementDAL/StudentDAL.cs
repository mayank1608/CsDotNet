using StudentManagementDTO.Models;

namespace StudentManagementDAL
{
    public class StudentDAL
    {
        private StudentMngtDbContext _dbContext;
        
        public StudentDAL()
        {
            _dbContext = new StudentMngtDbContext();
        }
        public bool AddStudentDetails(Students students)
        {
            try
            {
                _dbContext.Students.Add(students);
                _dbContext.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }

        }

    }
}
