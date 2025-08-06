# Birthday tracker

Simple CRUD ASP.NET Core MVC web app for storing and tracking birthday dates.

## Features

- Today and near birthdays display
- All CRUD operations
- Sort by id, name and date
- Images display

## Project structure

- `Controllers/` — MVC controllers
- `Models/` — models, view models and DTO
- `Views/` — Razor pages
- `Service/` — buisness logic
- `Repositories/` — operations with data
- `Data/` — data access (db context)
- `wwwroot/images/` — user images
- `Utils/` — helping utils

## Tech Stack

- ASP.NET Core MVC (.NET 9)
- Entity Framework Core
- Razor Views + Bootstrap 5
- MSSQL

## Getting Started

### 1. Clone the Repository

```bash
git clone https://github.com/delerus/Birthday-tracker
cd Birthday-tracker
```

### 2. Setup the app

Update connection string in `appsettings.json`
```bash
"ConnectionStrings": {
  "DatabaseConnection": "Your connection string"
}
```
Setup dependencies
```bash
dotnet restore
```
Run migrations
```bash
dotnet ef database update
```

### 3. Run the app

```bash
dotnet run
```
or use `dotnet watch` for hot reload
```bash
dotnet watch run
```

Additionaly you can run sql query in `testData.sql` for test data slice.