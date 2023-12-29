namespace AddressBook.Console.Services;

public class ConsoleInputOutputService : IInputOutputService
{
    public void WriteLine(string message)
    {
        System.Console.WriteLine(message);
    }
    
    public void WriteLine(string message, object? args)
    {
        System.Console.WriteLine(message, args);
    }

    public string ReadLine()
    {
        return System.Console.ReadLine();
    }
}