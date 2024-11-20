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
        ReaderManager readerManager = new ReaderManager();

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
                    string name;
                    while (true)
                    {
                        Console.Write("Введите имя читателя: ");
                        name = Console.ReadLine();

                        if (readerManager.IsValidName(name))
                        {
                            break;
                        }
                        else
                        {
                            Console.WriteLine("Ошибка: Имя может содержать только буквы. Пожалуйста, попробуйте снова.");
                        }
                    }

                    string email;
                    while (true)
                    {
                        Console.Write("Введите Email читателя: ");
                        email = Console.ReadLine();

                        if (readerManager.IsValidEmail(email))
                        {
                            break;
                        }
                        else
                        {
                            Console.WriteLine("Ошибка: Некорректный формат email. Пожалуйста, попробуйте снова.");
                        }
                    }

                    // Добавление нового читателя
                    Reader newReader = readerManager.AddReader(name, email);
                    Console.WriteLine($"Читатель добавлен: ID: {newReader.Id}");

                    // Сохранение списка читателей в JSON
                    readerManager.SaveReadersToJson(jsonFilePath); 
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
}