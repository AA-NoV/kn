using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
}