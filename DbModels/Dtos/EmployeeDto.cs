using System.ComponentModel.DataAnnotations.Schema;

namespace DbModels.Dtos
{
    public class EmployeeDto
    {
        public string PassportID { get; set; }

        public string Name { get; set; }

        public DateTime DateOfBirth { get; set; }

        public int Salary { get; set; }
    }
}
