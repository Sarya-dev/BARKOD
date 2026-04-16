namespace BarkodOtomasyon;

using System;
using System.Windows.Forms;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using BarkodOtomasyon.Data;
using BarkodOtomasyon.Services;

static class Program
{
    /// <summary>
    ///  The main entry point for the application.
    /// </summary>
    [STAThread]
    static void Main()
    {
        // Dependency Injection setup
        var services = new ServiceCollection();
        
        // Database Context
        services.AddDbContext<DatabaseContext>(options =>
            options.UseSqlite(ConnectionString.GetConnectionString()));
        
        // Services
        services.AddScoped<BarcodeService>();
        services.AddScoped<ProductService>();
        services.AddScoped<Form1>();

        var serviceProvider = services.BuildServiceProvider();

        Application.EnableVisualStyles();
        Application.SetCompatibleTextRenderingDefault(false);
        ApplicationConfiguration.Initialize();
        Application.Run(serviceProvider.GetRequiredService<Form1>());
    }    
}