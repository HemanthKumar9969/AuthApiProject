User Authentication & Authorization API with JWT
Project Overview
This project delivers a secure and robust Backend API built on ASP.NET Core Web API, designed to serve as the foundational authentication and authorization layer for modern applications. It meticulously handles critical user security processes, enabling clients to:

Register new user accounts with encrypted credentials.

Authenticate and log in, receiving a secure JSON Web Token (JWT) for subsequent interactions.

Access protected resources by presenting a valid JWT, ensuring only authenticated users can interact with sensitive API endpoints.

The architecture emphasizes stateless authentication through JWTs, employs BCrypt for industry-standard password hashing, and integrates seamlessly with MS SQL Server for reliable data persistence via Entity Framework Core. Adhering to best practices, the API includes secure configuration management and clear, interactive documentation via Swagger UI, facilitating easy development and testing.

Key Features
🔐 Secure User Registration: Implements a robust registration process that includes password hashing with unique salts (using BCrypt) to protect user credentials against common attack vectors like rainbow tables and brute-force attempts.

🔑 JWT-Based Authentication: Provides a streamlined login mechanism where successful authentication results in the issuance of a short-lived JSON Web Token, used by clients to establish authenticated sessions without relying on server-side session state.

🛡️ Endpoint Protection: Ensures that critical API endpoints are protected, verifying the authenticity and validity of every incoming JWT to restrict access solely to authorized users.

👥 Role-Based Authorization (RBAC): Enables granular control over API access by assigning specific roles (e.g., User, Admin) to users, allowing different endpoints or functionalities to be restricted based on the authenticated user's permissions.

🔒 Password Security Best Practices: Leverages BCrypt's adaptive hashing to store passwords, making them computationally expensive to crack and highly resistant to offline attacks.

⚙️ Centralized Configuration: Manages sensitive information, such as database connection strings and the JWT signing secret, securely through ASP.NET Core's configuration system, utilizing environment-specific files (appsettings.Development.json) to prevent credentials from being exposed in source control.

📄 Interactive API Documentation: Integrates Swagger UI to provide a live, interactive documentation portal for all API endpoints, significantly aiding developers in understanding, testing, and integrating with the API during development.

Technologies Used
Backend Framework: ASP.NET Core 8.0 Web API (compatible with .NET SDK 8.0+)

Database: MS SQL Server 2022

ORM (Object-Relational Mapper): Entity Framework Core 8.0+

Password Hashing Library: BCrypt.Net-Next

Authentication & Authorization Standard: JSON Web Tokens (JWT)

API Documentation Tool: Swashbuckle.AspNetCore (for Swagger UI generation)

Development Environment: Visual Studio Code 2022

API Testing Tool: Thunder Client (VS Code Extension), compatible with Postman/Insomnia

Core Security & Implementation Concepts
This project emphasizes several critical security and architectural best practices:

1. BCrypt: Secure Password Hashing and Salting
What is BCrypt? BCrypt is an adaptive password hashing function designed to be computationally expensive, making it highly resistant to brute-force attacks and rainbow table attacks. It inherently generates and incorporates a unique salt for each password.

How I used it in this Project:

In Models/User.cs: PasswordHash and PasswordSalt properties are defined as byte[] to store the binary output of the hashing process.

In Controllers/AuthController.cs (Register method):

BCrypt.Net.BCrypt.GenerateSalt() is called to create a unique salt string for each new user.

BCrypt.Net.BCrypt.HashPassword(plainPassword, salt) then hashes the user's password, embedding the salt within the resulting hash string. This hash string is converted to byte[] for database storage.

In Controllers/AuthController.cs (Login method):

BCrypt.Net.BCrypt.Verify(enteredPassword, storedHash) is used to compare the entered password with the stored hash. This function internally extracts the salt from the stored hash and uses it to re-hash the entered password, ensuring a secure comparison without ever exposing the plain-text password.

2. JWT (JSON Web Tokens) & The Secret Key
What is JWT? A JWT is a standard for securely transmitting information between parties as a JSON object. It's used for stateless authentication: once issued, the server doesn't need to maintain session state. The token itself contains the user's claims and a signature.

What is the Secret Key? The Secret Key is a highly confidential string of characters known only to the API server. It's used to cryptographically sign the JWT when it's issued and to verify the JWT's integrity when it's received. If the signature doesn't match (meaning the token was tampered with or issued by an untrusted source), the token is rejected. The security of your API heavily relies on keeping this key secret.

How I created and used it in this Project:

Secret Key Generation: I did not hardcode a simple key. Instead, I created a separate, temporary C# Console Application (JwtKeyGenerator) using System.Security.Cryptography.RNGCryptoServiceProvider. This generated a cryptographically strong, 256-bit (32-byte) random string, which was then Base64-encoded to be safely placed in configuration files. After generation, the JwtKeyGenerator project was deleted to maintain project cleanliness and prevent accidental commits.

