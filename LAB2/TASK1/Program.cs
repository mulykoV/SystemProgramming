using System;
using System.Threading;
using System.Threading.Tasks;

class Program
{
    static void Main(string[] args)
    {
        Task task1 = new Task(() => 
        {
            for (int i = 1; i <= 10; i++)
            {
                Console.Write($"{i} ");
                Thread.Sleep(200);
            }
        });

        Task task2 = new Task(() => 
        {
            for (char c = 'A'; c <= 'J'; c++)
            {
                Console.Write($"{c} ");
                Thread.Sleep(200);
            }
        });

        task1.Start();
        task2.Start();

        Task.WaitAll(task1, task2);

        Console.WriteLine("\nОбидві задачі завершено.");
    }
}
