using AddressBook.Core.Models;
using AddressBook.GUI.Services;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Contact = AddressBook.Core.Models.Contact;

namespace AddressBook.GUI.ViewModels;

public partial class NewContactViewModel : ObservableObject
{
    private readonly ContactService _contactService;


    [ObservableProperty] private Contact _contact = new()
    {
        Address = new Address()
    };


    public NewContactViewModel(ContactService contactService)
    {
        _contactService = contactService;
    }

    public Func<string?> ValidateFirstname => Contact.ValidateFirstname;
    public Func<string?> ValidateLastName => Contact.ValidateLastname;
    public Func<string?> ValidateEmail => Contact.ValidateEmail;
    public Func<string?> ValidatePhoneNumber => Contact.ValidatePhoneNumber;
    public Func<string?> ValidateStreet => Contact.Address.ValidateStreet;
    public Func<string?> ValidateCity => Contact.Address.ValidateCity;
    public Func<string?> ValidateZipCode => Contact.Address.ValidateZipCode;
    public Func<string?> ValidateCountry => Contact.Address.ValidateCountry;


    [RelayCommand]
    public async Task CreateContact()
    {
        if (Contact.Validate())
        {
            _contactService.CreateContact(Contact);
            await Shell.Current.GoToAsync("..");
        }


        // FIXME: should display error if not working
    }
}