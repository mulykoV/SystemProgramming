using System;
using System.Threading;

namespace LabWork
{
    class Program
    {
        static void Main(string[] args)
        {
            // Створюємо перший потік для чисел
            Thread thread1 = new Thread(PrintNumbers);
            thread1.Name = "ЧисловийПотік";
            
            // Створюємо другий потік для літер
            Thread thread2 = new Thread(PrintLetters);
            thread2.Name = "БуквенийПотік";

            // Запускаємо потоки методом Start()
            thread1.Start();
            thread2.Start();

            // Чекаємо завершення 
            thread1.Join();
            thread2.Join();
            Console.WriteLine("\nОбидва потоки завершили роботу.");
        }

        static void PrintNumbers()
        {
            for (int i = 1; i <= 40; i++)
            {
                Console.WriteLine($"[{Thread.CurrentThread.Name}] Число: {i}");
                Thread.Sleep(200); // Пауза 200 мс
            }
        }

        static void PrintLetters()
        {
            for (char c = 'A'; c <= 'Z'; c++)
            {
                Console.WriteLine($"[{Thread.CurrentThread.Name}] Літера: {c}");
                Thread.Sleep(300); // Пауза 300 мс
            }
        }
    }
}