# BonsaiShop

A **BonsaiShop** egy modern webáruház, amely bonsai fák és bonsai kiegészítők online értékesítésére készült.
A projekt célja egy gyors, biztonságos és felhasználóbarát webshop létrehozása, ahol a vásárlók egyszerűen böngészhetnek a termékek között, leadhatják rendeléseiket, valamint kezelhetik saját felhasználói fiókjukat.

---

## Technológiai környezet

- **Frontend:** Vue.js  
- **Backend:** C# ASP.NET Core Web API  
- **Adatbázis:** SQLite / Entity Framework Core  
- **Autentikáció:** JWT + Refresh Token
- **Kommunikáció:** Fetch API  
- **Fejlesztői eszközök:** Visual Studio 2022, Visual Studio Code  
- **Verziókezelés:** Git + GitHub  

---

## Főbb funkciók

### Vásárlói funkciók

- Regisztráció / Bejelentkezés
- Felhasználói profil kezelése
- Termékek böngészése kategóriák szerint
- Termék részletek megtekintése
- Kosár kezelés
- Rendelés leadása
- Saját rendelések megtekintése

### Admin funkciók

- Termékek hozzáadása / módosítása / törlése
- Felhasználók kezelése
- Rendelések kezelése
- Jogosultság alapú hozzáférés

---

## Felhasználói szerepkörök

### 1. Adminisztrátor

Az adminisztrátor teljes hozzáféréssel rendelkezik a rendszerhez.  
Feladata a termékek, rendelések és felhasználók kezelése.

### 2. Vásárló

A vásárló böngészhet a termékek között, rendelést adhat le, kezelheti profilját és nyomon követheti rendeléseit.

---

## Biztonsági megoldások

- JWT token alapú hitelesítés
- Refresh token kezelés
- Jogosultság ellenőrzés
- Jelszó titkosítás
- Hibakezelés egységes API válaszokkal
- Input validáció

---

## Projekt állapota
A projekt jelenleg aktív fejlesztés alatt áll.

---

## Telepítés és futtatás

### Backend indítása
```bash
cd backend/bonsai_webshop
dotnet restore
dotnet run
```

### Frontend indítása
```bash
cd frontend
npm install
npm run serve
```