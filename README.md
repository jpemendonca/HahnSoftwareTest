# CryptoInfoTracker

CryptoInfoTracker is a fullstack software solution developed as part of a technical assessment for **Hahn Software**. 
The project integrates a well-structured backend built in **.NET 9** using **Minimal APIs** with a modern **Vue.js + TypeScript** frontend, incorporating **Hangfire** for background job processing. It leverages **Clean Architecture**, **Domain-Driven Design (DDD)**, and **SOLID** principles.

---

![image](https://github.com/user-attachments/assets/cc1930bd-74a5-4f99-ba3d-089b22a8ecc3)


## 🌍 Purpose

The application’s core purpose is to:

- Fetch live cryptocurrency data from the public **Coinlore API**
- Store and update this data periodically using a **background job**
- Expose the data via a clean **REST API**
- Display it in a responsive, filterable **frontend table**

---


## 📦 How to Run the Project Locally

### Requirements
- .NET 9 SDK
- SQL Server (I used SQLServer on Docker)
- [Node.js](https://nodejs.org/)
- (Optional) EF Core CLI: `dotnet tool install --global dotnet-ef`

---

### ✅ 1. Web API (Hahn.WebApi)

This is the minimal API exposing `/cryptos`.

```bash
cd HahnSoftwareTest\src\Hahn.WebApi

# Restore and build
dotnet build

# Apply DB migrations (migration assembly is Hahn.WebApi)
dotnet ef database update

# Run the WebAPI
dotnet run
```

### ✅ 2. Worker Service (Hahn.WorkerService)
```bash
cd HahnSoftwareTest\src\Hahn.WorkerService

dotnet run
```

### ✅ 3. Frontend
```bash
cd HahnSoftwareTest\frontend
npm install
npm run dev
```


## 📂 Project Structure

```
HahnSoftwareTest/
├── src/                         # Backend source folder
│   ├── Hahn.Application         # Use cases, interfaces, UpsertCryptoService (CoinLore api access)
│   ├── Hahn.Domain              # Core domain entities, models, enums, repository interface
│   ├── Hahn.Infrastructure      # Data access with EF Core, AppDbContext
│   ├── Hahn.Jobs                # Hangfire job logic (CryptoUpsertJob)
│   ├── Hahn.WorkerService       # Host for background job scheduler
│   └── Hahn.WebApi              # Minimal API that exposes crypto data
└── frontend/                    # Vue 3 + TypeScript frontend
```

## ✨ Key Backend Features

### 🛠️ Worker Service with Hangfire

- The `Hahn.WorkerService` hosts a background job using **Hangfire**.
- Job scheduled via `RecurringJob.AddOrUpdate` to run **every hour**.
- The `CryptoUpsertJob` calls the `UpsertCryptoService`, which:
  - Sends a GET request to **Coinlore API**
  - Deserializes the response into domain models
  - Maps to internal `CryptoCurrency` entity
  - **Upserts** the data using a repository pattern and **EF Core**
- Dependencies registered via `AddWorkerServices()`

### 💡 Web API with Clean Architecture

- API hosted in `Hahn.WebApi` using **.NET Minimal APIs**
- Main endpoint: `GET /cryptos`
- Supports **sorting** by:
  - Name
  - Symbol
  - Price
  - 24h change
  - Market cap
- Sorting is dynamic via enums: `EnumSortBy` and `EnumSortDirection`
- Query parameters wrapped in `CryptoQueryParams`
- Business logic lives in `Hahn.Application`

### 📁 Infrastructure Layer

- Implements:
  - HTTP calls to **Coinlore API**
  - Data mapping and transformation
  - EF Core `DbContext` with **SQL Server** provider
- `UpsertAsync()` method handles full transformation of Coinlore data into domain entities

---

## 💻 Frontend – Vue 3 + TypeScript

- SPA built with **Vue 3** + **TypeScript** + **Vite**
- Styling via **Tailwind CSS** and **Flowbite UI**

### Core Features:

- Filter cryptos by **name** or **symbol**
- Sort by **top gainers**, **losers**, **market cap**
- **Pagination** support
- Main component: `App.vue`
- Custom composable: `useCryptos.ts` handles:
  - API data fetching
  - Filtering, sorting, pagination logic
  - Uses `fetch` or `axios` for API communication

---

## 🔗 Tech Stack Summary

### 📁 Backend:
- .NET 8
- Entity Framework Core
- SQL Server
- Hangfire
- Clean Architecture + DDD + SOLID

### 🔮 Frontend:
- Vue 3
- TypeScript
- Tailwind CSS
- Flowbite UI
- Vite

---

## ⚖️ How the App Works (Flow Summary)

1. Hangfire job runs every hour
2. Calls **Coinlore API**
3. Upserts crypto data into **SQL Server** database
4. Web API exposes `/cryptos` endpoint
5. Frontend fetches from API
6. Displays data in styled table with **filtering**, **sorting**, and **pagination**
