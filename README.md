# Barkod Okuma Sistemi

## 📋 Açıklama
Barkod taraması, ürün yönetimi ve stok takibi yapabilen Windows Forms masaüstü uygulaması.

## ✨ Özellikler

- 📱 **Barkod Okut/Ara** - Barkod yazarak ürün arama
- ➕ **Ürün Yönetimi** - Yeni ürün ekleme, düzenleme, silme
- 💰 **Fiyat & Stok Takibi** - Ürün fiyatı ve stok miktarı
- 💸 **Satış İşlemi** - Stok çıkışı ve satış yapma
- 📊 **Raporlama** - Stok raporu, depo değeri, ürün listesi

## 🛠️ Teknoloji

- **Framework:** .NET 8.0 Windows Forms
- **Veritabanı:** SQLite
- **ORM:** Entity Framework Core
- **IDE:** Visual Studio Code

## 🚀 Kurulum

### Gereksinimler
- .NET 8.0 SDK
- Windows x64

### Çalıştırma

1. Projeyi klonla:
```bash
git clone https://github.com/[KULLANICI]/barkod-okuma-sistemi.git
cd BarkodOtomasyon
```

2. Projeyi çalıştır:
```bash
dotnet run
```

### Standalone EXE

Bağımsız çalışan exe oluştur:
```bash
dotnet publish -c Release -r win-x64 --self-contained -p:PublishSingleFile=true
```

EXE konumu: `bin/Release/net8.0-windows/win-x64/publish/BarkodOtomasyon.exe`

## 📂 Proje Yapısı

```
BarkodOtomasyon/
├── src/
│   ├── Forms/           # UI Forms
│   │   ├── Form1.cs
│   │   ├── ProductAddForm.cs
│   │   ├── ProductEditForm.cs
│   │   ├── SalesForm.cs
│   │   └── ReportForm.cs
│   ├── Models/          # Veri modelleri
│   │   ├── Product.cs
│   │   └── Barcode.cs
│   ├── Services/        # Business logic
│   │   ├── ProductService.cs
│   │   └── BarcodeService.cs
│   ├── Data/            # Veritabanı
│   │   ├── DatabaseContext.cs
│   │   ├── ConnectionString.cs
│   │   └── DesignTimeDbContextFactory.cs
│   └── Program.cs
├── Migrations/          # EF Core migrations
├── BarkodOtomasyon.csproj
└── README.md
```

## 📖 Kullanım

### Ürün Ekleme
1. Ana ekranda barkod gir
2. "Yeni Ürün Ekle" tıkla
3. Ürün bilgilerini gir:
   - Ürün Adı
   - Açıklama
   - Fiyat
   - Stok
4. Kaydet

### Satış Yapma
1. Ürünü listeden seç
2. "Satış" tıkla
3. Satış miktarını gir
4. Stok otomatik azalır

### Rapor Görüntüleme
1. "Rapor" butonuna tıkla
2. Stok raporu, ürün listesi veya özet gör

## 🗄️ Veritabanı

Veritabanı konumu:
```
C:\Users\[Kullanıcı-Adı]\AppData\Roaming\BarkodOtomasyon\BarkodOtomasyon.db
```

### Tablolar

**Barcodes**
- Id (PK)
- Code (String, Unique)
- CreatedDate (DateTime)

**Products**
- Id (PK)
- Name (String)
- Description (String)
- Price (Decimal)
- Stock (Integer)
- BarcodeId (FK)
- CreatedDate (DateTime)

## 🐛 Bilinen Sorunlar

Hiç yok! 🎉

## 📝 Lisans

MIT License

## 👨‍💻 Geliştirici

Barkod Okuma Sistemi - 2026
