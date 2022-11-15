using System;
using System.Collections.Generic;
using System.Linq;
using System.Collections;
using System.Diagnostics;

namespace bubbleSort;
class Program
{
    public static int element = 100000;
    static readonly object locker = new object();
    public static int[] nthreads = {1, 2, 3, 4, 6};

    static void Main(string[] args)
    {
        var stopwatch = new Stopwatch();

        //random array generation
        int[] array = new int[element];
        for (var i = 0; i < array.Length; i++)
        {
            Random random = new Random();
            array[i] = random.Next(1, element);
        }
                    
        Console.WriteLine("Question 1: ");
            
        foreach (var n in nthreads)
        {
            stopwatch.Reset();
            stopwatch.Start();
            ParallelSorting(array.ToList(), n);
            stopwatch.Stop();
            Console.Write($"It takes {stopwatch.ElapsedMilliseconds} miliseconds to process {n} threads\n");
        }

        Console.WriteLine($"Completed");
        Console.Read();
    }

    static void BubbleSortList(List<int> array)
    {
        int counter;

        for (var y = 0; y < array.Count - 1; y++)
        {
            for (var i = 0; i < array.Count - 1; i++)
            {
                if (array[i] > array[i + 1])
                {
                    counter = array[i + 1];
                    array[i + 1] = array[i];
                    array[i] = counter;
                }
            }
        }
    }
    static List<int>[] SubListGeneration(List<int> array, int numberOfthreads) 
    {             
        var maximumNumber = array.Max();
        List<int>[] subLists = new List<int>[numberOfthreads];
        var splitFactor = maximumNumber / numberOfthreads;

        for (var j = 0; j < numberOfthreads - 1; j++)
        {
            subLists[j] = new List<int>();

            for (int i = 0; i < array.Count; i++)
            {
                //Choosing values less then splitFactor factor for current sublist
                if (array[i] <= splitFactor * (j + 1))
                {
                    var value = array[i];
                    subLists[j].Add(value);
                }
            }
            //To avoid dublicates remove current value from array                       
            array.RemoveAll(a => subLists[j].Contains(a));
        }
        //add remaining values to the last sublist
        subLists[subLists.Length - 1] = array;
        return subLists;
    }
    
    static int[] ParallelSorting(List<int> array, int numberOfthreads)
    {
        //making sublists
        var subLists = SubListGeneration(array, numberOfthreads);

        //starting threads
        var threads = new List<Thread>();

        for (int i = 0; i < numberOfthreads; i++)
        {
            var list = subLists[i];
            var thread = new Thread(() => BubbleSortList(list));
            thread.Start();
            threads.Add(thread);
        }

        //stopping threads
        foreach (var thread in threads)
        {
            thread.Join();
        }

        //join all sublists to sorted list
        var sortedList = new List<int>();
        foreach (var subList in subLists)
        {
            foreach (var list in subList)
            {
                sortedList.Add(list);
            }
        }

        return sortedList.ToArray();
    }
    
}
