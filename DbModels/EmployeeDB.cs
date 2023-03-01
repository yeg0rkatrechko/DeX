using System.ComponentModel.DataAnnotations.Schema;

namespace DbModels
{
    [Table(name: "Employees")]
    public class EmployeeDB
    {
        [Column(name: "ID_Employee")]
        public Guid Id { get; set; }

        [Column(name: "Pasport_ID")]
        public string PassportID { get; set; }

        [Column(name: "Name")]
        public string Name { get; set; }

        [Column(name: "Birth_Date")]
        public DateTime DateOfBirth { get; set; }

        [Column(name: "Salary")]
        public int Salary { get; set; }

    }
}
