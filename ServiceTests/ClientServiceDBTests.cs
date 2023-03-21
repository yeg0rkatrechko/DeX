using Microsoft.EntityFrameworkCore;
using Models;
using Services;
using Services.Exceptions;
using Xunit;
using DbModels;
using AutoMapper;
using Services.Mapping;
using Microsoft.Extensions.Configuration;

namespace ServiceTests
{
    public class ClientServiceDBTests
    {
        private readonly IMapper _mapper;
        private readonly BankContext _dbContext;
        private readonly IConfiguration _configuration;

        public ClientServiceDBTests()
        {
            var mapperConfig = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new MappingProfile());
            });
            _mapper = mapperConfig.CreateMapper();

            _configuration = new ConfigurationBuilder()
                .SetBasePath("C:\\Users\\katre\\Source\\Repos\\yeg0rkatrechko\\DeX\\BankAPI")
                .AddJsonFile("appsettings.json")
                .Build();

            var optionsBuilder = new DbContextOptionsBuilder<BankContext>()
                .UseInMemoryDatabase(databaseName: "TestDB");
            _dbContext = new BankContext(optionsBuilder, _configuration);
        }

        [Fact]
        public async Task AddClientAsync_Should_Throw_Exception_When_Client_Under_18()
        {
            // Arrange
            var clientService = new ClientService(_mapper, _dbContext);

            var clientToAdd = new Client {
                    Name = "Михаил",
                    PassportID = "AB123456",
                    DateOfBirth = new DateTime(2009, 11, 11),
                    };

            // Act and Assert
            try
            {
                await clientService.AddClientAsync(clientToAdd);
            }
            catch (Limit18YearsException e)
            {
                Assert.Equal(typeof(Limit18YearsException), e.GetType());
                Assert.Equal("Клиент не может быть младше 18 лет", e.Message);
            }
            catch (Exception)
            {
                Assert.True(false);
            }
        }

        [Fact]
        public async Task AddNewClientNoPassDataExceptionTest()
        {
            // Arrange
            var clientService = new ClientService(_mapper, _dbContext);

            Client client = new Client()
            {
                Name = "Игорь",
                PassportID = null,
                DateOfBirth = new DateTime(1998, 1, 2),
            };

            // Act/Assert
            try
            {
                await clientService.AddClientAsync(client);
            }
            catch (NoPassportDataException e)
            {
                Assert.Equal(typeof(NoPassportDataException), e.GetType());
                Assert.Equal("Вы не ввели паспортные данные", e.Message);
            }
            catch (Exception)
            {
                Assert.True(false);
            }
        }

        //[Fact]
        //public async Task AddNewAlreadyExcistingClientTest()
        //{
        //    // Arrange
        //    var clientService = new ClientService(_mapper, _dbContext);

        //    Client oldClient = FakeDataGenerator.CreateFakeClient();

        //    Client newClient = new Client()
        //    {
        //        ID = oldClient.ID,
        //        Name = oldClient.Name,
        //        PassportID = oldClient.PassportID,
        //        DateOfBirth = oldClient.DateOfBirth
        //    };

        //    // Act/Assert
        //    try
        //    {
        //        await clientService.AddClientAsync(oldClient);
        //        await clientService.AddClientAsync(newClient);
        //    }
        //    catch (ArgumentException ex)
        //    {
        //        Assert.Equal(typeof(ArgumentException), ex.GetType());
        //        Assert.Equal("Такой клиент уже существует", ex.Message);
        //    }
        //    catch (Exception)
        //    {
        //        Assert.True(false);
        //    }
        //}

        //[Fact]
        //public async Task DeleteClientByNonExcistingKeyTest()
        //{
        //    // Arrange
        //    var clientService = new ClientService(_mapper, _dbContext);

        //    Client existsClient = FakeDataGenerator.CreateFakeClient();

        //    Client noExistsClient = FakeDataGenerator.CreateFakeClient();

        //    // Act/Assert
        //    try
        //    {
        //        await clientService.AddClientAsync(existsClient);
        //        await Assert.ThrowsAsync<KeyNotFoundException>(() => clientService.DeleteClientAsync(noExistsClient.ID));

        //        await clientService.DeleteClientAsync(existsClient.ID);
        //        Assert.Null(await clientService._dbContext.Clients.FirstOrDefaultAsync(p => p.Id == existsClient.ID));
        //    }
        //    catch (Exception)
        //    {
        //        Assert.True(false);
        //    }

        //}

        //[Fact]
        //public async Task UpdateClientByNonExcistingKeyTest()
        //{
        //    // Arrange
        //    var clientService = new ClientService(_mapper, _dbContext);

        //    Client realClient = FakeDataGenerator.CreateFakeClient();

        //    Client notExistingClient = FakeDataGenerator.CreateFakeClient();

        //    // Act/Assert
        //    try
        //    {
        //        await clientService.AddClientAsync(realClient);
        //        await clientService.UpdateClientAsync(notExistingClient);

        //        await Assert.ThrowsAsync<KeyNotFoundException>(() => clientService.UpdateClientAsync(notExistingClient));

        //    }
        //    catch (Exception)
        //    {
        //        Assert.True(false);
        //    }

        //}

    }
}
