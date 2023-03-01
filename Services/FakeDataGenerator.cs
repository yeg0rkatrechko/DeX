using Bogus;
using Models;

namespace Services
{
    public class FakeDataGenerator
    {
        public static Faker<Client> CreateFakeClient()
        {
            var generator = new Faker<Client>("ru")
                .StrictMode(true)
                .RuleFor(x => x.ID, y => y.Random.Uuid())
                .RuleFor(x => x.PassportID, y => y.Random.String2(2).ToUpper() + y.Random.Int(1000, 9999).ToString())
                .RuleFor(x => x.Name, y => y.Name.FirstName())
                .RuleFor(x => x.DateOfBirth, y => y.Date.Between(DateTime.Parse("01.01.1950"), DateTime.Parse("01.01.2000")));
            return generator;
        }
        public static Faker<Employee> CreateFakeEmployee()
        {
            var generator = new Faker<Employee>("ru")
                .StrictMode(true)
                .RuleFor(x => x.ID, y => y.Random.Uuid())
                .RuleFor(x => x.PassportID, y => y.Random.String2(2).ToUpper() + y.Random.Int(10000, 99999).ToString())
                .RuleFor(x => x.Name, y => y.Name.FirstName())
                .RuleFor(x => x.DateOfBirth, y => y.Date.Between(DateTime.Parse("01.01.1950"), DateTime.Parse("01.01.2000")))
                .RuleFor(x => x.Contract, (x, y) => Convert.ToInt32(y.Name[0]) + y.PassportID)
                .RuleFor(x => x.Salary, y => y.Random.Int(10, 90) * 100);
            return generator;
        }

        public static List<Client> CreateClientsList(int Count)
        {
            var ListOfClients = new List<Client>();
            for (int i = 0; i < Count; i++)
            {
                ListOfClients.Add(CreateFakeClient());
            }
            return ListOfClients;
        }
        public static Dictionary<string, Client> CreateClientsDictionary(int Count)
        {
            var DictionaryOfClients = new Dictionary<string, Client>();
            for (int i = 0; i < Count; i++)
            {
                Client temp = CreateFakeClient();
                DictionaryOfClients.Add((temp.PassportID + i.ToString()), (temp));
            }
            return DictionaryOfClients;
        }
        public static List<Employee> CreateEmployeeList(int Count)
        {
            var ListOfEmployee = new List<Employee>();
            for (int i = 0; i < Count; i++)
            {
                ListOfEmployee.Add(CreateFakeEmployee());
            }
            return ListOfEmployee;
        }
        public static Dictionary<Client, List<Account>> CreateAccountDictionaryRandom(int Count)
        {
            Dictionary<Client, List<Account>> accountDictionary = new Dictionary<Client, List<Account>>();
            List<Client> tempClients = CreateClientsList(Count);
            Random random = new Random();
            for (int i = 0; i < Count; i++)
            {
                accountDictionary.Add((tempClients[i]),
                new List<Account>
                {new Account {Currency = new Currency("USD", 1), Amount = (random.Next(0,99) * 100)},
                new Account {Currency = new Currency("EUR", 2), Amount = (random.Next(0,99) * 100)},
                new Account {Currency = new Currency("RUB", 3), Amount = (random.Next(0,99) * 100)}});
            }
            return accountDictionary;
        }
        public static Dictionary<Client, List<Account>> CreateAccountDictionary()
        {
            Dictionary<Client, List<Account>> accountDictionary = new Dictionary<Client, List<Account>>();
            accountDictionary.Add(
                new Client("AB12345", new DateTime(1998, 10, 30), "Yegor Katrechko"),
                (new List<Account>
                {new Account {Currency = new Currency("USD", 1), Amount = 100},
                new Account {Currency = new Currency("EUR", 2), Amount = 10}}));
            accountDictionary.Add(
                new Client("AB54321", new DateTime(1995, 12, 04), "Katya Burlova"),
                (new List<Account>
                {new Account {Currency = new Currency("USD", 1), Amount = 120},
                new Account {Currency = new Currency("EUR", 2), Amount = 0}}));
            return accountDictionary;
        }
    }
}
