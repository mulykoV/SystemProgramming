using System;
using System.Threading;

class Program
{
    static void Main()
    {
        // Пріоритетні потоки за замовчуванням
        Thread t1 = new Thread(() => Count(5));
        Thread t2 = new Thread(() => Count(5));
        
        // Фоновий потік 
        Thread bgThread = new Thread(InfiniteLoop);
        bgThread.IsBackground = true; 

        t1.Start();
        t2.Start();
        bgThread.Start();

        Console.WriteLine("Основний потік завершено.");
    }

    static void Count(int max)
    {
        for (int i = 1; i <= max; i++)
        {
            Console.WriteLine($"Пріоритетний потік працює: {i}");
            Thread.Sleep(500);
        }
    }

    static void InfiniteLoop()
    {
        while (true)
        {
            Console.WriteLine("--- Фоновий потік працює вічно... ---");
            Thread.Sleep(100);
        }
    }
}