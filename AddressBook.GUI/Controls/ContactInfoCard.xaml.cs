namespace AddressBook.GUI.Controls;

public partial class ContactInfoCard : ContentView
{
    public static readonly BindableProperty LabelTextProperty =
        BindableProperty.Create(nameof(LabelText), typeof(string), typeof(ContactInfoCard));

    public static readonly BindableProperty PlaceholderTextProperty =
        BindableProperty.Create(nameof(PlaceholderText), typeof(string), typeof(ContactInfoCard));

    public static readonly BindableProperty InputTextProperty =
        BindableProperty.Create(nameof(InputText), typeof(string), typeof(ContactInfoCard));

    public static readonly BindableProperty ErrorTextProperty =
        BindableProperty.Create(nameof(ErrorText), typeof(string), typeof(ContactInfoCard));

    public static readonly BindableProperty HasErrorProperty =
        BindableProperty.Create(nameof(HasError), typeof(bool), typeof(ContactInfoCard));

    /* public static readonly BindableProperty ValidateProperty =
         BindableProperty.Create(nameof(Validate), typeof(ICommand), typeof(ContactInfoCard)); */

    public static readonly BindableProperty ValidateProperty =
        BindableProperty.Create(nameof(Validate), typeof(Func<string>), typeof(CheckButton));


    public ContactInfoCard()
    {
        InitializeComponent();
    }

    public string LabelText
    {
        get => (string) GetValue(LabelTextProperty);
        set => SetValue(LabelTextProperty, value);
    }

    public string PlaceholderText
    {
        get => (string) GetValue(PlaceholderTextProperty);
        set => SetValue(PlaceholderTextProperty, value);
    }

    public string InputText
    {
        get => (string) GetValue(InputTextProperty);
        set => SetValue(InputTextProperty, value);
    }

    public string ErrorText
    {
        get => (string) GetValue(ErrorTextProperty);
        set => SetValue(ErrorTextProperty, value);
    }

    public bool HasError
    {
        get => (bool) GetValue(HasErrorProperty);
        set => SetValue(HasErrorProperty, value);
    }

    public Func<string?> Validate
    {
        get => (Func<string>) GetValue(ValidateProperty);
        set => SetValue(ValidateProperty, value);
    }

    private void InputView_OnTextChanged(object? sender, TextChangedEventArgs e)
    {
        if (Validate != null)
        {
            HasError = false;
            ErrorText = string.Empty;
            var error = Validate.Invoke();
            if (!string.IsNullOrWhiteSpace(error))
            {
                HasError = true;
                ErrorText = error;
            }
        }
    }
}