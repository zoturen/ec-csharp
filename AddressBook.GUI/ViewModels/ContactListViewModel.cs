using AddressBook.GUI.Services;
using AddressBook.GUI.Views;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Contact = AddressBook.Core.Models.Contact;

namespace AddressBook.GUI.ViewModels;

public partial class ContactListViewModel : ObservableObject
{
    [ObservableProperty] private Contact? _selectedContact;

    public ContactListViewModel(ContactService contactService)
    {
        ContactService = contactService;
    }

    public ContactService ContactService { get; }

    [RelayCommand]
    public async Task NavigateToNewContact()
    {
        await Shell.Current.GoToAsync(nameof(NewContactView));
    }

    [RelayCommand]
    public async Task NavigateToEditContact(Guid id)
    {
        await Shell.Current.GoToAsync($"{nameof(EditContactView)}?{nameof(EditContactViewModel.Id)}={id}");
        SelectedContact = null;
    }
}