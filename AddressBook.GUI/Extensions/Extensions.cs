using AddressBook.GUI.ViewModels;
using AddressBook.GUI.Views;

namespace AddressBook.GUI.Extensions;

public static class Extensions
{
    public static IServiceCollection AddAddressBookViews(this IServiceCollection services)
    {
        services.AddSingleton<ContactListView>();
        services.AddSingleton<NewContactView>();
        services.AddSingleton<EditContactView>();
        return services;
    }

    public static IServiceCollection AddAddressBookViewModels(this IServiceCollection services)
    {
        services.AddSingleton<ContactListViewModel>();
        services.AddSingleton<NewContactViewModel>();
        services.AddSingleton<EditContactViewModel>();
        return services;
    }
}