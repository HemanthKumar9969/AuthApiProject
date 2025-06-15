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

4: Add a new header: Name: Content-Type, Value: application/json

5: Body Tab: Select JSON from the dropdown. Paste the login credentials:

JSON:

{
  "username": "Vishal",
  "email": "VishalAxisBank@gmail.com",
  "password": "vishal12340!!@@" 
 }

(You can change these values).

6: Send Request: Click the Send button (green play icon).
Expected Response: Status: 200 OK
Response Body: "Registration successful!"

***Try sending this request again with the exact same username/email. You should get a 400 Bad Request with a message like "Username already exists." or "Email already exists."***

### Animated Screenshot: User Registration Success

![Animation](https://github.com/user-attachments/assets/9a304a95-1600-4053-8658-34840ac2cfef)

## Test 2: User Login (Get JWT Token)

Objective: To authenticate a registered user and obtain a JSON Web Token (JWT). This token is essential for accessing all protected endpoints in subsequent tests.

1: Create New Request: Click the + button.

2: Method: Select POST.

3: URL: Enter http://localhost:5285/api/Auth/login

4: Headers Tab: Name: Content-Type, Value: application/json

7: Body Tab: Select JSON from the dropdown. Paste the login credentials using the user you just registered:

JSON:

{
  "username": "Vishal",
  "password": "vishal12340!!@@" 
 }
 
8: Send Request: Click the Send button.

9: Expected Response:
Status: 200 OK
Response Body: A JSON object containing your JWT. It will look like {"token": "..."}.

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
Status: 401 Unauthorized
Response Body: May be empty or contain a simple "Unauthorized" message.

This 401 Unauthorized status is the correct and desired outcome for this test. It confirms your API successfully blocks unauthenticated requests.

### Animated Screenshot: Unauthorized Access Attempt

![Animation](https://github.com/user-attachments/assets/37bb8a0e-aa39-460c-8dfb-4c88bc222487)

## Test 4: Accessing Protected Endpoint (Authorized Attempt)

Objective: To successfully access the /api/User/profile endpoint by providing the valid JWT obtained from Test 2. This confirms your API correctly validates the token and grants access.

1: Create New Request: Click the + button.

2: Method: Select GET.

3: URL: Enter http://localhost:5285/api/User/profile

4: Headers Tab:

5: Add a new header: Name: Authorization, Value: Type Bearer  (that's the word "Bearer", followed by a single space), then paste the entire JWT token you copied from Test 2.

Example of full value: Bearer eyJhbGci...YOUR_TOKEN...

9: Body Tab: Ensure it is empty or set to None.

10: Send Request: Click the Send button.

11: Expected Response:
   Status: 200 OK
   Response Body: A JSON object showing the claims extracted from your token (e.g., userId, username, email, role).

### Animated Screenshot: Authorized Access Success

![Animation](https://github.com/user-attachments/assets/9a7aeda8-2520-4da1-96f6-2c59b28a06a2)

## Test 5: Accessing Role-Based Endpoint (User Role)

Objective: To confirm that a user with the default "User" role can successfully access an endpoint (/api/User/user-data) specifically authorized for "User" roles. Your testuser should initially have the "User" role from registration.

1: Create New Request: Click the + button.

2: Method: Select GET.

3: URL: Enter http://localhost:5285/api/User/user-data

4: Headers Tab:
Name: Authorization
Value: Bearer  (followed by a space and the same JWT token you've been using from Test 2).

5: Body Tab: Ensure it is empty or set to None.

6: Send Request: Click the Send button.

7: Expected Response:
Status: 200 OK
Response Body: "Hello, User testuser! This data is accessible to general users."

### Animated Screenshot: User Role Access

![Animation](https://github.com/user-attachments/assets/7b3590ba-6c04-489d-a8cd-c9853b0528b1)

## Test 6. Accessing Role-Based Endpoint (Admin Role)
This is a two-phase test. First, we'll demonstrate a forbidden attempt with a "User" role token. Then, we'll manually elevate the user's role to "Admin" in the database, obtain a new token, and show successful access.

## Phase A: Forbidden Attempt (Using a 'User' Role Token)
Objective: To demonstrate that a user whose token only contains the "User" role cannot access the /api/User/admin-data endpoint, which is protected with [Authorize(Roles = "Admin")].

1: Create New Request: Click the + button.

2: Method: Select GET.

3: URL: Enter http://localhost:5285/api/User/admin-data

3:Headers Tab: Name: Authorization, Value: Bearer  (followed by a space and the same JWT token you've been using from Test 2. This token should still represent a "User" role).

4: Body Tab: Ensure it is empty or set to None.

5: Send Request: Click the Send button.

6: Expected Response (Phase A - Forbidden): Status: 403 Forbidden, Response Body: Likely empty or a simple "Forbidden" message.

This 403 Forbidden status is the correct and desired outcome for this phase. It demonstrates that your API's authorization correctly denies access to users who lack the required Admin role.

### Animated Screenshot: Admin Role Forbidden

![Animation](https://github.com/user-attachments/assets/0996e8a6-766f-4b6d-b351-35882f29ef61)


## Transition: Elevating User Role in MS SQL Server
Before proceeding to Phase B, you must update the testuser's role in the database and obtain a new JWT that reflects this change.

1: Open SQL Server Management Studio (SSMS).

2: Connect to your SQL Server instance (e.g., NALADALA\SQLEXPRESS).

2: Open a New Query Window: Right-click on your AuthDb database, then select New Query.

3: Execute the below SQL Query:

**USE AuthDb; -- Ensures you're working on the correct database

UPDATE [dbo].[Users]
SET [Role] = 'Admin'
WHERE [Username] = 'Vishal'; -- IMPORTANT: Use the exact username you registered.**


6: Verify Success: In the "Messages" tab, confirm you see (1 row affected).

7: Get a NEW JWT Token (Crucial Step):

8: Go back to Thunder Client.

Re-run your Login Request (Test 2) for Vishal.

9: Copy the newly generated JWT token from the response. This token will now include the updated Admin role claim. The old token is now stale for role-based checks.

## Phase B: Successful Access (Using the 'Admin' Role Token)

Objective: To successfully access the /api/User/admin-data endpoint using the new JWT which now contains the "Admin" role.

1: Thunder Client Setup:
If you still have the request tab open from Phase A, use that. Otherwise, create a new request.

2: Method: Select GET.

3: URL: http://localhost:5285/api/User/admin-data

4: Headers Tab: Name: Authorization, Value: Bearer  (followed by a space and the NEW JWT token you just obtained after updating the role in SSMS).

5:Send Request: Click the Send button.

6:Expected Response (Phase B - Success): Status: 200 OK, Response Body: "Hello, Admin testuser! This data is only accessible to users with the 'Admin' role."

This 200 OK status confirms that your API correctly grants access when the required Admin role is present in the JWT.

### Animated Screenshot: Admin Role Success
