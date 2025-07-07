# Employee Management App

A secure and extensible ASP.NET Core MVC application for managing employees, roles, and users with role-based access control, built with ASP.NET Identity and Entity Framework Core.

## Features

- ASP.NET Core MVC with Razor Views
- User Authentication & Authorization using ASP.NET Identity
- Role-based Access Control (Admin/User)
- Full CRUD operations for Employee records
- ViewModels for clean UI logic separation
- Custom validation attributes (e.g., email domain enforcement)
- SQL Server integration via Entity Framework Core
- Logging with NLog
- Custom error and access-denied pages

## Project Structure

```

EmployeeManagement/
├── Controllers/
├── DataAccess/
├── Models/
├── ViewModels/
├── Views/
│   ├── Account/
│   ├── Administration/
│   ├── Home/
│   └── Shared/
├── Utilities/
│   └── ValidEmailDomainAttribute.cs
├── wwwroot/
├── Program.cs
└── appsettings.json

````

## ViewModels

- `LoginViewModel`
- `RegisterViewModel`
- `CreateRoleViewModel`
- `EditRoleViewModel`
- `EditUsersInRoleViewModel`
- `EditUserViewModel`
- `UserRolesViewModel`
- `EmployeeCreateViewModel`
- `EmployeeEditViewModel`
- `HomeDetailsViewModel`

## Custom Validation

- `ValidEmailDomainAttribute.cs`: Ensures user registration is restricted to a specific email domain (e.g., `@company.com`)

## Technologies

- Framework: ASP.NET Core 8 MVC
- Data Access: Entity Framework Core + SQL Server
- Authentication: ASP.NET Identity
- Frontend: Razor Pages (`.cshtml`)
- Logging: NLog

## Authorization

- Fallback policy requires all routes to be authenticated by default.
- Admin users can:
  - Manage roles and users
  - Assign and remove users from roles
- Users can:
  - View and update their employee profile

Tip: To promote a user to Admin, assign them the "Admin" role manually or via the UI if available.

## Getting Started

### Prerequisites

- .NET 8 SDK
- SQL Server (LocalDB or full edition)
- EF Tools (install via `dotnet tool install --global dotnet-ef`)
- Visual Studio 2022+ or VS Code

### Installation

1.  Clone the repository:
    ```bash
    git clone https://github.com/abisoyeo/employee-management-app.git)
    cd employee-management-app
    ```
2.  Configure the connection string in `appsettings.json`:
    ```json
    "ConnectionStrings": {
      "EmployeeDbConnection": "Server=localhost;Database=EmployeeDB;Trusted_Connection=True;"
    }
    ```
3.  Apply migrations to set up the database:
    ```bash
    dotnet ef database update
    ```
4.  Run the app:
    ```bash
    dotnet run
    ```
    Using Visual Studio: Open the `.sln` file and press `F5` to build and run.

### Pages & Routes

* `/Account/Register` – Register a new user
* `/Account/Login` – Login page
* `/Administration/ListUsers` – View users (admin only)
* `/Administration/ListRoles` – View/manage roles
* `/Home/Index` – Employee list
* `/Home/Create` – Create new employee
* `/Home/Edit/{id}` – Edit employee
* `/Home/Details/{id}` – Employee details

### Error Handling

* `/Error/404` – Not Found
* `/Error/500` – Internal Server Error
* `/Account/AccessDenied` – Unauthorized access

Handled via:
```csharp
app.UseStatusCodePagesWithReExecute("/Error/{0}");
````

## License

This project is licensed under the MIT License. See the `LICENSE` file for details.
