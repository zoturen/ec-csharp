namespace AddressBook.Console.Services;

public interface IMenuService
{
    void ShowMenu();
    Task InitializeMenuAsync();
    Task CreateContactAsync();
    Task ListContactsAsync();
    Task SearchContactsAsync();
    Task DeleteContactAsync();
}