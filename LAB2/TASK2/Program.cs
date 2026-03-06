using System;
using System.Threading.Tasks;

class Program
{
    static void Main(string[] args)
    {
        Task[] tasks = new Task[3];

        for (int i = 0; i < 3; i++)
        {
            tasks[i] = Task.Run(() =>
            {
                for (int j = 1; j <= 5; j++)
                {
                    Console.WriteLine($"Task Id: {Task.CurrentId} рахує: {j}");
                }
            });
            Console.WriteLine($"Запущено задачу з Id: {tasks[i].Id}");
        }

        Task.WaitAll(tasks);
    }
}
