namespace StokTakipSistemi;

using System;
using System.Linq;
using System.Windows.Forms;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using StokTakipSistemi.Data;
using StokTakipSistemi.Models;
using StokTakipSistemi.Services;

internal static class Program
{
    /// <summary>
    ///  The main entry point for the application.
    /// </summary>
    [STAThread]
    static void Main()
    {
        try
        {
            // Dependency Injection setup
            var services = new ServiceCollection();
            
            // Database Context
            services.AddDbContext<DatabaseContext>(options =>
                options.UseSqlite(ConnectionString.GetConnectionString()));
            
            // Services
            services.AddScoped<BarcodeService>();
            services.AddScoped<ProductService>();
            services.AddScoped<StockService>();
            services.AddScoped<DepotService>();
            services.AddScoped<Form1>();

            var serviceProvider = services.BuildServiceProvider();

            // Seed test data
            SeedTestData(serviceProvider);

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            ApplicationConfiguration.Initialize();
            Application.Run(serviceProvider.GetRequiredService<Form1>());
        }
        catch (Exception ex)
        {
            MessageBox.Show($"HATA: {ex.Message}\n\n{ex.StackTrace}", "Kritik Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            Environment.Exit(1);
        }
    }

    static void SeedTestData(ServiceProvider serviceProvider)
    {
        using (var scope = serviceProvider.CreateScope())
        {
            var context = scope.ServiceProvider.GetRequiredService<DatabaseContext>();
            
            // Ensure database is created
            context.Database.Migrate();

            // Eğer zaten veri varsa kur
            if (context.Barcodes.Any() || context.Products.Any())
                return;

            try
            {
                // Create test barcode
                var barcode = new Barcode
                {
                    Code = "1234567890",
                    CreatedDate = DateTime.Now
                };
                context.Barcodes.Add(barcode);
                context.SaveChanges();

                // Create test product
                var product = new Product
                {
                    Name = "Lastik 205/55R16",
                    Description = "Test ürün",
                    Price = 500,
                    Stock = 10,
                    BarcodeId = barcode.Id,
                    Brand = "TestMarka",
                    Size = "205/55R16",
                    Season = "4 Mevsim",
                    Series = "Test",
                    CreatedDate = DateTime.Now
                };
                context.Products.Add(product);
                context.SaveChanges();

                // Create test depot
                var depot = new Depot
                {
                    Name = "Depo 1",
                    Description = "Test deposu",
                    CreatedDate = DateTime.Now
                };
                context.Depots.Add(depot);
                context.SaveChanges();

                // Add stock movement
                var movement = new StockMovement
                {
                    ProductId = product.Id,
                    DepotId = depot.Id,
                    Quantity = 10,
                    MovementType = "IN",
                    Notes = "Başlangıç stoku",
                    MovementDate = DateTime.Now,
                    CreatedDate = DateTime.Now
                };
                context.StockMovements.Add(movement);
                context.SaveChanges();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Seed hatası: {ex.Message}");
            }
        }
    }
}