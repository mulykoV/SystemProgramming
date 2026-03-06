using System;
using System.Threading;
using System.Threading.Tasks;

class Program
{
    static void Main(string[] args)
    {
        int n = 10;

        Parallel.Invoke(
            () => {
                long factorial = 1;
                for (int i = 1; i <= 5; i++) factorial *= i;
                Console.WriteLine($"Факторіал 5: {factorial}");
            },
            () => {
                int sum = 0;
                for (int i = 1; i <= n; i++) sum += i;
                Console.WriteLine($"Сума до {n}: {sum}");
            },
            () => {
                Thread.Sleep(300);
                Console.WriteLine("Повідомлення після паузи 300 мс");
            }
        );

        Console.WriteLine("Всі паралельні методи завершені.");
    }
}