using System;
using System.Threading.Tasks;

class Task2
{
    static void Main()
    {
        const double TARGET = 50.0;
        const double EPSILON = 0.01; 
        double[] array = new double[1000];
        for (int i = 0; i < 1000; i++) array[i] = i * 0.1;

        Parallel.For(0, array.Length, (i, state) =>
        {
            double value = array[i];
            
            // Перевірка умови входження в окіл
            if (Math.Abs(value - TARGET) < EPSILON)
            {
                Console.WriteLine($"Знайдено значення {value} на індексі {i}. Зупиняємо цикл.");
                state.Break(); 
            }


            array[i] = Math.Sqrt(value);
        });
    }
}