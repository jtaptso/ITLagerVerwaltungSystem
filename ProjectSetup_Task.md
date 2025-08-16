# Project Setup – IT Warehouse Management System

This guide details the actionable steps for setting up the solution foundation using Clean Architecture, ASP.NET Core 9, Entity Framework Core, and SQL Server.

---

## 1. Initialize Solution Structure

- Create a new solution (e.g., `ITLagerVerwaltungSystem.sln`).
- Add the following projects:
  - `ITLagerVerwaltungSystem.API` (ASP.NET Core 9 Web API)
  - `ITLagerVerwaltungSystem.Core` (Domain & Application Layer)
  - `ITLagerVerwaltungSystem.Infrastructure` (Data Access Layer)
  - `ITLagerVerwaltungSystem.Client` (UI: React, Blazor, or WPF)
  - `ITLagerVerwaltungSystem.Shared` (Common types/utilities)

## 2. Configure Clean Architecture Folder Structure

- Ensure each project has folders for Entities, Interfaces, Services, DTOs, etc.
- Reference projects appropriately:
  - `API` references `Core` and `Infrastructure`
  - `Infrastructure` references `Core`
  - `Client` references `Shared` (and API via HTTP)
  - `Shared` is referenced by all

## 3. Set Up ASP.NET Core 9 Web API Project

- Create the API project targeting .NET 9.
- Add necessary NuGet packages (ASP.NET Core, Swashbuckle for Swagger, etc.).
- Configure startup, dependency injection, and middleware.

## 4. Set Up Entity Framework Core with SQL Server

- Add EF Core and SQL Server NuGet packages to `Infrastructure`.
- Configure `DbContext` and connection string in `appsettings.json`.
- Register `DbContext` in API's DI container.

## 5. Create Initial Database Migration and Apply to SQL Server

- Use EF Core CLI or Package Manager Console:
  - `dotnet ef migrations add InitialCreate`
  - `dotnet ef database update`
- Verify database schema in SQL Server.

## 6. Set Up Client Project

- Choose client technology (React, Blazor, WPF).
- Scaffold initial project.
- Set up API communication (e.g., HttpClient, Axios).
- Configure authentication (if needed).

---

## Checklist

```markdown
- [ ] Solution structure created
- [ ] Clean Architecture folders configured
- [ ] ASP.NET Core 9 API project set up
- [ ] Entity Framework Core & SQL Server configured
- [ ] Initial migration applied
- [ ] Client project scaffolded
```

---

## Notes
- Use Visual Studio or CLI for project/solution management.
- Store connection strings and secrets securely (e.g., user secrets, environment variables).
- Use Git for source control and commit initial setup.

---
