using System;
using System.Collections.Generic;
using System.Threading.Tasks;

class Task4
{
    static void Main()
    {
        // Створюємо колекцію даних (як у Завданні 3)
        List<double> dataList = new List<double>();
        for (int i = 1; i <= 1000; i++) 
        {
            dataList.Add(i); 
        }

        Console.WriteLine("Запуск Parallel.ForEach з лямбда-виразом...");

        Parallel.ForEach(dataList, x => 
        {
            double exponent = Math.PI * x;
            double numerator = Math.Exp(exponent);
            double denominator = Math.Pow(x, Math.PI);
            
            double result = numerator / denominator;

            if (x % 200 == 0)
            {
                Console.WriteLine($"Вхідне значення: {x:F0}, Результат: {result:E2}");
            }
        });

        Console.WriteLine("\nПрограму завершено. Тіло циклу було задане лямбда-виразом.");
    }
}