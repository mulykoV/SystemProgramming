using System;
using System.Threading.Tasks;

class Program
{
    static void Main()
    {
        Console.OutputEncoding = System.Text.Encoding.UTF8;

        Console.Write("Введіть число N: ");
        if (int.TryParse(Console.ReadLine(), out int n))
        {
            
            Task<int> sumTask = Task.Run(() =>
            {
                int sum = 0;

                for (int i = 1; i <= n; i++)
                {
                    sum += i;
                }

                return sum;
            });

            
            Task printTask = sumTask.ContinueWith(task =>
            {
                Console.WriteLine($"Сума чисел від 1 до {n} = {task.Result}");
            });
            printTask.Wait();
        }
        else
        {
            Console.WriteLine("Помилка: введіть коректне ціле число.");
        }
    }
}