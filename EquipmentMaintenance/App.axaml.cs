using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Markup.Xaml;
using EquipmentMaintenance.Models;
using EquipmentMaintenance.Services;
using EquipmentMaintenance.ViewModels;
using EquipmentMaintenance.Views;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.IO;

namespace EquipmentMaintenance;

public partial class App : Application
{
    public static IServiceProvider? Services { get; private set; }

    public override void Initialize()
    {
        AvaloniaXamlLoader.Load(this);
    }

    public override void OnFrameworkInitializationCompleted()
    {
        string dbFilePath = SetDatabaseFilePath();

        var serviceCollection = new ServiceCollection();
        BuildViewModels(serviceCollection);
        BuildServices(serviceCollection);
        serviceCollection.AddDbContextFactory<AppDbContext>(options => options.UseSqlite($"Data Source={dbFilePath}"));
        Services = serviceCollection.BuildServiceProvider();

        CreateDatabase();

        if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
        {
            desktop.MainWindow = new MainWindow
            {
                DataContext = Services.GetRequiredService<MainViewModel>(),
            };
        }

        base.OnFrameworkInitializationCompleted();
    }

    private static string SetDatabaseFilePath()
    {
        var applicationFolder = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "EquipmentMaintenance");
        Directory.CreateDirectory(applicationFolder);
        var dbPath = Path.Combine(applicationFolder, "app.db");

        return dbPath;
    } 

    private static void BuildServices(IServiceCollection services)
    {
        services.AddTransient<INavigationService, NavigationService>();
        services.AddTransient<ICrudService, CrudService>();
    }

    private static void BuildViewModels(IServiceCollection services)
    {
        services.AddSingleton<MainViewModel>();
        services.AddSingleton<EmployeeListViewModel>();
        services.AddTransient<EmployeeObjectViewModel>();
    }

    private static void CreateDatabase()
    {
        using var scope = Services?.CreateScope();

        if (scope is not null)
        {
            var factory = scope.ServiceProvider.GetRequiredService<IDbContextFactory<AppDbContext>>();
            using var context = factory.CreateDbContext();
            context.Database.EnsureCreated();
        }
    }
}