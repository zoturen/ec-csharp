using System.Text;
using AddressBook.Core.Models;
using StringWriter = System.IO.StringWriter;

namespace AddressBook.Console.Requests;

public static class ContactResponses
{
    public static string ListContact(Contact contact)
    {
       var sb = new StringBuilder();
       sb.AppendLine($"Name: {contact.FirstName} {contact.LastName}");
       sb.AppendLine($"Email: {contact.Email}");

       return sb.ToString();
    }
    
    public static string ListContactDetailed(Contact contact)
    {
        var sb = new StringBuilder();
        sb.AppendLine($"Name: {contact.FirstName} {contact.LastName}");
        sb.AppendLine($"Email: {contact.Email}");
        sb.AppendLine($"Phone Number: {contact.PhoneNumber}");
        sb.AppendLine("Address:");
        sb.AppendLine(Indent($"Street: {contact.Address.Street}", 4));
        sb.AppendLine(Indent($"City: {contact.Address.City}", 4));
        sb.AppendLine(Indent($"Zip Code: {contact.Address.ZipCode}", 4));
        sb.AppendLine(Indent($"Country: {contact.Address.Country}", 4));
        return sb.ToString();
    }
    
    private static string Indent(string input, int indent)
    {
        var lines = input.Split(Environment.NewLine);
        var sb = new StringBuilder();
        foreach (var line in lines)
        {
            sb.Append($"{new string(' ', indent)}{line}");
        }
        return sb.ToString();
    }
}