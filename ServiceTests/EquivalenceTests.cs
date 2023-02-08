using Models;
using Services;
using Xunit;
namespace ServiceTests
{
    public class EquivalenceTests
    {
        [Fact]
        public static void GetHashCodeNecessityPositivTest()
        {
            var testDictionary = FakeDataGenerator.CreateAccountDictionary();
            var firstAccount = testDictionary.First();
            Client testClient = new Client("AB12345", new DateTime(1998, 10, 30), "Yegor Katrechko");
            Assert.Equal(firstAccount.Key, testClient);
        }
        [Fact]
        public void GetHashCodeNecessityPositivTestTwo()
        {
            //Arrange
            Dictionary<Client, List<Account>> accounDictionary = FakeDataGenerator.CreateAccountDictionary();
            var firstAccount = accounDictionary.First();

            Client client = new Client
            {
                Name = firstAccount.Key.Name,
                PassportID = firstAccount.Key.PassportID,
                DateOfBirth = firstAccount.Key.DateOfBirth,
            };

            //Act
            var expectedAccount = accounDictionary[firstAccount.Key];
            var actualAccount = accounDictionary[client];

            //Assert
            Assert.Equal(expectedAccount, actualAccount);
        }
    }
   
}