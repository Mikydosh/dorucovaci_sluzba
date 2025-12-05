# 📦 Kurýr.cz - Doručovací služba

Webová aplikace pro správu a sledování zásilek vytvořená v **ASP.NET Core MVC** s **Entity Framework Core** a **MySQL** databází. Design stránky pomocí **Bootstrap**, vlastního **CSS** a obrázky + logo v **Adobe Illustrator**.

---
## 📷 Screenshoty
**Hlavní stránka**
<img width="800" alt="Hlavní stránka" src="https://github.com/user-attachments/assets/fb2a73f0-4931-4268-9561-aca29bf1b50b" />

---

**Správa zásilek**
<img width="800" alt="Správa zásilek" src="https://github.com/user-attachments/assets/201c250b-d409-4a25-a8bf-cb04f5339aca" />

---

**Detail zásilky**
<img width="800" alt="Detail zásilky" src="https://github.com/user-attachments/assets/e81de936-3d1e-4ea4-a912-fc0396f1d0b2" />

---

**Panel kurýra**
<img width="800" alt="Panel kurýra" src="https://github.com/user-attachments/assets/fe850b7d-0cad-41c5-a035-3e47c3a0ccbb" />

---

**Panel uživatele**
<img width="800" alt="Panel uživatele" src="https://github.com/user-attachments/assets/dd75d4bb-5aab-4d10-af22-f0f07ca3cf8e" />

---


**Formuláře**
<img width="600" alt="Vytvoření zásilky" src="https://github.com/user-attachments/assets/19b6764e-f5ed-4589-93ec-42a4abb762b3" />

---

<img width="450" height="562" alt="Sledování zásilky" src="https://github.com/user-attachments/assets/1bb704cc-e633-4c7d-963f-6a1649742cfe" />

---

<img width="400" height="510" alt="Login" src="https://github.com/user-attachments/assets/32ebc2d4-7a0c-49cf-a063-7159200831cd" />

a další...

---

## 🎯 O projektu

**Kurýr.cz** je systém pro správu doručovací služby, který umožňuje:
- Vytváření a sledování zásilek
- Správu uživatelů s různými rolemi (Admin, Podpora, Kurýr, Uživatel)
- Tracking zásilek
- Historii změn stavů zásilek
- Přiřazování kurýrů k zásilkám
---
## ✨Funkce

### 🏠 Veřejné funkce (anonymní uživatelé)
- **Home page** s přehledným rozhraním
- **Sledování zásilky** pomocí čísla a emailu
- **Vytvoření zásilky** bez registrace

### 👤 Pro přihlášené uživatele
- **Dashboard** s přehledem vlastních zásilek
- **Detail zásilky** s historií stavů a grafickým znázorněním
- **Profil uživatele** s možností úpravy údajů

### 🔧 Pro Admina a Podporu
- **Správa zásilek** (úprava stavu, přiřazení kurýra)
- **Správa uživatelů** (vytvoření, úprava, mazání, přiřazení rolí)
- **Přehled všech zásilek** s filtrováním a vyhledáváním

### 🚚 Pro Kurýra
- **Přehled přiřazených zásilek**
- **Změna stavu zásilky** (Vytvořeno → Přepravováno → Doručeno)
- **Historie doručení** pro každou zásilku

---

## 📦 Požadavky

Před instalací se ujisti, že máš nainstalováno:

