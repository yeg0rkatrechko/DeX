using Models;
using Services;

Employee Gosha = new Employee("Gosha Berbat", "Frontend",  1000);
Currency Dollar = new Currency("Dollar", 51);

void UpdateContract(Employee employee, string _contract)
{
    employee.Contract = new string(_contract);
}
UpdateContract(Gosha, "Backend");


void UpdateCurrency(Currency currency, int _code)
{ 
    currency.Code = _code;
    Console.WriteLine(currency.Code);
    Dollar = new Currency("Ruble", 512);
}
UpdateCurrency(Dollar, 111);
Console.WriteLine(Dollar.Code);

Client Katya = new Client("ABC123", "Katya Burlova");

Employee KatyaB = BankService.ClientToEmployee(Katya);
