namespace Models
{
    public class Employee : Person
    {
        public string Contract { get; set; }
        public decimal Salary { get; set; }
        public Employee() { }
        public Employee(string _name, string _contract, decimal _salary)
        {
            Name = _name;
            Contract = _contract;
            Salary = _salary;
        }
    }
}
