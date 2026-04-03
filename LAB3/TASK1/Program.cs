using System;
using System.Diagnostics;
using System.Threading.Tasks;

class Task1
{
    static void Main()
    {
        int size = 100000000;
        double[] data = new double[size];

        // Заповнюємо початковими даними
        for (int i = 0; i < size; i++) data[i] = i + 1;

        Stopwatch sw = Stopwatch.StartNew();

        Parallel.For(0, size, i =>
        {
            // Складність 1: x = x / 10
            // data[i] = data[i] / 10;  

            // Складність 2: x = x / PI
            // data[i] = data[i] / Math.PI;

            // Складність 3: x = e^x / x^PI
            // data[i] = Math.Exp(data[i]) / Math.Pow(data[i], Math.PI);

            // Складність 4: x = e^(PI*x) / x^PI
            data[i] = Math.Exp(Math.PI * data[i]) / Math.Pow(data[i], Math.PI);
        });

        sw.Stop();
        Console.WriteLine($"Завдання 1 завершено за: {sw.ElapsedMilliseconds} мс");
    }
}