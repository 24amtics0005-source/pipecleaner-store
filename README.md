# Pipecleaner Store 🛒

A full-stack e-commerce web application built with **ASP.NET Core Razor Pages** and **SQLite**, targeting **.NET 10**.

## Features

- 🛍️ Product listing with categories
- 🛒 Shopping cart (session-based)
- ✅ Checkout & order management
- 🔧 Admin panel
- 💾 SQLite database with Entity Framework Core migrations
- 🌱 Automatic database seeding on first run

## Tech Stack

| Layer | Technology |
|---|---|
| Framework | ASP.NET Core 10 Razor Pages |
| ORM | Entity Framework Core 9 |
| Database | SQLite (via `pipecleaner.db`) |
| Frontend | HTML / CSS / JavaScript |
| Session | ASP.NET Core session middleware |

## Getting Started

### Prerequisites

- [.NET 10 SDK](https://dotnet.microsoft.com/download)

### Run Locally

```bash
# Clone the repo
git clone <your-repo-url>
cd "web.net project"

# Restore packages
dotnet restore

# Run (auto-applies migrations and seeds data)
dotnet run
```

The app will start at **http://localhost:5100**

## Project Structure

```
web.net project/
├── Data/               # ApplicationDbContext
├── Migrations/         # EF Core migration files
├── Models/             # Domain models (Product, Order, CartItem, OrderItem)
├── Pages/              # Razor Pages (Index, Cart, Checkout, Admin, Products)
│   ├── Admin/
│   ├── Checkout/
│   └── Products/
├── Services/           # StoreService (business logic)
├── wwwroot/            # Static assets (CSS, JS, images)
├── Program.cs          # App entry point & DI setup
├── appsettings.json    # Configuration
└── web.net project.csproj
```

## Notes

- The SQLite database (`pipecleaner.db`) is **not tracked by Git** — it is auto-created when you first run the app.
- Products are seeded automatically if the database is empty.
