using System.Collections;
using AddressBook.Core.Models;

namespace AddressBook.Core.Services;

public interface IFileService <T> where T : IRoot
{
    public Task<bool> SaveToFileAsync(IEnumerable entities, string fileName);
    
    public Task<List<T>> ReadFromFileAsync(string fileName);
}