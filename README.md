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

3: Apply Database Migrations:

dotnet ef database update

4: Run the Application: 

dotnet run

The API will be available at:

https://localhost:5001/swagger

## API Endpoints and Testing:
### Testing the API with Thunder Client (Step-by-Step)

All API endpoints are hosted at http://localhost:5285. We'll use Thunder Client for testing.

## Test 1: User Registration

Objective: Create a new user account.

Request Setup:

1: Method: Change the dropdown from GET to POST.

2: URL: Enter http://localhost:5285/api/Auth/register

3: Headers Tab:

4: Add a new header:
Name: Content-Type
Value: application/json

5: Body Tab:
Select JSON from the dropdown.
Paste the login credentials:
{
  "username": "Vishal",
  "email": "VishalAxisBank@gmail.com",
  "password": "vishal12340!!@@" 
}

(You can change these values).

6: Send Request: Click the Send button (green play icon).

7: Expected Response: Status: 200 OK

8: Response Body: "Registration successful!"

***Try sending this request again with the exact same username/email. You should get a 400 Bad Request with a message like "Username already exists." or "Email already exists."***

### Animated Screenshot: User Registration Success

![Animation](https://github.com/user-attachments/assets/9a304a95-1600-4053-8658-34840ac2cfef)

## Test 2: User Login (Get JWT Token)

Objective: To authenticate a registered user and obtain a JSON Web Token (JWT). This token is essential for accessing all protected endpoints in subsequent tests.

1: Create New Request: Click the + button.

2: Method: Select POST.

3: URL: Enter http://localhost:5285/api/Auth/login

4: Headers Tab:

5: Name: Content-Type

6: Value: application/json

7: Body Tab: Select JSON from the dropdown. Paste the login credentials using the user you just registered:
{
 "usernameOrEmail": "Vishal",
 "password": "vishal12340!!@@"
}
8: Send Request: Click the Send button.

9: Expected Response:

10: Status: 200 OK

11: Response Body: A JSON object containing your JWT. It will look like {"token": "..."}.

12: VERY IMPORTANT: Copy the entire token string (the very long string of characters) from the response body! You will use this token in all subsequent tests.

### Animated Screenshot: User Login & JWT Retrieval

![Animation](https://github.com/user-attachments/assets/192eada1-feb6-44aa-881d-bdda9ea5303b)

## Test 3: Accessing Protected Endpoint (Unauthorized Attempt)

Objective: To verify that the /api/User/profile endpoint, which is secured by the [Authorize] attribute, correctly responds with an "Unauthorized" status when no JWT is provided. This demonstrates your API's basic protection mechanism.

1: Create New Request: Click the + button.

2: Method: Select GET.

3: URL: Enter http://localhost:5285/api/User/profile

4: Headers Tab:

Crucially, ensure there is NO Authorization header present. If you previously added one, uncheck its checkbox on the left side or delete it. Default headers like User-Agent are fine.

5: Body Tab: Ensure it is empty or set to None.

6: Send Request: Click the Send button.

7: Expected Response:

8: Status: 401 Unauthorized

9: Response Body: May be empty or contain a simple "Unauthorized" message.

This 401 Unauthorized status is the correct and desired outcome for this test. It confirms your API successfully blocks unauthenticated requests.

### Animated Screenshot: Unauthorized Access Attempt

![Animation](https://github.com/user-attachments/assets/37bb8a0e-aa39-460c-8dfb-4c88bc222487)

