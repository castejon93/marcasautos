# MarcasAutos API

## 📋 Quick Links

- [Setup](#setup-instructions)
- [Running](#running-the-application)
- [Tests](#running-tests)
- [API](#api-endpoints)

---

## 🔧 Tech Stack

| Component | Technology |
|-----------|-----------|
| **Framework** | ASP.NET Core / .NET 10 |
| **Language** | C# 14.0 |
| **Database** | PostgreSQL |
| **ORM** | Entity Framework Core |
| **Testing** | xUnit + Testcontainers |

---

## 📁 Project Structure

```
MarcasAutos/
├── MarcasAutos/                      # Main API project
│   ├── Controllers/MarcasAutosController.cs
│   ├── Models/MarcaAuto.cs
│   ├── Data/AppDbContext.cs
│   ├── Migrations/MigrationExtension.cs
│   └── appsettings.json
├── MarcasAutos.Tests/                # Integration tests
│   ├── MarcasAutosTests.cs
│   ├── CustomWebApplicationFactory.cs
│   ├── Helpers.cs
│   └── appsettings.json
├── docker-compose.yml
└── README.md
```

---

## 📦 Prerequisites

- **.NET SDK 10.0** or later
- **Docker** and **Docker Compose**
- **Visual Studio 2022** or **VS Code** (optional)

---

## 🚀 Setup Instructions


### 2. Start PostgreSQL

```bash
docker-compose up -d
```

**Connection Details:**
- Host: `localhost:5432`
- Database: `marcasautosdb`
- Username: `postgres`
- Password: `postgres123`

### 3. Restore & Run Migrations

```bash
dotnet restore
dotnet ef database update --project MarcasAutos
```

---

## ▶️ Running the Application

### Using .NET CLI

```bash
cd MarcasAutos
dotnet run
```

**Access:** `http://localhost:5000` or `https://localhost:5001`

### Using Docker Compose

```bash
docker-compose up
```

---

## 🧪 Running Tests

Tests automatically spin up PostgreSQL containers via Testcontainers.

```bash
# Run all tests
dotnet test

# Run specific test
dotnet test --filter "GetAll_ReturnsExpectedData"
```

---

## 🔌 API Endpoints

### Get All Car Brands

```http
GET /MarcasAutos
```

**Response (200 OK):**
```json
[
  {
    "id": 1,
    "name": "Toyota",
    "country": "Japón",
    "year": 1937
  },
  {
    "id": 2,
    "name": "Ford",
    "country": "Estados Unidos",
    "year": 1903
  }
]
```

---

## 🗄️ Database

### Schema

**Table: MarcasAutos**

| Column | Type | Notes |
|--------|------|-------|
| id | INT | Primary Key |
| name | VARCHAR | Brand name |
| country | VARCHAR | Country of origin |
| year | INT | Year founded |

### Connection Strings

```
Host=marcasautos.db;Port=5432;Database=marcasautosdb;Username=postgres;Password=postgres123
```

