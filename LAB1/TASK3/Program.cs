using System;
using System.Threading;

class Program
{
    static bool stop = false;
    static long[] counts = new long[3];

    static void Main()
    {
        Thread[] threads = new Thread[3];
        ThreadPriority[] priorities = { ThreadPriority.Lowest, ThreadPriority.AboveNormal, ThreadPriority.Highest };

        for (int i = 0; i < 3; i++)
        {
            int index = i;
            threads[i] = new Thread(() => {
                while (!stop) { counts[index]++; }
            });
            threads[i].Priority = priorities[i];
        }

        foreach (var t in threads) t.Start();
        
        Thread.Sleep(10000); // Даємо 5 секунд на роботу
        stop = true;

        long total = 0;
        foreach (var c in counts) total += c;

        Console.WriteLine("\nРезультати:");
        for (int i = 0; i < 3; i++)
        {
            double percent = (double)counts[i] / total * 100;
            Console.WriteLine($"Потік {i} ({priorities[i]}): {counts[i]} ітерацій ({percent:F2}%)");
        }
    }
}