using System.Collections;
using AddressBook.Core.Models;
using Newtonsoft.Json;


namespace AddressBook.Core.Services;

public class JsonFileService <T> : IFileService<T> where T : IRoot
{
    public async Task<bool> SaveToFileAsync(IEnumerable entities, string fileName)
    {
        var jsonString = JsonConvert.SerializeObject(entities);
        
        try
        {
            await File.WriteAllTextAsync(fileName, jsonString);
            return true;
        }
        catch (Exception e)
        {
            Console.WriteLine($"An error occurred writing file: {e.Message}");
            return false;
        }
    }

    public Task<List<T>> ReadFromFileAsync(string fileName)
    {
        try
        {
            var jsonString = File.ReadAllTextAsync(fileName).Result;
            return Task.FromResult(JsonConvert.DeserializeObject<List<T>>(jsonString) ?? new List<T>());
        }
        catch (Exception e)
        {
            Console.WriteLine($"An error occurred reading file: {e.Message}");
            return Task.FromResult(new List<T>());
        }
    }
}