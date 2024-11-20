using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class ReaderManager
{
    private List<Reader> Readers = new List<Reader>();
    public bool AddReader(Reader reader)
    {
        if (Readers.Exists(r => r.Id == reader.Id))
        {
            return false;
        }
        Readers.Add(reader);
        return true;
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