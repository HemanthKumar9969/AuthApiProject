## User Authentication & Authorization API with JWT

## Project Overview

This project delivers a secure and robust Backend API built on ASP.NET Core Web API, designed to serve as the foundational authentication and authorization layer for modern applications. It meticulously handles critical user security processes, enabling clients to:

## Register new user accounts with encrypted credentials.
## Authenticate and log in, receiving a secure JSON Web Token (JWT) for subsequent interactions.
## Access protected resources by presenting a valid JWT, ensuring only authenticated users can interact with sensitive API endpoints.

The architecture emphasizes stateless authentication through JWTs, employs BCrypt for industry-standard password hashing, and integrates seamlessly with MS SQL Server for reliable data persistence via Entity Framework Core. Adhering to best practices, the API includes secure configuration management and clear, interactive documentation via Swagger UI, facilitating easy development and testing.

## Key Features
🔐 Secure User Registration: Implements a robust registration process that includes password hashing with unique salts (using BCrypt) to protect user credentials against common attack vectors like rainbow tables and brute-force attempts.

🔑 JWT-Based Authentication: Provides a streamlined login mechanism where successful authentication results in the issuance of a short-lived JSON Web Token, used by clients to establish authenticated sessions without relying on server-side session state.

🛡️ Endpoint Protection: Ensures that critical API endpoints are protected, verifying the authenticity and validity of every incoming JWT to restrict access solely to authorized users.

👥 Role-Based Authorization (RBAC): Enables granular control over API access by assigning specific roles (e.g., User, Admin) to users, allowing different endpoints or functionalities to be restricted based on the authenticated user's permissions.

🔒 Password Security Best Practices: Leverages BCrypt's adaptive hashing to store passwords, making them computationally expensive to crack and highly resistant to offline attacks.

⚙️ Centralized Configuration: Manages sensitive information, such as database connection strings and the JWT signing secret, securely through ASP.NET Core's configuration system, utilizing environment-specific files (appsettings.Development.json) to prevent credentials from being exposed in source control.

📄 Interactive API Documentation: Integrates Swagger UI to provide a live, interactive documentation portal for all API endpoints, significantly aiding developers in understanding, testing, and integrating with the API during development.

## Technologies Used
1: Backend Framework: ASP.NET Core 8.0 Web API (compatible with .NET SDK 8.0+)

2: Database: MS SQL Server 2022

2: ORM (Object-Relational Mapper): Entity Framework Core 8.0+

3: Password Hashing Library: BCrypt.Net-Next

4: Authentication & Authorization Standard: JSON Web Tokens (JWT)

5: API Documentation Tool: Swashbuckle.AspNetCore (for Swagger UI generation)

6: Development Environment: Visual Studio Code 2022

7: API Testing Tool: Thunder Client (VS Code Extension), compatible with Postman/Insomnia
