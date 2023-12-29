using AddressBook.Console.Services;

namespace AddressBook.Console;

public class App
{
    private readonly IMenuService _menuService;

    public App(IMenuService menuService)
    {
        _menuService = menuService;
    }
   
    /// <summary>
    /// This is the entry point of the application.
    /// </summary>
    /// <returns></returns>
    public Task RunAsync()
    {
        System.Console.CancelKeyPress += (sender, args) =>
        {
            System.Console.WriteLine("Goodbye! Please dont forget to rate us 5 stars on Omniway!");
        };
        
        return _menuService.InitializeMenuAsync();
    }
}