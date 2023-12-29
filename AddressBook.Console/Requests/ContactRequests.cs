using AddressBook.Console.Services;
using AddressBook.Core.Models;

namespace AddressBook.Console.Requests;

public static class ContactRequests
{
    public static Contact CreateContact(IInputOutputService ioService)
    {
        var contact = new Contact();
        var address = new Address();
        contact.Address = address;
        
        GetAndValidateInput(
            "Please enter the contact's first name:", 
            input => contact.FirstName = input,
            contact.ValidateFirstname, ioService);

        GetAndValidateInput(
            "Please enter the contact's last name:", 
            input => contact.LastName = input, 
            contact.ValidateLastname, ioService);
        
        GetAndValidateInput(
            "Please enter the contact's email address:",
            input => contact.Email = input,
            contact.ValidateEmail, ioService);
        
        GetAndValidateInput(
            "Please enter the contact's phone number:",
            input => contact.PhoneNumber = input,
            contact.ValidatePhoneNumber, ioService);
        
          GetAndValidateInput(
            "Please enter the contact's street:",
            input => address.Street = input,
            address.ValidateStreet, ioService);
          
          GetAndValidateInput(
            "Please enter the contact's city:",
            input => address.City = input,
            address.ValidateCity, ioService);
              
          GetAndValidateInput(
              "Please enter the contact's zip code:",
              input => address.ZipCode = input,
              address.ValidateZipCode, ioService);
          
          GetAndValidateInput(
              "Please enter the contact's country:",
              input => address.Country = input,
              address.ValidateCountry, ioService);
          
        return contact;
    }
    public static void GetAndValidateInput(string message, Action<string> setter, Func<string?> validator, IInputOutputService ioService)
    {
        while (true)
        {
            ioService.WriteLine(message);
            setter(ioService.ReadLine());
            var error = validator();
            if (error != null)
            {
                ioService.WriteLine(error);
                continue;
            }
            break;
        }
    }
}