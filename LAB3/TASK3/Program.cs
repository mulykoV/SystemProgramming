using System;
using System.Collections.Generic;
using System.Threading.Tasks;

class Task3
{
    static void Main()
    {
        List<double> list = new List<double>();
        for (int i = 1; i <= 1000; i++) list.Add(i);

        Parallel.ForEach(list, item =>
        {
            // Обчислення x = e^x / x^PI
            double result = Math.Exp(item) / Math.Pow(item, Math.PI);
            if (item % 100 == 0) Console.WriteLine($"Елемент {item}: {result}");
        });
    }
}