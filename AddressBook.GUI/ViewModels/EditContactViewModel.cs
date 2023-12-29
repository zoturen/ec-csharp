using AddressBook.Core.Models;
using AddressBook.GUI.Services;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Contact = AddressBook.Core.Models.Contact;

namespace AddressBook.GUI.ViewModels;

[QueryProperty(nameof(Id), nameof(Id))]
public partial class EditContactViewModel : ObservableObject
{
    private readonly ContactService _contactService;

    [ObservableProperty] private Contact _contact = new()
    {
        Address = new Address()
    };

    public EditContactViewModel(ContactService contactService)
    {
        _contactService = contactService;
    }

    public string Id { get; set; } = "";

    public Func<string?> ValidateFirstname => Contact.ValidateFirstname;
    public Func<string?> ValidateLastName => Contact.ValidateLastname;
    public Func<string?> ValidateEmail => Contact.ValidateEmail;
    public Func<string?> ValidatePhoneNumber => Contact.ValidatePhoneNumber;
    public Func<string?> ValidateStreet => Contact.Address.ValidateStreet;
    public Func<string?> ValidateCity => Contact.Address.ValidateCity;
    public Func<string?> ValidateZipCode => Contact.Address.ValidateZipCode;
    public Func<string?> ValidateCountry => Contact.Address.ValidateCountry;


    [RelayCommand]
    public async void UpdateContact()
    {
        if (Contact.Validate())
        {
            _contactService.UpdateContact(Contact);
            await Shell.Current.GoToAsync("..");
        }
    }

    [RelayCommand]
    public async void DeleteContact()
    {
        _contactService.DeleteContact(Contact);
        await Shell.Current.GoToAsync("..");
    }

    public void OnAppearing()
    {
        Contact = _contactService.GetContact(Id);
    }
}