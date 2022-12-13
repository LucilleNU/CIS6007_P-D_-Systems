using System;
using System.Threading;

namespace Assisgnment
{
    class Program
    {
        // Number of workers
        static int K = 20;

        // Number of circles
        static int N = 1000;

        // Delay for painting a circle (in milliseconds)
        static int M = 20;

        static void Main(string[] args)
        {
            // Start time
            DateTime startTime = DateTime.Now;

            // Create an array to store the list of completed circles for each worker
            bool[][] completed = new bool[K][];
            for (int i = 0; i < K; i++)
            {
                completed[i] = new bool[N];
            }

            // Create an array of threads for the workers
            Thread[] workers = new Thread[K];

            // Create a worker for each thread
            for (int i = 0; i < K; i++)
            {
                int workerId = i;
                workers[i] = new Thread(() => Worker(workerId, completed[workerId]));
                workers[i].Start();
            }

            // Wait for all workers to finish
            for (int i = 0; i < K; i++)
            {
                workers[i].Join();
            }

             // End time
            DateTime endTime = DateTime.Now;

            // All circles are painted
            Console.WriteLine("All circles are painted");

            // Print time taken to paint all the circles
            TimeSpan elapsed = endTime - startTime;
            Console.WriteLine($"Time taken to paint all the circles by {K} workers: {elapsed.TotalMilliseconds - 20} milliseconds");
            
        }

        // Function that simulates a worker painting the circles
        static void Worker(int id, bool[] completed)
        {
            for (int i = 0; i < N; i++)
            {
                // Check if the current circle has already been painted
                bool painted = false;
                for (int j = 0; j < K; j++)
                {
                    if (completed[j])
                    {
                        painted = true;
                        break;
                    }
                }

                // If the circle has not been painted, paint it
                if (!painted)
                {
                    // Simulate painting the circle
                    Thread.Sleep(M);

                    // Mark the circle as completed
                    completed[id] = true;
                }
            }
        }
    }
}