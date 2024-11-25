using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

public class ReaderManager
{
    private List<Reader> Readers = new List<Reader>();
    private int nextId;

    public void LoadReaders(List<Reader> readers)
    {
        Readers = readers;
        nextId = Readers.Count > 0 ? Readers.Max(r => r.Id) + 1 : 1; // Устанавливаем следующий ID
    }

    public Reader AddReader(string name, string email)
    {
        Reader newReader = new Reader()
        {
            Id = nextId++,
            Name = name,
            Email = email
        };
        Readers.Add(newReader);
        return newReader;
    }

    public void RemoveReader(int readerId)
    {
        Readers.RemoveAll(r => r.Id == readerId);
        if (Readers.Count == 0)
        {
            nextId = 1; // Сбросить nextId, если больше нет читателей
        }
    }

    public Reader GetReader(int readerId)
    {
        return Readers.FirstOrDefault(r => r.Id == readerId);
    }

    public List<Reader> GetAllReaders()
    {
        return Readers;
    }

    public bool IsValidEmail(string email)
    {
        var emailRegex = new Regex(@"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$");
        return emailRegex.IsMatch(email);
    }

    public bool IsValidName(string name)
    {
        var nameRegex = new Regex(@"^[A-Za-zА-Яа-яЁё]+$");
        return nameRegex.IsMatch(name);
    }
}