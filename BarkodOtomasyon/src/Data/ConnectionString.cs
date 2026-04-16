using System;
using System.IO;

namespace BarkodOtomasyon.Data;

public static class ConnectionString
{
    public static string GetConnectionString()
    {
        // SQLite için connection string - Masaüstüne DB oluştur
        string appDataPath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
        string appFolder = Path.Combine(appDataPath, "BarkodOtomasyon");
        
        // Klasör yoksa oluştur
        if (!Directory.Exists(appFolder))
            Directory.CreateDirectory(appFolder);
        
        string dbPath = Path.Combine(appFolder, "BarkodOtomasyon.db");
        return $"Data Source={dbPath}";
    }
}