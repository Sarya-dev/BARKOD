@echo off
REM Stok Takip Sistemi - Baslatici
REM Bu dosya herhangi bir PC'de calisir

setlocal enabledelayedexpansion

REM Uygulama yolu (USERPROFILE her PC'de farkli)
set "AppPath=%USERPROFILE%\Desktop\BARKOD\Stok Takip Sistemi\Stok Takip Sistemi\bin\Debug\net8.0-windows"

if not exist "%AppPath%\StokTakipSistemi.exe" (
    echo.
    echo [HATA] Uygulama bulunamadi!
    echo.
    echo Beklenen yol:
    echo %AppPath%\StokTakipSistemi.exe
    echo.
    echo Lutfen BARKOD klasorununun masaustunde oldugunu kontrol edin.
    echo.
    pause
    exit /b 1
)

REM Uygulamayi basla
cd /d "%AppPath%"
start "" StokTakipSistemi.exe

exit /b 0
