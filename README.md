# Archival Library - Backend

This backend service, built with .NET, facilitates the uploading, cataloging, and secure storage of documents. It handles user authentication, manages document metadata, and integrates with a PostgreSQL database for persistent storage.

## Features

- **User Authentication**: Implements secure user registration, login, and session management using JWT, ensuring that only authorized users can interact with the system.
- **Document Management**: Allows users to upload documents with associated metadata such as title, description, and category.
- **Metadata Storage**: Stores document metadata in a PostgreSQL database, enabling easy retrieval and management of records.
- **Error Handling**: Robust error handling with custom middleware to manage exceptions gracefully.
- **Security**: Ensures secure access with JWT authentication and secure database connections.

## Tech Stack

- **.NET 8**: Backend framework used for building a robust and scalable REST API.
- **PostgreSQL**: Database system used for persistent storage of user and document metadata.
- **Entity Framework Core**: ORM (Object-Relational Mapper) used to interact with the PostgreSQL database.
- **ASP.NET Core Identity**: Used for handling user authentication and authorization, including JWT-based authentication.
- **DotNetEnv**: Used for loading environment variables from `.env` files into the application configuration.

## Project Structure

- `Controllers/`: Handles incoming HTTP requests and defines the API endpoints.
- `Services/`: Contains business logic related to document management, authentication, and other operations.
- `Repositories/`: Manages data access to the PostgreSQL database using Entity Framework Core.
- `Entities/`: Defines the database models and entities used within the application.
- `Middlewares/`: Custom middleware for exception handling and logging.
- `Startup/`: Configures services, middleware, and the application pipeline.
- `Data/`: Contains database context and migration files for Entity Framework Core.
- `Dtos/`: Defines Data Transfer Objects used to transfer data between layers.
- `Exceptions/`: Handles custom exceptions for better error management within the application.
- `Interfaces/`: Defines interfaces for services and repositories to promote dependency injection and loose coupling.

## Getting Started

### Prerequisites

- **.NET SDK 8.0**: Ensure you have the .NET SDK installed.
- **PostgreSQL**: Ensure PostgreSQL is installed and running.
- **Microsoft Visual Studio (Recommended)**

### Installation

1. Clone the repository:

   ```bash
   git clone https://github.com/OmaralAnazi/archival-library-backend.git

   ```

2. Navigate to the project directory (make sure you are at the project folder, not the solution folder):

   ```bash
   cd archival-library-backend

   ```

3. Install dependencies:

   ```bash
    dotnet restore

   ```

4. To configure your production environment, create a `.env.production` file in the root directory of your solution. Add the necessary environment variables, such as the connection string to your database. Ensure the database exists and the specified user has appropriate permissions. You can also create a `.env.development` file for your development environment. Use This Template for any of your `.env` files:

   ```bash
    # Connection string
    ConnectionStrings__DefaultConnection=...

    # JWT configuration
    JWT__Issuer=...
    JWT__Audience=...
    JWT__SigningKey=...

    # File upload path
    FileStorage__UploadPath=...

   ```

5. Run the application (Ctrl + F5):

   Production:

   ```bash
   dotnet run --launch-profile "https (prod)"
   ```

   Or Development:

   ```bash
   dotnet run --launch-profile "https (dev)"

   ```