Configuration (appsettings.json): The generated secret key, along with Issuer (who issued the token, e.g., your API's domain) and Audience (who the token is for, e.g., your client app's domain) were stored in the JwtSettings section of appsettings.json.

JWT Generation (Controllers/AuthController.cs - CreateToken method): This method builds the JWT. It includes user claims (ID, username, email, role) in the token's payload, retrieves the secret key from configuration, and uses System.IdentityModel.Tokens.Jwt to sign and serialize the token.

JWT Validation (Program.cs): The Microsoft.AspNetCore.Authentication.JwtBearer middleware is configured in Program.cs. It's set up to ValidateIssuer, ValidateAudience, ValidateLifetime, and crucially, ValidateIssuerSigningKey using the same secret key from appsettings.json.

3. Swagger UI: API Documentation and Testing
What is Swagger UI? Swagger UI is an interactive, web-based tool that visualizes and documents RESTful APIs. It's invaluable during development for understanding, exploring, and directly testing API endpoints.

How I used it in this Project:

By adding the Swashbuckle.AspNetCore NuGet package and configuring it in Program.cs (AddSwaggerGen, UseSwagger, UseSwaggerUI), the API automatically generates comprehensive documentation.

A security definition for "Bearer" tokens was added to Swagger, enabling an "Authorize" button in the UI. This allowed me to paste a JWT (obtained from the login endpoint) and test all protected endpoints directly in the browser, significantly streamlining the testing process.

4. Secure Configuration Management
To prevent sensitive information (like SQL Server credentials and the JWT secret) from being pushed to public GitHub repositories:

appsettings.json: Contains general configuration and placeholders for sensitive data. This file is safe to commit to Git.

appsettings.Development.json: Contains the actual, sensitive credentials for local development. This file is ignored by Git.

.gitignore: Explicitly lists appsettings.Development.json (and other IDE-specific files like .vs/, bin/, obj/) to prevent them from being tracked and committed to the repository.

Project Structure and File Overview
AuthApiProject/
├── AuthApi/
│   ├── Controllers/
│   │   ├── AuthController.cs          # Handles user registration and login, JWT generation
│   │   └── UserController.cs          # Example of protected endpoints, demonstrating authorization
│   ├── Data/
│   │   ├── ApplicationDbContext.cs    # Entity Framework Core DbContext, connects models to DB
│   │   └── Migrations/                # Folder for EF Core database migration files
│   ├── DTOs/
│   │   ├── LoginDto.cs                # Data Transfer Object for login requests
│   │   └── RegisterDto.cs             # Data Transfer Object for registration requests
│   ├── Models/
│   │   └── User.cs                    # User data model, defines database table structure
│   ├── Properties/
│   │   └── launchSettings.json        # Development-specific launch configurations (ports, swagger launch)
│   ├── appsettings.json               # Main application configuration (public parts)
│   ├── appsettings.Development.json   # Local-only sensitive configurations (ignored by Git)
│   ├── Program.cs                     # Application startup, configures services and middleware
│   └── AuthApi.csproj                 # Project file, lists NuGet package references
└── .gitignore                         # Specifies files/folders Git should ignore


Getting Started
Follow these steps to set up and run the project locally.

Prerequisites
.NET SDK (8.0+): Download here

Visual Studio Code: With the C# extension (from Microsoft).

MS SQL Server 2022: (or compatible version) running locally.

SQL Server Management Studio (SSMS) or Azure Data Studio: For database management.

Thunder Client: VS Code Extension for API testing.

Setup Instructions
Clone the Repository:

git clone https://github.com/HemanthKumar9969/AuthApiProject.git
cd AuthApiProject/AuthApi

Restore NuGet Packages:

dotnet restore

Configure appsettings.Development.json:

Create a new file named appsettings.Development.json inside the AuthApi folder (next to appsettings.json).

Add your actual SQL Server credentials and a newly generated, strong JWT secret key to this file.

{
  "ConnectionStrings": {
    "DefaultConnection": "Server=NALADALA\\SQLEXPRESS;Database=AuthDb;User Id=sa;Password=9969;TrustServerCertificate=True;"
  },
  "JwtSettings": {
    "Secret": "YOUR_UNIQUE_AND_STRONG_JWT_SECRET_KEY_HERE_MIN_32_BYTES_LONG",
    "Issuer": "https://yourdomain.com",  // Update to your API's actual domain
    "Audience": "https://yourclientapp.com" // Update to your client application's actual domain
  }
}

