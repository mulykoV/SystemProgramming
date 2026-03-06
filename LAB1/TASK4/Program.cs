using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

class Program
{
    static volatile bool isRunning = true; 
    
    static long[] counts;
    static Thread[] threads;
    static ThreadPriority[] priorities;

    static void Main()
    {
        Console.OutputEncoding = System.Text.Encoding.UTF8;
        
        Console.Write("Введіть кількість потоків: ");
        if (!int.TryParse(Console.ReadLine(), out int threadCount) || threadCount <= 0) 
        {
            Console.WriteLine("Некоректна кількість потоків.");
            return;
        }

        counts = new long[threadCount];
        threads = new Thread[threadCount];
        priorities = new ThreadPriority[threadCount];

        for (int i = 0; i < threadCount; i++)
        {
            Console.WriteLine($"\nНалаштування для потоку #{i}:");
            Console.WriteLine("0 - Lowest, 1 - BelowNormal, 2 - Normal, 3 - AboveNormal, 4 - Highest");
            Console.Write("Виберіть пріоритет (за замовчуванням 2): ");
            
            if (!int.TryParse(Console.ReadLine(), out int p)) p = 2;
            if (p < 0 || p > 4) p = 2;
            
            priorities[i] = (ThreadPriority)p;

            int index = i;
            threads[i] = new Thread(() =>
            {
                while (isRunning)
                {
                    counts[index]++;
                }
            });
            threads[i].Priority = priorities[i];
            threads[i].Name = $"Thread_{i}";
        }

        Console.Clear();
        Console.WriteLine("Потоки запущено. Натисніть будь-яку клавішу для зупинки та виводу таблиці...\n");

        foreach (var t in threads) t.Start();

        // Потік для відображення прогресу в реальному часі
        Thread monitorThread = new Thread(() =>
        {
            while (isRunning)
            {
                try {
                    Console.SetCursorPosition(0, 2);
                    for (int i = 0; i < threadCount; i++)
                    {
                        Console.WriteLine($"Потік {i} [{priorities[i]}]: {counts[i]:N0} ітерацій | Стан: {threads[i].ThreadState}      ");
                    }
                } catch { }
                
                Thread.Sleep(100);
            }
        });
        monitorThread.Start();

        // Чекаємо на натискання клавіші користувачем
        Console.ReadKey(true);
        isRunning = false;

        Console.WriteLine("\nЗупинка потоків... Зачекайте.");

        // Чекаємо завершення всіх потоків
        foreach (var t in threads) t.Join();
        monitorThread.Join();

        DisplayResults(threadCount);
    }

    static void DisplayResults(int count)
    {
        long total = counts.Sum();
        Console.WriteLine("\n" + new string('=', 65));
        Console.WriteLine($"{"ID",-4} | {"Пріоритет",-15} | {"Кількість ітерацій",-20} | {"Час ЦП %",-10}");
        Console.WriteLine(new string('-', 65));

        for (int i = 0; i < count; i++)
        {
            double percent = total > 0 ? (double)counts[i] / total * 100 : 0;
            Console.WriteLine($"{i,-4} | {priorities[i],-15} | {counts[i],-20:N0} | {percent,8:F2}%");
        }
        Console.WriteLine(new string('=', 65));
        Console.WriteLine("Натисніть будь-яку клавішу для виходу...");
        Console.ReadKey();
    }
}