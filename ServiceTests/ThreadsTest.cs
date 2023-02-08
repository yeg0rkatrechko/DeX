using Models;
using DbModels;
using Services;
using Xunit;
using Xunit.Abstractions;

namespace ServiceTests
{

    public class ThreadsTest
    {
        object locker = new object();

        private ITestOutputHelper _output;

        public ThreadsTest(ITestOutputHelper output)
        {
            _output = output;
        }

        [Fact]
        public void RefillAccount()
        {
            var account = new Account { Amount = 0 };
            void Refill()
            {
                Random rnd = new Random();
                int input;
                for (int i = 0; i < 10; i++)
                {
                    lock (locker)
                    {
                        input = rnd.Next(100, 1000);
                        account.Amount += input;
                        _output.WriteLine($"{Thread.CurrentThread.Name} пополнил счет на {input}");
                        _output.WriteLine($"На счету: {account.Amount}");
                    }
                    Thread.Sleep(1000);
                }
            }
            var firstThread = new Thread(Refill) { Name = "Первый поток" };
            var secondThread = new Thread(Refill) { Name = "Второй поток" };
            firstThread.Start();
            secondThread.Start();
            Thread.Sleep(10000);
        }
    }
}