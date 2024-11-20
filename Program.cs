using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.ConstrainedExecution;
using System.Text;
using System.Threading.Tasks;

class Program
{
    static void Main(string[] args)
    {
        ReaderManager readerManager = new ReaderManager();
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

            string choise = Console.ReadLine();
            switch (choise)
            {
                case "1":
                    Reader newReader = new Reader();
                    Console.Write("Введите ID читателя:");
                    newReader.Id = Console.ReadLine();
                    Console.Write("Введите имя читателя:");
                    newReader.Name = Console.ReadLine();
                    Console.Write("Введите Email читателя:");
                    newReader.Email = Console.ReadLine();

                    if (readerManager.AddReader(newReader))
                    {
                        Console.WriteLine("Читатель добавлен");
                    }
                    else
                    {
                        Console.WriteLine($"Читатель с ID {newReader.Id} уже существует.");
                    }

                    break;

                case "2":
                    Console.Write("Введите ID читателя для удаления:");
                    string readerIdToRemove = Console.ReadLine();
                    readerManager.RemoveReader(readerIdToRemove);
                    Console.WriteLine($"Читатель с ID {readerIdToRemove} удален");
                    break;

                case "3":
                    Console.Write("Введите ID читателя для просмотра:");
                    string readerIdToView = Console.ReadLine();
                    Reader foundReader = readerManager.GetReader(readerIdToView);
                    if (foundReader != null)
                    {
                        Console.WriteLine($"ID: {foundReader.Id}, Имя: {foundReader.Name}, Email: {foundReader.Email}");
                    }
                    else
                    {
                        Console.WriteLine("Читатель не найден");
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

                        default:
                            Console.WriteLine("Некорректный выбор, попробуйте снова.");
                        break;
                    }
            }
            Console.WriteLine("Вы вышли из программы.");
        }
    }