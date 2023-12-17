using AddressBook.Core.Models;
using AddressBook.Core.Services;
using Microsoft.Extensions.Configuration;

namespace AddressBook.Core.Repositories;

public class ContactRepository : IContactRepository
{
    private readonly IFileService<Contact> _fileService;
    private readonly string _fileName;
    
    // Lazy<T> is a class that allows us to defer the creation of a value until it is needed.
    // Very useful in our tests, since otherwise we would need to create a TestContactRepository
    // that sets the value of _contacts in its constructor or have it's own method of populating the _contacts.
    private Lazy<Task<List<Contact>>> _contacts;

    public ContactRepository(IFileService<Contact> fileService, IConfiguration configuration)
    {
        _fileService = fileService;
        _fileName = configuration["Files:Contacts"] ?? throw new ArgumentNullException(nameof(configuration));
        
        _contacts = new Lazy<Task<List<Contact>>>(async () =>
            await _fileService.ReadFromFileAsync(_fileName));
    }
    
    public async Task<RepositoryResponse<List<Contact>>> GetContactsAsync()
    {
        return new RepositoryResponse<List<Contact>>
        {
            Success = true,
            Entity = await _contacts.Value
        };
    }

    public async Task<RepositoryResponse<Contact?>> GetContactAsync(Guid id)
    {
        var contacts = await _contacts.Value;
        var contact = contacts.FirstOrDefault(c => c.Id == id);
        if (contact == null)
        {
            return new RepositoryResponse<Contact?>
            {
                Success = false,
                Message = "Contact not found"
            };
        }
        return new RepositoryResponse<Contact?>
        {
            Success = true,
            Entity = contact
        };
        
    }

    public async Task<RepositoryResponse<Contact>> CreateContactAsync(Contact contact)
    {
        var contacts = await _contacts.Value;
        if (contacts.Any(c => c.Email == contact.Email))
        {
            return new RepositoryResponse<Contact>
            {
                Success = false,
                Message = "Contact with this email already exists"
            };
        }
        
        _contacts = new Lazy<Task<List<Contact>>>(() => Task.FromResult(contacts.Append(contact).ToList()));
        if (await _fileService.SaveToFileAsync(contacts, _fileName))
        {
            return new RepositoryResponse<Contact>
            {
                Success = true,
                Entity = contact
            };
        }
        
        return new RepositoryResponse<Contact>
        {
            Success = false,
            Message = "Could not save contact"
        };
    }

    public async Task<RepositoryResponse<Contact>> UpdateContactAsync(Contact contact)
    {
        var contacts = await _contacts.Value;
      var index = contacts.ToList().FindIndex(c => c.Id == contact.Id);
      if (index >= 0)
      {
          contacts.ToList()[index] = contact;
          await _fileService.SaveToFileAsync(contacts, _fileName);
            return new RepositoryResponse<Contact>
            {
                Success = true,
                Entity = contact
            };
      }
      return new RepositoryResponse<Contact>
      {
          Success = false,
          Message = "Could not update contact"
      };
    }

    public async Task<RepositoryResponse> DeleteContactAsync(Guid id)
    {
        var contacts = await _contacts.Value;
        var index = contacts.FindIndex(c => c.Id == id);
        if (index >= 0)
        {
            contacts.RemoveAt(index);
            var isSuccess = await _fileService.SaveToFileAsync(contacts, _fileName);
            _contacts = new Lazy<Task<List<Contact>>>(() => Task.FromResult(contacts));
            return new RepositoryResponse
            {
                Success = isSuccess,
                Message = isSuccess ? null : "Could not delete contact"
            };
        }
        return new RepositoryResponse
        {
            Success = false,
            Message = "Could not delete contact"
        };
    }

    public async Task<RepositoryResponse> DeleteContactByEmailAddressAsync(string email)
    {
        var contacts = await _contacts.Value;
        var index = contacts.FindIndex(c => c.Email == email);
        if (index >= 0)
        {
            contacts.RemoveAt(index);
            var isSuccess = await _fileService.SaveToFileAsync(contacts, _fileName);
            _contacts = new Lazy<Task<List<Contact>>>(() => Task.FromResult(contacts));
            return new RepositoryResponse
            {
                Success = isSuccess,
                Message = isSuccess ? null : "Could not delete contact"
            };
        }
        return new RepositoryResponse
        {
            Success = false,
            Message = "Could not delete contact"
        };
    }

    public async Task<RepositoryResponse<Contact?>>  GetContactByEmail(string email)
    {
        var contacts = await _contacts.Value;
        var contact = contacts.FirstOrDefault(c => c.Email == email);
        if (contact == null)
        {
            return new RepositoryResponse<Contact?>
            {
                Success = false,
                Message = "Contact not found"
            };
        }
        return new RepositoryResponse<Contact?>
        {
            Success = true,
            Entity = contact
        };
    }
    
    
}