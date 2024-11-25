using System;
using System.Collections.Generic;
using System.Linq;

class Program
{
    static void Main(string[] args)
    {
        ReaderManager readerManager = new ReaderManager();
        JsonFileManager jsonFileManager = new JsonFileManager();

        // Загрузка читателей из JSON
        List<Reader> readers = jsonFileManager.LoadReaders();
        readerManager.LoadReaders(readers); // Передаем загруженных читателей в ReaderManager

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
                    jsonFileManager.SaveReaders(readerManager.GetAllReaders()); // Сохранение списка читателей в JSON
                    Console.WriteLine($"Читатель добавлен: ID: {newReader.Id}");
                    break;

                case "2":
                    Console.Write("Введите ID читателя для удаления: ");
                    if (int.TryParse(Console.ReadLine(), out int readerIdToRemove))
                    {
                        readerManager.RemoveReader(readerIdToRemove);
                        jsonFileManager.SaveReaders(readerManager.GetAllReaders());
                        Console.WriteLine($"Читатель с ID {readerIdToRemove} удален.");
                    }
                    else
                    {
                        Console.WriteLine("Некорректный ID.");
                    }
                    break;

                case "3":
                    Console.Write("Введите ID читателя для просмотра: ");
                    if (int.TryParse(Console.ReadLine(), out int readerIdToView))
                    {
                        var foundReader = readerManager.GetReader(readerIdToView);
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
                    var allReaders = readerManager.GetAllReaders();
                    if (allReaders.Count > 0)
                    {
                        Console.WriteLine("Список всех читателей:");
                        foreach (var reader in allReaders)
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