using AddressBook.GUI.ViewModels;

namespace AddressBook.GUI.Views;

public partial class ContactListView : ContentPage
{
    public ContactListView(ContactListViewModel vm)
    {
        InitializeComponent();
        BindingContext = vm;
    }
}