using System;
using System.IO;
using System.Linq;

class Program
{
    static string templateName = "LibraryStarter";

    static void Main(string[] args)
    {
        Console.WriteLine("Enter your library name:");
        var libName = Console.ReadLine();

        Console.WriteLine("Enter your template library name, or just enter to default 'LibraryStarter':");
        var templateNameInput = Console.ReadLine();

        if (!string.IsNullOrWhiteSpace(templateNameInput))
            templateName = templateNameInput;

        var parentDir = new DirectoryInfo(".").Parent;
        var libDir = Path.Combine(parentDir.FullName, libName);

        if (!Directory.Exists(libDir))
            Directory.CreateDirectory(libDir);

        CopyDirectory(libName, Path.Combine(parentDir.FullName, templateName));

        Directory.CreateDirectory(Path.Combine(libDir, "src", libName));
        CopyDirectory(libName, Path.Combine(parentDir.FullName, templateName, "src", templateName));
           
        Directory.CreateDirectory(Path.Combine(libDir, "src", $"{libName}.Tests"));
        CopyDirectory(libName, Path.Combine(parentDir.FullName, templateName, "src", $"{templateName}.Tests"));

        Console.WriteLine($"{libName} solution created.");

        Console.Read();
    }

    private static void CopyDirectory(string libName, string directory)
    {
        Directory.GetFiles(directory).ToList().ForEach(a =>
        {
            CopyFile(libName, a);
        });
    }

    private static void CopyFile(string libName, string filePath)
    {
        File.WriteAllText(filePath.Replace(templateName, libName), File.ReadAllText(filePath).Replace(templateName, libName));
    }
}
