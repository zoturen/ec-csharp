using System.Collections;
using AddressBook.Core.Models;
using AddressBook.Core.Services;
using Newtonsoft.Json;

namespace AddressBook.Console.Services;

public class JsonFileService <T> : IFileService<T> where T : IRoot
{
    private static string PATH = $"{Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData)}/AddressBook";
    public async Task<bool> SaveToFileAsync(IEnumerable entities, string fileName)
    {
        var jsonString = JsonConvert.SerializeObject(entities);
        
        var path = Path.Combine(PATH, fileName);
    
        try
        {
            await File.WriteAllTextAsync(path, jsonString);
            return true;
        }
        catch (Exception e)
        {
            System.Console.WriteLine($"An error occurred writing file: {e.Message}");
            return false;
        }
    }

    public async Task<List<T>> ReadFromFileAsync(string fileName)
    {
        var path = Path.Combine(PATH, fileName);
    
        try
        {
            if (!File.Exists(path))
            {
                using StreamWriter sw = File.CreateText(path);
            }
        
            var jsonString = await File.ReadAllTextAsync(path);

            return JsonConvert.DeserializeObject<List<T>>(jsonString) ?? new List<T>();
        }
        catch (Exception e)
        {
            System.Console.WriteLine($"An error occurred reading file: {e.Message}");
            return new List<T>();
        }
    }
}