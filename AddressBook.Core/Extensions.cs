using AddressBook.Core.Models;
using AddressBook.Core.Repositories;
using AddressBook.Core.Services;
using Microsoft.Extensions.DependencyInjection;

namespace AddressBook.Core;

public static class Extensions
{
    public static IServiceCollection AddAddressBookCore(this IServiceCollection services)
    {
        services.AddSingleton<IContactRepository, ContactRepository>();
        
        return services;
    }
}