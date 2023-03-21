using Models;
namespace Services
{
    public class BankService
    {
        static public int OwnersSalaries(decimal profit, decimal expenses, int numberOfOwners)
        {
            return (int)((profit - expenses) / numberOfOwners);
        }
        static public Employee ClientConvertToEmployee(Client client)
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