(To generate a strong JWT secret, you can use a temporary C# console app with System.Security.Cryptography.RNGCryptoServiceProvider and Convert.ToBase64String.)

Database Migrations:

dotnet ef database update

This command will create the AuthDb database and Users table in your SQL Server instance.

Running the Application
In your terminal, navigate to the AuthApi project folder (AuthApiProject/AuthApi).

Run the application:

dotnet run

Your browser should automatically open to http://localhost:5285/swagger, which is the Swagger UI for testing.

API Endpoints and Testing
All API endpoints are hosted at http://localhost:5285. We'll use Thunder Client (or your preferred API client) for testing.

General Thunder Client Usage:

Open Thunder Client in VS Code.

Click + to create a new request tab.

Set HTTP Method, URL, Headers, and Body as specified below.

Click Send.

1. User Registration
URL: POST http://localhost:5285/api/Auth/register

Headers: Content-Type: application/json

Body:

{
  "username": "testuser",
  "email": "test@example.com",
  "password": "Password123!"
}

Expected Response: 200 OK (success message) or 400 Bad Request (for duplicates/validation errors).

2. User Login (Get JWT Token)
URL: POST http://localhost:5285/api/Auth/login

Headers: Content-Type: application/json

Body:

{
  "usernameOrEmail": "testuser",
  "password": "Password123!"
}

Expected Response: 200 OK with a JSON object containing the token string. Copy this token for all subsequent protected requests.

3. Accessing Protected Endpoint (Unauthorized Attempt)
URL: GET http://localhost:5285/api/User/profile

Headers: NO Authorization header.

Body: Empty.

Expected Response: 401 Unauthorized.

4. Accessing Protected Endpoint (Authorized Attempt)
URL: GET http://localhost:5285/api/User/profile

Headers: Authorization: Bearer <YOUR_JWT_TOKEN> (replace with token from step 2).

Body: Empty.

Expected Response: 200 OK with user claims (e.g., userId, username, email, role).

5. Accessing Role-Based Endpoint (User Role)
URL: GET http://localhost:5285/api/User/user-data

Headers: Authorization: Bearer <YOUR_JWT_TOKEN> (token should have "User" role).

Body: Empty.

Expected Response: 200 OK with a user-specific message.

6. Accessing Role-Based Endpoint (Admin Role)
This test has two phases:

Phase A: Forbidden Attempt (User Role Token)
URL: GET http://localhost:5285/api/User/admin-data

Headers: Authorization: Bearer <YOUR_JWT_TOKEN> (token should still have "User" role).

Body: Empty.

Expected Response: 403 Forbidden.

Phase B: Successful Access (Admin Role Token)
Update Role in DB: In SSMS, run:

USE AuthDb;
UPDATE [dbo].[Users] SET [Role] = 'Admin' WHERE [Username] = 'testuser';

Get NEW JWT Token: Re-run Step 2 (Login) to obtain a new JWT for testuser. This token will now contain the "Admin" role.

Re-run Request:

URL: GET http://localhost:5285/api/User/admin-data

Headers: Authorization: Bearer <YOUR_NEW_ADMIN_JWT_TOKEN>

Body: Empty.

Expected Response: 200 OK with an admin-specific message.

Live Project Screenshots
To impress your interviewer, include clear screenshots of your project in action here.

Project File Structure: A screenshot of your VS Code Explorer showing the AuthApi project's folder structure (as outlined in "Project Structure and File Overview" above).

Running Swagger UI: A screenshot of your browser showing the Swagger UI for your API (e.g., http://localhost:5285/swagger), ideally with one of the endpoints expanded.

Thunder Client Successful Test: A screenshot of Thunder Client showing a successful 200 OK response for a protected endpoint (e.g., api/User/profile) with the JWT in the headers.

Example Placeholder (Replace these with your actual image links):

### Project File Structure

![Project Structure Screenshot](https://placehold.co/800x450/000000/FFFFFF?text=YOUR_PROJECT_STRUCTURE_SCREENSHOT_HERE)

### Swagger UI in Action

![Swagger UI Screenshot](https://placehold.co/800x450/000000/FFFFFF?text=YOUR_SWAGGER_UI_SCREENSHOT_HERE)

### Thunder Client Test Example

![Thunder Client Successful Test](https://placehold.co/800x450/000000/FFFFFF?text=YOUR_THUNDER_CLIENT_TEST_SCREENSHOT_HERE)

(Replace https://placehold.co/... with the direct URLs to your uploaded screenshots.)

Future Enhancements
This project lays a strong foundation. Here are some potential next steps to expand its functionality and architectural robustness:

Service Layer & Repository Pattern: Refactor business logic into a dedicated service layer and abstract data access using a repository pattern for better separation of concerns and testability.

Unit Testing: Implement comprehensive unit tests for business logic (service layer) and controllers using frameworks like XUnit and mocking libraries.

Refresh Tokens: Add support for refresh tokens to provide long-lived user sessions without compromising security.

Email Verification: Implement a flow to send email verification links for new user registrations.

Forgot Password Flow: Create a secure mechanism for users to reset their forgotten passwords via email.

Global Error Handling: Implement custom middleware to provide consistent and user-friendly error responses for unhandled exceptions.

Rate Limiting: Protect against abuse by limiting the number of requests a user or IP can make within a certain timeframe.

Logging: Integrate a robust logging library (e.g., Serilog) to track application events, errors, and user activity.

Dockerization: Containerize the API using Docker for easier deployment and environment consistency.
