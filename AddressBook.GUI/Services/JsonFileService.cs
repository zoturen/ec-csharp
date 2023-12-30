using System.Collections;
using AddressBook.Core.Models;
using AddressBook.Core.Services;
using Newtonsoft.Json;

namespace AddressBook.GUI.Services;

public class JsonFileService<T> : IFileService<T> where T : IRoot
{
    public Task<bool> SaveToFileAsync(IEnumerable entities, string fileName)
    {
        try
        {
            var targetFile = Path.Combine(FileSystem.Current.AppDataDirectory, fileName);
            var jsonString = JsonConvert.SerializeObject(entities);
            File.WriteAllText(targetFile, jsonString);
            return Task.FromResult(true);
        }
        catch (Exception e)
        {
            Console.WriteLine($"An error occurred writing file: {e.Message}");
            return Task.FromResult(false);
        }
    }

    public Task<List<T>> ReadFromFileAsync(string fileName)
    {
        try
        {
            var targetFile = Path.Combine(FileSystem.Current.AppDataDirectory, fileName);
            using var inputStream = File.OpenRead(targetFile);
            using var streamReader = new StreamReader(inputStream);
            var jsonString = streamReader.ReadToEnd();
            return Task.FromResult(JsonConvert.DeserializeObject<List<T>>(jsonString) ?? []);
        }
        catch (Exception e)
        {
            Console.WriteLine($"An error occurred reading file: {e.Message}");
            return Task.FromResult(new List<T>());
        }
    }
}