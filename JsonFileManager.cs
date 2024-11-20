using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

public class JsonFileManager
{
    private const string FilePath = "reafers.json";
    public void SaveReaders(List<Reader> readers)
    {
        var json = JsonConvert.SerializeObject(readers, Formatting.Indented);
        File.WriteAllText(FilePath, json);
    }
    public List<Reader> LoadReaders()
    {
        if (!File.Exists(FilePath))
        {
            return new List<Reader>();
        }
        var json = File.ReadAllText(FilePath);
        return JsonConvert.DeserializeObject<List<Reader>>(json);
    }
}