- **.NET SDK 9.0 nebo novější** - [Stáhnout zde](https://dotnet.microsoft.com/download)
- **MySQL Server 9.0 nebo novější** - [Stáhnout zde](https://dev.mysql.com/downloads/mysql/)
  - ⚠️ **DŮLEŽITÉ:** MySQL musí běžet na **portu 3300** (ne výchozí 3306)
- **Git** (volitelné) - [Stáhnout zde](https://git-scm.com/)
- **IDE** (doporučeno):
  - [Visual Studio 2022](https://visualstudio.microsoft.com/) nebo
  - [Visual Studio Code](https://code.visualstudio.com/) nebo
  - [JetBrains Rider](https://www.jetbrains.com/rider/)

---

## 🚀 Instalace a spuštění

### 1. Klonování/stažení projektu

**Pomocí Git:**
```bash
git clone https://github.com/Mikydosh/dorucovaci_sluzba.git
cd DorucovaciSluzba
```

**Nebo stáhni ZIP** a rozbal do složky.

Spusť soubor **DorucovaciSluzba.sln**

---

### 2. Konfigurace MySQL

#### a) Změň port MySQL na 3300

**MySQL Workbench:**
1. Otevři MySQL Workbench
2. Klikni na **Server** → **Options File**
3. V sekci **Networking** nastav **Port** na `3300`
4. Restartuj MySQL server

**Nebo v konfiguračním souboru:**

**Windows:** `C:\ProgramData\MySQL\MySQL Server 8.0\my.ini`
```ini
[mysqld]
port = 3300
```

**Linux/Mac:** `/etc/mysql/my.cnf`
```ini
[mysqld]
port = 3300
```

**Restartuj MySQL:**
```bash
# Windows (jako admin)
net stop MySQL80
net start MySQL80

# Linux
sudo systemctl restart mysql

# Mac
brew services restart mysql
```

#### b) Uprav connection string

**Soubor:** `DorucovaciSluzba/appsettings.json`

```json
{
  "ConnectionStrings": {
    "MySQL": "server=localhost;port=3300;database=dorucovaci_sluzba;user=root;password=TVOJE_HESLO"
  }
}
```

**⚠️ DŮLEŽITÉ:** 
- Změň `TVOJE_HESLO` na svoje MySQL heslo!
- Port je nastaven na `3300`

---

### 3. Spuštění migrací

Otevři **terminál/příkazovou řádku** v root složce projektu a spusť:

```bash
# Přejdi do složky s hlavním projektem
cd DorucovaciSluzba

# Vytvoř databázové tabulky
dotnet ef database update --project ../DorucovaciSluzba.Infrastructure
```

**Nebo v Package Manager Console (Visual Studio):**

⚠️ **Musí být zvolena DorucovaciSluzba.Infrastructure**

```powershell
Update-Database
```

**Migrace automaticky:**
- Vytvoří všechny tabulky
- Vygeneruje role (Admin, Podpora, Kurýr, Uživatel)
- Vytvoří testovací uživatele s různými rolemi
- Vytvoří základní zásilky s vazbami na již vytvořené uživatele

---

### 4. Spuštění aplikace

```bash
# V root složce projektu
dotnet run --project DorucovaciSluzba
```

**Nebo v Visual Studio:**
- Stiskni `F5` nebo klikni na **▶ https**

⚠️ **Pokud nevidíš** `▶ https`, je potřeba kliknout v průzkumníku řešení pravým tlačítkem myši  na vrstvu `DorucovaciSluzba` a vybrat `Nastavit jako projekt pro spuštění`

Aplikace se spustí na:
- **HTTPS:** `https://localhost:7076`
- **HTTP:** `http://localhost:5076`
a spustí prohlížeč

Nebo otevři prohlížeč a přejdi na: **https://localhost:7076**

---

## 🔐 Testovací účty

Aplikace při prvním spuštění automaticky vytvoří testovací účty pro všechny role:

### 👑 Admini

| Jméno | Email | Username | Heslo | 
|-------|-------|-------|-------|
| Admin Admin | `admin@kuryr.cz` | `admin` | `Admin123` |
| Mikydosh Mikydosh | `mikydosh@kuryr.cz` | `Mikydosh` | `Mikydosh1` |

**Oprávnění:**
- Plný přístup ke všemu
- Správa uživatelů (vytvoření, úprava, mazání, přiřazení rolí)
- Správa zásilek (úprava stavu, přiřazení kurýra)
- Přehled všech zásilek a uživatelů

---

### 🛠️ Podpora

| Jméno | Email | Username | Heslo | 
|-------|-------|-------|-------|
| Support Support | `support@kuryr.cz` | `Support` | `Support1` |

**Oprávnění:**
- Správa zásilek (úprava stavu, přiřazení kurýra)
- Přehled všech zásilek
- **Nemůže** spravovat uživatele

---

### 🚚 Kurýři

| Jméno | Email | Username | Heslo | 
|-------|-------|-------|-------|
| Petr Svoboda | `petr.svoboda@email.cz` | `petr.svoboda`| `Kuryr1` |
| Lukáš Černý | `lukas.cerny@gmail.com` | `lukas.cerny` | `Kuryr1` |
| Martin Veselý | `martin_vesely@seznam.cz` | `martin_vesely` | `Kuryr1` |

**Oprávnění:**
- Přehled **pouze vlastních přiřazených zásilek**
- Změna stavu zásilky
- **Nemůže** přiřazovat jiné kurýry

---

### 👤 Uživatelé

| Jméno | Email | Username | Heslo | 
|-------|-------|-------|-------|
| Karel Procházka | `karel.prochazka@email.cz` | `karel.prochazka` | `Uzivatel1` |
| Eva Málková | `eva.malkova@email.cz` | `eva.malkova` | `Uzivatel1` |
| Jana Horáková | `jana_horakova@seznam.cz` | `jana_horakova` | `Uzivatel1` |
| Pavel Dobrý | `paveldobry@gmail.com` | `paveldobry` | `Uzivatel1` |
| Kateřina Dobrá | `katerina.dobra@gmail.com` | `katerina.dobra` | `Uzivatel1` |

**Oprávnění:**
- Přehled **pouze vlastních zásilek** (kde je odesílatel nebo příjemce)
- Sledování zásilek pomocí čísla
- Vytváření nových zásilek

---

## 🔐 Uživatelské role

### Hierarchie oprávnění

```
Admin (nejvyšší)
  ├── Správa uživatelů ✅
  ├── Správa zásilek ✅
  └── Přiřazení kurýrů ✅

Podpora
  ├── Správa uživatelů ❌
  ├── Správa zásilek ✅
  └── Přiřazení kurýrů ✅

Kurýr
  ├── Správa uživatelů ❌
  ├── Správa zásilek (pouze vlastní) ✅
  └── Přiřazení kurýrů ❌

Uživatel (nejnižší)
  ├── Správa uživatelů ❌
  ├── Správa zásilek (pouze vlastní) ✅
  └── Přiřazení kurýrů ❌
```

---

## 🎨 Barevná paleta

Aplikace používá konzistentní barevnou paletu inspirovanou ilustracemi:

| Barva | Hex kód | RGB |
|-------|---------|-----|
| **Primary** | `#537280` | `rgb(83, 114, 128)` |
| **Secondary** | `#B8CDD9` | `rgb(184, 205, 217)` |
| **Success** | `#6BB77B` | `rgb(107, 183, 123)` |
| **Info** | `#E5C89B` | `rgb(229, 200, 155)` |
| **Warning** | `#E8A87C` | `rgb(232, 168, 124)` |
| **Danger** | `#D96B5C` | `rgb(217, 107, 92)` |

---

## 🐛 Řešení problémů

### ❌ Chyba: "Connection refused" při spuštění
**Řešení:**
- Zkontroluj, že **MySQL server běží na portu 3300**
- Ověř **connection string** v `appsettings.json` (obsahuje `port=3300`)
- Zkontroluj **username a password** pro MySQL
- Zkus připojení manuálně: `mysql -h localhost -P 3300 -u root -p`

### ❌ Chyba: "No database provider has been configured"
**Řešení:**
- Spusť migrace: `dotnet ef database update --project DorucovaciSluzba.Infrastructure`
- Zkontroluj, že je správně nakonfigurován `AppDbContext` v `Program.cs`

### ❌ Chyba: "Port 7076 is already in use"
**Řešení:**
- Změň port v `Properties/launchSettings.json`
- Nebo zastav jinou běžící instanci aplikace
- Nebo použij jiný port: `dotnet run --urls="https://localhost:7077"`

### ❌ Nedaří se přihlásit s testovacími účty
**Řešení:**
- Ujisti se, že jsi spustil migrace (vytvoří se testovací účty automaticky)
- Zkontroluj, že používáš správné **heslo** (viz [Testovací účty](#-testovací-účty))
- Zkus resetovat databázi: `dotnet ef database drop` a poté `dotnet ef database update`

### ❌ Opera/Chrome zobrazuje vlastní error stránku místo custom
**Řešení:**
- Vymaž browser cache (`Ctrl + Shift + Delete`)
- Zkus inkognito mód
- Ujisti se, že v `Program.cs` je správně nakonfigurován error handling

### ❌ Obrázky se nezobrazují
**Řešení:**
- Zkontroluj, že existují v `wwwroot/images/`
- Vymaž browser cache
- Zkontroluj cestu v kódu: `~/images/nazev-obrazku.png`

### ❌ MySQL běží na portu 3306 místo 3300
**Řešení:**
1. Zastav MySQL server
2. Změň port v konfiguračním souboru (viz [Konfigurace MySQL](#2-konfigurace-mysql))
3. Restartuj MySQL server
4. Ověř port: `netstat -an | grep 3300` (Linux/Mac) nebo `netstat -an | findstr 3300` (Windows)

---

## 📄 Licence

Tento projekt je vytvořen jako **školní projekt** a je volně dostupný pro vzdělávací účely.

---

## 👨‍💻 Autor

**Michal Dolanský**
(SWI, 3. ročník)

---
