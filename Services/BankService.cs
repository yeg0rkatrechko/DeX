using Models;
namespace Services
{
    public class BankService
    {
        static public Employee ClientToEmployee (Client client)
        {
            Employee employee = new Employee()
            {
                Name = client.Name,
                DateOfBirth = client.DateOfBirth,
                PassportID = client.PassportID,
                Salary = 0
            };
            return employee;
        }
    }
}