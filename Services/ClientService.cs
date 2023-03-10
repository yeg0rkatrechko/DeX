using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DbModels;
using Services.Exceptions;
using Models;

namespace Services
{
    public class ClientService
    {
        private BankContext _dbContext;
        public ClientService(BankContext dbContext)
        {
            _dbContext = dbContext;
        }

        private Dictionary<Client, List<Account>> _tempStorage;
        public void ClientAdd()
        {
            string tempPassID;
            DateTime DOB;
            string tempName;
            Console.WriteLine("Введите номер паспорта: ");
            tempPassID = Console.ReadLine();
            if (tempPassID == null) {throw new NoPassData("Вы не ввели паспортные данные");}
            Console.WriteLine("Введите имя и фамилию: ");
            tempName = Console.ReadLine();
            Console.WriteLine("Введите дату рождения: YYYY MM DD");
            int year = Convert.ToInt32(Console.ReadLine());
            int month = Convert.ToInt32(Console.ReadLine());
            int day = Convert.ToInt32(Console.ReadLine());
            DOB = new DateTime(year, month, day);
            if (DateTime.Now.Year - DOB.Year < 18) {throw new Under18("Клиент должен быть совершеннолетним");}
            Client client = new Client(tempPassID, DOB, tempName);
            _tempStorage.Add(client, (new List<Account> { new Account() }));
        }
        public void DictionaryOutput()
        {
            foreach (var temp in _tempStorage)
            {
                Console.WriteLine(_tempStorage.Keys);
            }
        }
    }
}
