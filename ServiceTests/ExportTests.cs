using Models;
using Services;
using Xunit;

namespace ServiceTests
{
    public class ExportTests
    {
        [Fact]
        public async void ExportClientsToCsvFromDBTest()
        {
            //Arrenge
            DbModels.BankContext bankContext = new DbModels.BankContext();

            string directoryPath = Path.Combine("C:", "Users", "katre", "Desktop", "ExportTest");
            string fileName = "clientsFromDb.csv";

            ExportService exportService = new ExportService(directoryPath, fileName);

            List<Client> clients = new List<Client>();
            foreach (var client in bankContext.Clients)
            {
                clients.Add(new Client
                {
                    ID = client.Id,
                    Name = client.Name,
                    PassportID = client.PassportID,
                    DateOfBirth = client.DateOfBirth
                });
            }


            //Act
            await exportService.ExportClientToCsv(clients);

            //Asert
            Assert.True(true);
        }

        [Fact]
        public async void ReadClientsFromCsvToDBTest()
        {
            //Arrenge
            ClientServiceDB clientServiceDB = new ClientServiceDB();
            string directoryPath = Path.Combine("C:", "Users", "katre", "Desktop", "ExportTest");
            string fileName = "clientsToDb.csv";

            ExportService exportService = new ExportService(directoryPath, fileName);

            List<Client> clients = new List<Client>();
            List<Client> clientsFromCsv = new List<Client>();

            for (int i = 0; i < 10; i++)
            {
                clients.Add(FakeDataGenerator.CreateFakeClient());
            }

            //Act
            await exportService.ExportClientToCsv(clients);

            clientsFromCsv = await exportService.ReadClientFromCsv(directoryPath, fileName);

            foreach (var client in clientsFromCsv)
            {
                await clientServiceDB.AddClientAsync(client);
            }

            //Asert
            Assert.True(true);
        }


        [Fact]
        public async void ClientSerializationWriteAndReadFromFileAsync_Test()
        {
            //Arrenge
            string directoryPath = Path.Combine("C:", "Users", "katre", "Desktop", "ExportTest");
            string fileName = "clientSerialization.json";

            ExportService exportService = new ExportService(directoryPath, fileName);
            List<Client> clients = FakeDataGenerator.CreateFakeClient().Generate(10);

            //Act
            await exportService.PersonSerializationExport(clients, directoryPath, fileName);
            var clientsDesirialization = await exportService.PersonDeserializationImport<Client>(directoryPath, fileName);

            //Assert
            Assert.Equal(clients.First().ID, clientsDesirialization.First().ID);

        }

        [Fact]
        public async void EmployeeSerializationWriteAndReadFromFileAsync_Test()
        {
            //Arrenge
            string directoryPath = Path.Combine("C:", "Users", "katre", "Desktop", "ExportTest");
            string fileName = "employeeSerialization.json";

            ExportService exportService = new ExportService(directoryPath, fileName);
            List<Employee> employees = FakeDataGenerator.CreateFakeEmployee().Generate(10);

            //Act
            await exportService.PersonSerializationExport(employees, directoryPath, fileName);
            List<Employee> employeesDesirialization = await exportService.PersonDeserializationImport<Employee>(directoryPath, fileName);

            //Assert
            Assert.Equal(employees.First().ID, employeesDesirialization.First().ID);

        }
    }
}
