using AddressBook.Console.Requests;
using AddressBook.Core.Repositories;

namespace AddressBook.Console.Services;

public class MenuService : IMenuService
{
    private readonly IContactRepository _contactRepository;
    private readonly IInputOutputService _ioService;

    public MenuService(IContactRepository contactRepository, IInputOutputService ioService)
    {
        _contactRepository = contactRepository;
        _ioService = ioService;
    }
    public void ShowMenu()
    {
        _ioService.WriteLine("1. Add a new contact");
        _ioService.WriteLine("2. List all contacts");
        _ioService.WriteLine("3. Search contacts");
        _ioService.WriteLine("4. Delete a contact");
        _ioService.WriteLine("5. Exit");
        _ioService.WriteLine("-m for this menu");
    }

    public void WelcomeMessage()
    {
        _ioService.WriteLine("This is your missing piece!");
        _ioService.WriteLine("Welcome to the Address Book!");
    }

    public async Task InitializeMenuAsync()
    {
        System.Console.Clear();
        WelcomeMessage();
        while (true)
        {
            _ioService.WriteLine("Please select an option:");
            ShowMenu();
            var input = _ioService.ReadLine();
            switch (input)
            {
                case "1":
                    await CreateContactAsync();
                    break;
                case "2":
                    await ListContactsAsync();
                    break;
                case "3":
                    await SearchContactsAsync();
                    break;
                case "4":
                    await DeleteContactAsync();
                    break;
                case "5":
                    _ioService.WriteLine("Goodbye! Please dont forget to rate us 5 stars on Omniway!");
                    return;
                
                case "-m":
                    ShowMenu();
                    break;
                default:
                    _ioService.WriteLine("Invalid selection");
                    _ioService.WriteLine("-m for menu");
                    break;
            }
        }
    }

    public async Task CreateContactAsync()
    {
        var contact = ContactRequests.CreateContact(_ioService);
        await _contactRepository.CreateContactAsync(contact);
        _ioService.WriteLine("Contact added successfully!");
        _ioService.WriteLine("Press any key to continue...");
        System.Console.ReadKey();
    }

    public async Task ListContactsAsync()
    {
        var res = await _contactRepository.GetContactsAsync();
        var enumerable = res.Entity!.ToList();
        foreach (var contact in enumerable)
        {
            _ioService.WriteLine(ContactResponses.ListContact(contact));
        }

        _ioService.WriteLine("There are currently {0} contacts in your address book", enumerable.Count);
        _ioService.WriteLine("Press any key to continue...");
        System.Console.ReadKey();
    }

    public async Task SearchContactsAsync()
    {
        _ioService.WriteLine("Please enter the email address of the contact you want to search for:");
        var email = System.Console.ReadLine();
        if (email is null)
        {
            _ioService.WriteLine("Invalid email");
            _ioService.WriteLine("Try again? (y/n)");
            var input = _ioService.ReadLine();
            if (input == "y")
            {
                await SearchContactsAsync();
            }
            return;
        }
        var res = await _contactRepository.GetContactByEmail(email);
        if (!res.Success || res.Entity is null)
        {
            _ioService.WriteLine("Contact not found");
            _ioService.WriteLine("Try again? (y/n)");
            var input = _ioService.ReadLine();
            if (input == "y")
            {
                await SearchContactsAsync();
            }
            return;
        }
        _ioService.WriteLine(ContactResponses.ListContactDetailed(res.Entity));
        _ioService.WriteLine("Press any key to continue...");
        System.Console.ReadKey();
    }

    public async Task DeleteContactAsync()
    {
        _ioService.WriteLine("Please enter the email address of the contact you want to delete:");
        var email = System.Console.ReadLine();
        if (email is null)
        {
            _ioService.WriteLine("Invalid email");
            _ioService.WriteLine("Try again? (y/n)");
            var input = _ioService.ReadLine();
            if (input == "y")
            {
                await DeleteContactAsync();
            }
            return;
        }
        
        var contactExistsRes = await _contactRepository.GetContactByEmail(email);
        if (contactExistsRes.Success is false || contactExistsRes.Entity is null)
        {
            _ioService.WriteLine("Contact not found");
            _ioService.WriteLine("Try again? (y/n)");
            var input = _ioService.ReadLine();
            if (input == "y")
            {
                await DeleteContactAsync();
            }
            return;
        }

        _ioService.WriteLine("Are you sure you want to delete this contact? (y/n)");
        var confirmation = _ioService.ReadLine();
        if (confirmation == "y")
        {
            var res = await _contactRepository.DeleteContactByEmailAddressAsync(email);
            if (!res.Success)
            {
                _ioService.WriteLine("Something went wrong deleting the contact");
                _ioService.WriteLine("Press any key to continue...");
                System.Console.ReadKey();
                return;
            }
            _ioService.WriteLine("Contact deleted successfully!");
            _ioService.WriteLine("Press any key to continue...");
            System.Console.ReadKey();
        }
    }
}