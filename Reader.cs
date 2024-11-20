using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class Reader
{
    public string Id { get; set; }
    public string Name { get; set; }
    public string Email { get; set; }
}

public class ReaderManager
{
    private List<Reader> Readers = new List<Reader>;
    public void AddReader(Reader reader)
    {
        Readers.Add(reader);
    }
    public void RemoveReader(string readerId)
    {
        Readers.RemoveAll(r => r.Id == readerId);
    }
    public Reader GetReader(string readerId)
    {
        return Readers.Find(r => r.Id == readerId);
    }
    public List<Reader> GetAllReaders()
    {
        return Readers;
    }
}