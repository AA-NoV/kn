using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.ConstrainedExecution;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

class Program
{
    static void Main(string[] args)
    {
        JsonFileManager jsonFileManager = new JsonFileManager();
        List<Reader> readers = jsonFileManager.LoadReaders();
        int nextId = readers.Count > 0 ? readers.Max(r => r.Id) + 1 : 1;

        bool running = true;

        while (running)
        {
            Console.WriteLine("\nВыберите действие:");
            Console.WriteLine("1. Добавить читателя");
            Console.WriteLine("2. Удалить читателя");
            Console.WriteLine("3. Просмотреть читателя по ID");
            Console.WriteLine("4. Просмотреть всех читателей");
            Console.WriteLine("5. Выход");
            Console.WriteLine("Ваш выбор:");

            string choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    Console.Write("Введите имя читателя: ");
                    string name = Console.ReadLine();
                    Console.Write("Введите Email читателя: ");
                    string email = Console.ReadLine();

                    if (IsValidEmail(email))
                    {
                        Reader newReader = new Reader { Id = nextId++, Name = name, Email = email };
                        readers.Add(newReader);
                        jsonFileManager.SaveReaders(readers);
                        Console.WriteLine($"Читатель добавлен: ID: {newReader.Id}");
                    }
                    else
                    {
                        Console.WriteLine("Некорректный формат email.");
                    }
                    break;

                case "2":
                    Console.Write("Введите ID читателя для удаления: ");
                    string readerIdToRemoveStr = Console.ReadLine();
                    if (int.TryParse(readerIdToRemoveStr, out int readerIdToRemove))
                    {
                        var readerToRemove = readers.FirstOrDefault(r => r.Id == readerIdToRemove);
                        if (readerToRemove != null)
                        {
                            readers.Remove(readerToRemove);
                            jsonFileManager.SaveReaders(readers);
                            Console.WriteLine($"Читатель с ID {readerIdToRemove} удален.");
                        }
                        else
                        {
                            Console.WriteLine("Читатель не найден.");
                        }
                    }
                    else
                    {
                        Console.WriteLine("Некорректный ID.");
                    }
                    break;

                case "3":
                    Console.Write("Введите ID читателя для просмотра: ");
                    string readerIdToViewStr = Console.ReadLine();
                    if (int.TryParse(readerIdToViewStr, out int readerIdToView))
                    {
                        var foundReader = readers.FirstOrDefault(r => r.Id == readerIdToView);
                        if (foundReader != null)
                        {
                            Console.WriteLine($"ID: {foundReader.Id}, Имя: {foundReader.Name}, Email: {foundReader.Email}");
                        }
                        else
                        {
                            Console.WriteLine("Читатель не найден.");
                        }
                    }
                    else
                    {
                        Console.WriteLine("Некорректный ID.");
                    }
                    break;

                case "4":
                    if (readers.Count > 0)
                    {
                        Console.WriteLine("Список всех читателей:");
                        foreach (var reader in readers)
                        {
                            Console.WriteLine($"ID: {reader.Id}, Имя: {reader.Name}, Email: {reader.Email}");
                        }
                    }
                    else
                    {
                        Console.WriteLine("Нет зарегистрированных читателей.");
                    }
                    break;

                case "5":
                    running = false;
                    break;

                default:
                    Console.WriteLine("Некорректный выбор, попробуйте снова.");
                    break;
            }
        }

        Console.WriteLine("Вы вышли из программы.");
    }

    static bool IsValidEmail(string email)
    {
        var emailRegex = new Regex(@"^[^@\s]+@[^@\s]+\.[^@\s]+$");
        return emailRegex.IsMatch(email);
    }
}