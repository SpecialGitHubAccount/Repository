using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;


/// <summary>
/// Задания были выполнены под паттерн "Стратегия":
/// FileSystemVisitorBase - абстракный класс - стратегии.
/// В программе имеются 2 стратегии у которых разные методы GetAllItems:
/// fileSystemVisitor - для нормальной работы.
/// fakeFileVisitor - для библиотеки тестов.
/// FilevisitorContext - контекст стратегии.
/// </summary>
namespace Task1_2
{
    public class Program
    {
        const string PATH = @"C:\Windows";

        public static void Main(string[] args)
        {
            Run();

            Console.WriteLine("Result:");

            ShowResult();            

            Console.ReadLine();
        }

        static void ShowResult()
        {
            Console.WriteLine("All found files or directories");

            foreach (var item in allFoundFilesAndDirectories)
            {
                Console.WriteLine(item);
            }
        }

        static void InputYesOrNo(out string dialogString)
        {
            do
            {
                Console.WriteLine("Enter Y or N");
                dialogString = Console.ReadLine();
            }
            while (!Regex.IsMatch(dialogString, @"(Y|y|N|n)"));
        }

        static void InputFileExtension(out string dialogString)
        {
            do
            {
                Console.WriteLine("Enter extension");
                dialogString = Console.ReadLine();
            }
            while (!Regex.IsMatch(dialogString, @".([a-z])+"));
        }

        static FileVisitorContext CreateFileVisitorContext()
        {
            fileVisitor = null;
            string dialogString = string.Empty;
            // Для выхода из меню.
            bool isExit = false;
            Func<string, bool> fileExtensionFilter = null;

            while (!isExit)
            {
                Console.WriteLine("1 - select extension of file");
                Console.WriteLine("2 - go");
                dialogString = Console.ReadLine();

                switch (dialogString)
                {
                    case "1":                        
                        Console.WriteLine("Set extension of file");
                        string fileExtension = string.Empty;
                        InputFileExtension(out fileExtension);
                        fileExtensionFilter = (s) => s.EndsWith(fileExtension);
                        break;
                    case "2":
                        isExit = true;
                        break;
                    default:
                        Console.WriteLine("You have to select a value from 1 to 2");
                        break;
                }
            }
            
            if (fileExtensionFilter != null)
            {
                fileVisitor = new FileSystemVisitor(PATH, fileExtensionFilter);
                fileVisitor.Start += OnStart;        
                fileVisitor.FilteredFileFinded += OnFileOrDirectoryFound;
                fileVisitor.FilteredDirectoryFinded += OnFileOrDirectoryFound;
                fileVisitor.End += OnEnd;
            }
            else
            {
                fileVisitor = new FileSystemVisitor(PATH);
                fileVisitor.Start += OnStart;
                fileVisitor.FileFinded += OnFileOrDirectoryFound;
                fileVisitor.DirectoryFinded += OnFileOrDirectoryFound;
                fileVisitor.End += OnEnd;
            }
            
            return new FileVisitorContext(fileVisitor);
        }

        static void Run()
        {
            visitorContext = CreateFileVisitorContext();

            if (visitorContext != null)
            {
                foreach (string item in visitorContext.GetAllFilesAndDirectories())
                {
                    allFoundFilesAndDirectories.Add(item.ToString());
                }
            }
        }

        static void OnStart(object sender, EventArgs e)
        {
            Console.WriteLine("Started");
        }
        static void OnFileOrDirectoryFound(object sender, FileOrDirectoryFindedEventArgs e)
        {
            string dialog = string.Empty;
            Console.WriteLine(e.itemName + " finded");
            Console.WriteLine("Do you want to stop finding?");

            InputYesOrNo(out dialog);

            if (dialog.ToLower() == "y")
            {
                e.isBreak = true;
            }
            
            Console.WriteLine("Do you want to exclude this file?");

            InputYesOrNo(out dialog);

            if (dialog.ToLower() == "y")
            {
                e.isExclude = true;
            }           
        }
        static void OnEnd(object sender, EventArgs e)
        {
            Console.WriteLine("Finished");
        }

        static List<string> allFoundFilesAndDirectories = new List<string>();

        static FileVisitorContext visitorContext = null;

        static FileSystemVisitor fileVisitor = null;
    }
}