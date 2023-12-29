using AddressBook.GUI.ViewModels;

namespace AddressBook.GUI.Views;

public partial class EditContactView : ContentPage
{
    public EditContactView(EditContactViewModel vm)
    {
        InitializeComponent();
        BindingContext = vm;
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();
        ((EditContactViewModel) BindingContext!).OnAppearing();
    }
}