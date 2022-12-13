using System;
using System.Threading;

namespace CirclePaintingSimulator
{
    class Program
    {
        static void Main(string[] args)
        {
            // Start time
            DateTime startTime = DateTime.Now;
            
            // Set the number of workers and the speed at which they move
            int k = 20;
            int m = 10;

            // Set the number of circles and the time it takes to paint a circle
            int n = 1000;
            int paintingDelay = 20;
            
            Console.WriteLine($"{k} workers are assigned");
            Console.WriteLine("----------");
            

            // Generate the coordinates of the circle centers
            Random random = new Random();
            int[,] circleCenters = new int[n, 2];
            for (int i = 0; i < n; i++)
            {
                circleCenters[i, 0] = random.Next(0, 100);
                circleCenters[i, 1] = random.Next(0, 100);
            }

            // Create the workers and assign them their sections of the field
            Worker[] workers = new Worker[k];
            int sectionSize = n / k;
            for (int i = 0; i < k; i++)
            {
                int start = i * sectionSize;
                int end = (i + 1) * sectionSize - 1;
                workers[i] = new Worker(i, m, circleCenters, start, end, paintingDelay);
            }

            // Start the workers
            foreach (Worker worker in workers)
            {
                worker.Start();
            }

            // Wait for all the workers to finish
            foreach (Worker worker in workers)
            {
                worker.Join();
            }
            
            Console.WriteLine("----------");
            
            // End time
            DateTime endTime = DateTime.Now;

            // Print a message indicating that all the circles have been painted
            Console.WriteLine("All circles have been painted!");
            
            // Print time taken to paint all the circles
            TimeSpan elapsed = endTime - startTime;
            Console.WriteLine($"Time taken to paint all the circles: {elapsed.TotalMilliseconds - 20} milliseconds");
        }
    }

    class Worker
   {
        private readonly int id;
        private readonly int speed;
        private readonly int[,] circleCenters;
        private readonly int startIndex;
        private readonly int endIndex;
        private readonly int paintingDelay;
        private readonly Thread thread;

        public Worker(int id, int speed, int[,] circleCenters, int startIndex, int endIndex, int paintingDelay)
        {
            this.id = id;
            this.speed = speed;
            this.circleCenters = circleCenters;
            this.startIndex = startIndex;
            this.endIndex = endIndex;
            this.paintingDelay = paintingDelay;

            thread = new Thread(new ThreadStart(this.Run));
        }

        public void Start()
        {
            thread.Start();
        }

        public void Join()
        {
            thread.Join();
        }
        private void Run()
        {
            Console.WriteLine($"Worker {id} started");

            for (int i = startIndex; i <= endIndex; i++)
            {
                // Paint the circle at the given coordinates
                int x = circleCenters[i, 0];
                int y = circleCenters[i, 1];
                //Console.WriteLine($"Worker {id} painting circle at ({x}, {y}//)");

                // Simulate the time it takes to paint the circle
                Thread.Sleep(paintingDelay);
            }

            Console.WriteLine($"Worker {id} finished");
        }
    }
}