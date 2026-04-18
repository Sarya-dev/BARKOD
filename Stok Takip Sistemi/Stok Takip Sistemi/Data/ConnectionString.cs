using System;
using System.IO;

namespace StokTakipSistemi.Data;

public static class ConnectionString
{
    public static string GetConnectionString()
    {
        // SQLite için connection string - AppData'ya DB oluştur
        string appDataPath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
        string appFolder = Path.Combine(appDataPath, "StokTakipSistemi");
        
        // Klasör yoksa oluştur
        if (!Directory.Exists(appFolder))
            Directory.CreateDirectory(appFolder);
        
        string dbPath = Path.Combine(appFolder, "StokTakipSistemi.db");
        return $"Data Source={dbPath}";
    }
}
