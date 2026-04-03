using System;
using System.IO;

class Program
{
    static void Main(string[] args)
    {
        Console.Write("Введіть шлях до директорії: ");
        string path = Console.ReadLine() ?? "";

        if (Directory.Exists(path))
            PrintDirectory(path, 0);
        else
            Console.WriteLine("Директорія не знайдена.");
    }

    static void PrintDirectory(string path, int indent)
    {
        var dirInfo = new DirectoryInfo(path);
        Console.WriteLine(new string(' ', indent * 2) + "\U0001F4C1 Folder " + dirInfo.Name);

        try
        {
            foreach (var file in dirInfo.GetFiles())
                Console.WriteLine(new string(' ', (indent + 1) * 2) + "\U0001F4C4 File" + file.Name);

            foreach (var dir in dirInfo.GetDirectories())
                PrintDirectory(dir.FullName, indent + 1);
        }
        catch (UnauthorizedAccessException)
        {
            Console.WriteLine(new string(' ', (indent + 1) * 2) + "Немає доступу");
        }
        catch (Exception ex)
        {
            Console.WriteLine(new string(' ', (indent + 1) * 2) + "Помилка: " + ex.Message);
        }
    }
}