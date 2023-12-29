using AddressBook.GUI.ViewModels;

namespace AddressBook.GUI.Views;

public partial class NewContactView : ContentPage
{
    public NewContactView(NewContactViewModel vm)
    {
        InitializeComponent();
        BindingContext = vm;
    }
}