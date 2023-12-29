using System.Collections.ObjectModel;
using AddressBook.Core.Repositories;
using CommunityToolkit.Mvvm.ComponentModel;
using Contact = AddressBook.Core.Models.Contact;

namespace AddressBook.GUI.Services;

public partial class ContactService : ObservableObject
{
    private readonly IContactRepository _contactRepository;

    [ObservableProperty] private ObservableCollection<Contact> _contactList = [];
    [ObservableProperty] private bool _emptyList;
    [ObservableProperty] private bool _notEmptyList;

    public ContactService(IContactRepository contactRepository)
    {
        _contactRepository = contactRepository;
        UpdateContactCollection();
    }

    private void UpdateContactCollection()
    {
        var task = _contactRepository.GetContactsAsync();
        var res = task.Result;
        if (res.Success)
        {
            ContactList.Clear();
            foreach (var item in res.Entity!) ContactList.Add(item);
            EmptyList = ContactList.Count == 0;
            NotEmptyList = ContactList.Count > 0;
        }
    }

    public void DeleteContact(Contact contact)
    {
        var task = _contactRepository.DeleteContactAsync(contact.Id);
        var res = task.Result;
        if (res.Success) UpdateContactCollection();
    }

    public bool UpdateContact(Contact contact)
    {
        var task = _contactRepository.UpdateContactAsync(contact);
        var res = task.Result;
        if (res.Success) UpdateContactCollection();
        return res.Success;
    }

    public bool CreateContact(Contact contact)
    {
        var task = _contactRepository.CreateContactAsync(contact);
        var res = task.Result;
        if (res.Success) UpdateContactCollection();
        return res.Success;
    }

    public Contact GetContact(string id)
    {
        var guid = Guid.Parse(id);
        var task = _contactRepository.GetContactAsync(guid);
        var res = task.Result;
        if (res.Success) return res.Entity!;
        return new Contact();
    }
}