using System.Windows.Input;
using CommunityToolkit.Mvvm.Input;

namespace AddressBook.GUI.Controls;

public partial class CheckButton : ContentView
{
    public static readonly BindableProperty SizeProperty =
        BindableProperty.Create(nameof(Size), typeof(int), typeof(CheckButton));

    public static readonly BindableProperty OnSubmitProperty =
        BindableProperty.Create(nameof(OnSubmit), typeof(ICommand), typeof(CheckButton));

    public CheckButton()
    {
        InitializeComponent();
    }

    public int Size
    {
        get => (int) GetValue(SizeProperty);
        set => SetValue(SizeProperty, value);
    }

    public ICommand OnSubmit
    {
        get => (ICommand) GetValue(OnSubmitProperty);
        set => SetValue(OnSubmitProperty, value);
    }

    [RelayCommand]
    public async Task Submit()
    {
        if (OnSubmit.CanExecute(null)) OnSubmit.Execute(null);
        // await Shell.Current.GoToAsync("..");
    }
}