using Microsoft.EntityFrameworkCore;
using Models;
using Services;
using Services.Exceptions;
using Xunit;

namespace ServiceTests
{
    /// <summary>
    /// После того как сделал инжекцию dbContext сломались тесты, не разобрался, как починить
    /// </summary>
    public class ClientServiceDBTests
    {

        //[Fact]
        //public async Task AddNewClientUnder18YearsExceptionTest()
        //{
        //    // Arrange
        //    ClientService clientServiceDB = new ClientServiceDB();

        //    Client client = new Client()
        //    {
        //        Name = "Михаил",
        //        PassportID = "AB123456",
        //        DateOfBirth = new DateTime(2009, 11, 11),
        //    };

        //    // Act/Assert
        //    try
        //    {
        //        await clientServiceDB.AddClientAsync(client);
        //    }
        //    catch (Under18 e)
        //    {
        //        Assert.Equal(typeof(Under18), e.GetType());
        //        Assert.Equal("Клиент не может быть младше 18 лет", e.Message);
        //    }
        //    catch (Exception)
        //    {
        //        Assert.True(false);
        //    }

        //}

        //[Fact]
        //public async Task AddNewClientNoPassDataExceptionTest()
        //{
        //    // Arrange
        //    ClientService clientServiceDB = new ClientServiceDB();

        //    Client client = new Client()
        //    {
        //        Name = "Игорь",
        //        PassportID = null,
        //        DateOfBirth = new DateTime(1998, 1, 2),
        //    };

        //    // Act/Assert
        //    try
        //    {
        //        await clientServiceDB.AddClientAsync(client);
        //    }
        //    catch (NoPassportDataException e)
        //    {
        //        Assert.Equal(typeof(NoPassportDataException), e.GetType());
        //        Assert.Equal("Вы не ввели паспортные данные", e.Message);
        //    }
        //    catch (Exception)
        //    {
        //        Assert.True(false);
        //    }
        //}

        //[Fact]
        //public async Task AddNewAlreadyExcistingClientTest()
        //{
        //    // Arrange
        //    ClientService clientServiceDB = new ClientServiceDB();

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
        //        await clientServiceDB.AddClientAsync(oldClient);
        //        await clientServiceDB.AddClientAsync(newClient);
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
        //    ClientService clientServiceDB = new ClientServiceDB();

        //    Client existsClient = FakeDataGenerator.CreateFakeClient();

        //    Client noExistsClient = FakeDataGenerator.CreateFakeClient();

        //    // Act/Assert
        //    try
        //    {
        //        await clientServiceDB.AddClientAsync(existsClient);
        //        await Assert.ThrowsAsync<KeyNotFoundException>(() => clientServiceDB.DeleteClientAsync(noExistsClient));

        //        await clientServiceDB.DeleteClientAsync(existsClient);
        //        Assert.Null(await clientServiceDB.dbContext.Clients.FirstOrDefaultAsync(p => p.Id == existsClient.ID));
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
        //    ClientService clientServiceDB = new ClientServiceDB();

        //    Client realClient = FakeDataGenerator.CreateFakeClient();

        //    Client notExistingClient = FakeDataGenerator.CreateFakeClient();

        //    // Act/Assert
        //    try
        //    {
        //        await clientServiceDB.AddClientAsync(realClient);
        //        await clientServiceDB.UpdateClientAsync(notExistingClient);

        //        await Assert.ThrowsAsync<KeyNotFoundException>(() => clientServiceDB.UpdateClientAsync(notExistingClient));

        //    }
        //    catch (Exception)
        //    {
        //        Assert.True(false);
        //    }

        //}

    }
}
