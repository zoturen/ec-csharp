namespace AddressBook.Console.Services;

/// <summary>
/// This interface is used to abstract the input and output of the console.
/// Just to be able to mock it in the tests.
/// </summary>
public interface IInputOutputService
{
    void WriteLine(string message);
    void WriteLine(string message, object? args);
    string ReadLine();
    
}