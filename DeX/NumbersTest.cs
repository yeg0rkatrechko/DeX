using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeX
{
    public class NumbersTest
    {
        public delegate void EventHandler(string message);
        public static event EventHandler DifferenceSpotted;
        public static void NumbersAnalysis (int[] array, double percentage)
        {
            Console.WriteLine("Going through array...");
            Console.WriteLine($"First value is {array[0]}");
            for (int i = 1; i < array.Length; i++) 
            {
                Console.WriteLine($"Current value is {array[i]}. It's №{i+1} in array");
                double difference = 1;
                double coef = 100;
                difference = array[i] / array[i - 1];
                if (difference > coef/percentage)
                {
                    DifferenceSpotted?.Invoke($"Number №{i + 1} is more than {percentage}% bigger than №{i}");
                }
                Thread.Sleep(700);
            }
        }
    }
}
