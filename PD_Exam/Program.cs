using System;
using System.Linq;
using System.Threading.Tasks;

namespace PD_Exam
{
    public static class Program
    {
        public static void Main()
        {
            // Setting constants
            const int N = 100;
            const int max = 10000;

            // Creating an unsorted array of numbers
            int[] numbers = Enumerable.Range(0, max).ToArray();

            // Shuffleing the array to make it unsorted
            Random random = new Random();
            for (int i = 0; i < numbers.Length; i++)
            {
                int j = random.Next(i, numbers.Length);
                int temp = numbers[i];
                numbers[i] = numbers[j];
                numbers[j] = temp;
            }

            // Find a number bigger than N using a parallel algorithm
            int output = FindNumberBiggerThanN(numbers, N);

            // Print or display the output
            Console.WriteLine($"Found a number bigger than {N}: {output}");
        }

        public static int FindNumberBiggerThanN(int[] numbers, int N)
        {
            // Use Parallel.For to search for a number bigger than N in parallel
            int output = 0;
            Parallel.For(0, numbers.Length, (i) =>
            {
                // If a number is found, set the output and exit the loop
                if (numbers[i] > N)
                {
                    output = numbers[i];
                    return;
                }
            });
            return output;
        }
    }
}

