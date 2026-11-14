## Getting Started

### Requirements
- .NET 8 SDK
- Visual Studio 2022 or a compatible IDE
- SQL Server LocalDB (or another compatible SQL Server instance)
- Entity Framework Core tools

### Installation

1. Clone the repository:

   ```bash
   git clone YOUR_REPOSITORY_URL
   ```

2. Navigate to the project directory:

   ```bash
   cd CinemaFind
   ```

3. Restore dependencies:

   ```bash
   dotnet restore
   ```

4. Initialize User Secrets:

   ```bash
   dotnet user-secrets init
   ```

5. Configure your database connection:

   ```bash
   dotnet user-secrets set "ConnectionStrings:DefaultConnection" "YOUR_CONNECTION_STRING"
   ```

6. Apply database migrations:

   ```bash
   dotnet ef database update
   ```

7. Run the application:

   ```bash
   dotnet run
   ```

The application will create the required database schema using Entity Framework Core migrations.

**Note:** Each developer must configure their own database connection. Local database files and development secrets are not included in this repository.