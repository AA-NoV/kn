using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

public class ReaderManager
{
    private List<Reader> Readers = new List<Reader>();
    private int nextId = 1;

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
    }

    public Reader GetReader(int readerId) 
    {
        return Readers.Find(r => r.Id == readerId); 
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
    public void LoadReadersFromJson(string filePath)
    {
        if (File.Exists(filePath))
        {
            string jsonString = File.ReadAllText(filePath);
            Readers = JsonSerializer.Deserialize<List<Reader>>(jsonString) ?? new List<Reader>();
            nextId = Readers.Count > 0 ? Readers[^1].Id + 1 : 1; // Устанавливаем следующий ID
        }
    }

    public void SaveReadersToJson(string filePath)
    {
        string jsonString = JsonSerializer.Serialize(Readers);
        File.WriteAllText(filePath, jsonString);
    }
}