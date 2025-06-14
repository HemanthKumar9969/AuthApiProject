## User Authentication & Authorization API with JWT ##

## Project Overview:

This project is a secure and scalable ASP.NET Core Web API that provides foundational user authentication and authorization for modern applications. It supports:

1: User registration with encrypted credentials (using BCrypt)

2: JWT-based login for stateless authentication

3: Access control to protected API endpoints via token validation

Built with Entity Framework Core and MS SQL Server, it ensures robust data persistence. The API follows security best practices with centralized configuration and integrates Swagger UI for interactive API documentation and easy testing.

## Key Features:

1: Secure User Registration: Implements a robust registration process that includes password hashing with unique salts (using BCrypt) to protect user credentials against common attack vectors like rainbow tables and brute-force attempts.

2: JWT-Based Authentication: Provides a streamlined login mechanism where successful authentication results in the issuance of a short-lived JSON Web Token, used by clients to establish authenticated sessions without relying on server-side session state.

3: Endpoint Protection: Ensures that critical API endpoints are protected, verifying the authenticity and validity of every incoming JWT to restrict access solely to authorized users.

4: Role-Based Authorization (RBAC): Enables granular control over API access by assigning specific roles (e.g., User, Admin) to users, allowing different endpoints or functionalities to be restricted based on the authenticated user's permissions.

5: Password Security Best Practices: Leverages BCrypt's adaptive hashing to store passwords, making them computationally expensive to crack and highly resistant to offline attacks.

6: Centralized Configuration: Manages sensitive information, such as database connection strings and the JWT signing secret, securely through ASP.NET Core's configuration system, utilizing environment-specific files (appsettings.Development.json) to prevent credentials from being exposed in source control.

7: Interactive API Documentation: Integrates Swagger UI to provide a live, interactive documentation portal for all API endpoints, significantly aiding developers in understanding, testing, and integrating with the API during development.

## Technologies Used:

1: Backend Framework: ASP.NET Core 8.0 Web API (compatible with .NET SDK 8.0+)

2: Database: MS SQL Server 2022

2: ORM (Object-Relational Mapper): Entity Framework Core 8.0+

3: Password Hashing Library: BCrypt.Net-Next

4: Authentication & Authorization Standard: JSON Web Tokens (JWT)

5: API Documentation Tool: Swashbuckle.AspNetCore (for Swagger UI generation)

6: Development Environment: Visual Studio Code 2022

7: API Testing Tool: Thunder Client (VS Code Extension), compatible with Postman/Insomnia

## Prerequisites: 

1: .NET SDK (8.0+): Download here

2: Visual Studio Code: With the C# extension (from Microsoft).

3: MS SQL Server 2022: (or compatible version) running locally.

4: SQL Server Management Studio (SSMS) or Azure Data Studio: For database management.

5: Thunder Client: VS Code Extension for API testing.

## Getting Started
Follow these steps to set up and run the project locally.

## Prerequisites
Ensure the following tools are installed:

1:.NET SDK 8.0+

2: Visual Studio Code with the C# Extension

3: Microsoft SQL Server 2022 or compatible version

4: SQL Server Management Studio (SSMS) or Azure Data Studio

5: Thunder Client (VS Code extension for testing APIs)

## Setup Instructions
1: Clone the Repository:

git clone https://github.com/HemanthKumar9969/AuthApiProject.git

cd AuthApiProject/AuthApi

2: Restore NuGet Packages:

dotnet restore

3: Configure appsettings.Development.json:

Create a file named appsettings.Development.json in the AuthApi project folder (same location as appsettings.json).
Add your configuration details:

![image](https://github.com/user-attachments/assets/bbf3b563-7f7a-4977-a256-500bdb83a6f7)

4️: Apply Database Migrations:

dotnet ef database update

5️: Run the Application: 

dotnet run

The API will be available at:

https://localhost:5001/swagger

![Screenshot (198)](https://github.com/user-attachments/assets/0530b149-0036-4bcf-8a35-208b58334475)

## API Endpoints and Testing:
### Testing the API with Thunder Client (Step-by-Step)

All API endpoints are hosted at http://localhost:5285. We'll use Thunder Client for testing.

## Test 1: User Registration

Objective: Create a new user account.

Request Setup:

1: Method: Change the dropdown from GET to POST.
2: URL: Enter http://localhost:5285/api/Auth/register (or https://localhost:7208/api/Auth/register if you're using HTTPS).
3: Headers Tab:
3: Add a new header:
4: Name: Content-Type
5: Value: application/json
6: Body Tab:
7: Select JSON from the dropdown.
8: Paste the following JSON into the text area:

{
  "username": "thunderuser",
  "email": "thunder@example.com",
  "password": "ThunderPassword123!"
}

You can change these values.
Send Request: Click the Send button (green play icon).
Expected Response:
Status: 200 OK
Response Body: "Registration successful!"
***Try sending this request again with the exact same username/email. You should get a 400 Bad Request with a message like "Username already exists." or "Email already exists."***
