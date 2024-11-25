using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;

public class JsonFileManager
{
    private const string FilePath = "readers.json"; // Исправлено имя файла

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
        return JsonConvert.DeserializeObject<List<Reader>>(json) ?? new List<Reader>();
    }
}