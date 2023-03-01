using Models;
using Services.Exceptions;

namespace Services
{
    public class ClientService
    {
        private Dictionary<Client, List<Account>> tempStorage;
        public void ClientAdd()
        {
            string tempPassID;
            DateTime DOB;
            string tempName;
            Console.WriteLine("Введите номер паспорта: ");
            tempPassID = Console.ReadLine();
            if (tempPassID == null) { throw new NoPassData("Вы не ввели паспортные данные"); }
            Console.WriteLine("Введите имя и фамилию: ");
            tempName = Console.ReadLine();
            Console.WriteLine("Введите дату рождения: YYYY MM DD");
            int year = Convert.ToInt32(Console.ReadLine());
            int month = Convert.ToInt32(Console.ReadLine());
            int day = Convert.ToInt32(Console.ReadLine());
            DOB = new DateTime(year, month, day);
            if (DateTime.Now.Year - DOB.Year < 18) { throw new Under18("Клиент должен быть совершеннолетним"); }
            Client client = new Client(tempPassID, DOB, tempName);
            tempStorage.Add(client, (new List<Account> { new Account() }));
        }
        public void DictionaryOutput()
        {
            foreach (var temp in tempStorage)
            {
                Console.WriteLine(tempStorage.Keys);
            }
        }
    }
}
