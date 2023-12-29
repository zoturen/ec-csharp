using CommunityToolkit.Mvvm.Input;

namespace AddressBook.GUI.Controls;

public partial class BackButton : ContentView
{
    public static readonly BindableProperty SizeProperty =
        BindableProperty.Create(nameof(Size), typeof(int), typeof(ContactInfoCard));


    public BackButton()
    {
        InitializeComponent();
    }

    public int Size
    {
        get => (int) GetValue(SizeProperty);
        set => SetValue(SizeProperty, value);
    }

    [RelayCommand]
    public async Task NavigateBack()
    {
        await Shell.Current.GoToAsync("..");
    }
}