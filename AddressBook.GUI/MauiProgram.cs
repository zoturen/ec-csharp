using System.Reflection;
using AddressBook.Core;
using AddressBook.Core.Services;
using AddressBook.GUI.Extensions;
using AddressBook.GUI.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Contact = AddressBook.Core.Models.Contact;

namespace AddressBook.GUI;

public static class MauiProgram
{
    public static MauiApp CreateMauiApp()
    {
        var builder = MauiApp.CreateBuilder();

        /*var config = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
            .Build();*/

        builder
            .UseMauiApp<App>()
            .ConfigureFonts(fonts =>
            {
                fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                fonts.AddFont("faregular.ttf", "FAR");
                fonts.AddFont("fasolid.ttf", "FAS");
                fonts.AddFont("fabrands.ttf", "FAB");
            });

        var a = Assembly.GetExecutingAssembly();
        using var stream = a.GetManifestResourceStream("AddressBook.GUI.appsettings.json");

        var config = new ConfigurationBuilder()
            .AddJsonStream(stream)
            .Build();


        builder.Configuration.AddConfiguration(config);

        builder.Services.AddSingleton<IFileService<Contact>, JsonFileService<Contact>>();
        builder.Services.AddSingleton<ContactService>();
        builder.Services.AddAddressBookCore();
        builder.Services.AddAddressBookViewModels();
        builder.Services.AddAddressBookViews();

#if DEBUG
        builder.Logging.AddDebug();
#endif

        return builder.Build();
    }
}