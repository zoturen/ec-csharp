using AddressBook.GUI.Views;

namespace AddressBook.GUI;

public partial class AppShell : Shell
{
    public AppShell()
    {
        InitializeComponent();

        Routing.RegisterRoute(nameof(ContactListView), typeof(ContactListView));
        Routing.RegisterRoute(nameof(NewContactView), typeof(NewContactView));
        Routing.RegisterRoute(nameof(EditContactView), typeof(EditContactView));
    }
}