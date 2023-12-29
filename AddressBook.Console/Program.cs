using AddressBook.Console;
using AddressBook.Console.Services;
using AddressBook.Core;
using AddressBook.Core.Models;
using AddressBook.Core.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

var builder = Host.CreateDefaultBuilder(args);



var config = new ConfigurationBuilder()
    .SetBasePath(Directory.GetCurrentDirectory())
    .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
    .Build();


builder.ConfigureServices((services) =>
{
    services.AddAddressBookCore();
    services.AddScoped<IInputOutputService, ConsoleInputOutputService>();
    services.AddScoped<IMenuService, MenuService>();
    services.AddSingleton<IConfiguration>(config);
    services.AddSingleton<IFileService<Contact>, JsonFileService<Contact>>();
});


var host = builder.Build();

var app = ActivatorUtilities.CreateInstance<App>(host.Services);
await app.RunAsync();
