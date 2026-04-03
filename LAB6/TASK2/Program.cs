using System;
using System.IO;

class FileSearcher
{
    static void Main()
    {
        Console.Write("Введіть шлях для пошуку: ");
        string rootPath = Console.ReadLine() ?? "/home/volodymyr";
        Console.Write("Введіть ім'я файлу (повне або часткове): ");
        string fileName = Console.ReadLine() ?? "";

        SearchFiles(rootPath, fileName);
    }

    static void SearchFiles(string path, string target)
    {
        try
        {
            foreach (string f in Directory.GetFiles(path, $"*{target}*"))
            {
                FileInfo info = new FileInfo(f);
                Console.WriteLine($"\nЗнайдено: {info.FullName}");
                Console.WriteLine($"Розмір: {info.Length / 1024.0:F2} KB");
                Console.WriteLine($"Створено: {info.CreationTime}");
            }

            foreach (string d in Directory.GetDirectories(path))
                SearchFiles(d, target);
        }
        catch (UnauthorizedAccessException)
        {
            Console.WriteLine($"Немає доступу: {path}");
        }
        catch (DirectoryNotFoundException)
        {
            Console.WriteLine($"Директорію не знайдено: {path}");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Помилка в {path}: {ex.Message}");
        }
    }
}